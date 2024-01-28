using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public static Action OnPlayerJump;
    public static Action OnPlayerGrounded;
    public static Action OnPlayerFalling;

    public Rigidbody2D RB;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float speed = 8f;
    public float jumpingPower = 16f;
    public float groundCheckRadious = 0.5f;

    public EntityBoolStates playerStates;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        playerStates.Initialize(GetComponentInChildren<Animator>());
    }

    void Update()
    {
        HandleMovement();
        ReturnToPosition();
    }

    private void ReturnToPosition()
    {
        if (transform.position.x < GameManager.Instance.playerNormalPosition.position.x)
        {
            RB.velocity = new Vector2(speed, RB.velocity.y);
        }
        else
        {
            transform.position = new Vector3(GameManager.Instance.playerNormalPosition.position.x, transform.position.y);
        }
    }

    public void HandleMovement()
    {
        if (CheckIsGrounded())
        {
            HandleGrounded();
        }
        else
        {
            HandleAirborne();
        }
    }

    private void HandleGrounded()
    {
        playerStates.IsFalling = false;

        if (playerStates.IsJumping)
        {
            playerStates.IsJumping = false;
            OnPlayerGrounded?.Invoke();
            playerStates.PlayWalk();
        }
        else
        {
            // Handle walking here if needed
        }
    }

    private void HandleAirborne()
    {
        playerStates.IsWalking = false;

        if (RB.velocity.y <= 0 && !playerStates.IsFalling)
        {
            playerStates.IsFalling = true;
            OnPlayerFalling?.Invoke();
            playerStates.PlayFall();
        }
        else if (RB.velocity.y > 0 && !playerStates.IsJumping)
        {
            playerStates.IsJumping = true;
            OnPlayerJump?.Invoke();
            playerStates.PlayJump();
            AudioManager.Instance?.PlayOneShot(FmodEvents.Instance.Jump, transform.position);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && CheckIsGrounded())
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpingPower);
        }
    }

    private bool CheckIsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
    }
}

[System.Serializable]
public class EntityBoolStates
{
    public bool IsWalking, IsJumping, IsFalling;
    public Animator animator;

    public void Initialize(Animator animator)
    {
        this.animator = animator;
        IsWalking = false;
        IsJumping = false;
        IsFalling = false;
    }

    public void PlayWalk()
    {
        animator.Play("Walk");
    }

    public void PlayJump()
    {
        animator.Play("Jump");
    }

    public void PlayFall()
    {
        animator.Play("Fall");
    }
}
