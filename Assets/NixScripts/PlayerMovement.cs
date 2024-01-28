using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool jumpInput;


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

        playerStates.IsWalking = true;
        playerStates.IsJumping = false;
        playerStates.IsFalling = false;

        playerStates.animator = GetComponentInChildren<Animator>();
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
            RB.velocity = new Vector2(RB.velocity.x + Time.deltaTime * speed, RB.velocity.y);
        }
        else
        {
            transform.position = new Vector3(GameManager.Instance.playerNormalPosition.position.x, transform.position.y);
        }
    }

    public void HandleMovement()
    {
        if(CheckIsGrounded())
        {
            playerStates.IsFalling = false;

            if (playerStates.IsJumping == false)
            {
                if(jumpInput == true)
                {
                    if(playerStates.IsJumping == false)
                    {
                        playerStates.IsJumping = true;
                        RB.velocity = new Vector2(RB.velocity.x, jumpingPower);
                        playerStates.PlayJump();
                        OnPlayerJump?.Invoke();
                    }
                }
                else
                {
                    if (playerStates.IsWalking == false)
                    {
                        playerStates.IsWalking = true;
                        OnPlayerGrounded?.Invoke();
                        playerStates.PlayWalk();
                    }

                }

            }
        }
        else
        {
            if(RB.velocity.y <= 0)
            {
                playerStates.IsJumping = false;
                playerStates.IsWalking = false;

                if(playerStates.IsFalling == false)
                {
                    playerStates.IsFalling = true;
                    OnPlayerFalling?.Invoke();
                    playerStates.PlayFall();
                }

            }
            else
            {
                playerStates.IsFalling = false;
                playerStates.IsWalking = false;

                if(playerStates.IsJumping == false)
                {
                    playerStates.IsJumping = true;
                    OnPlayerJump?.Invoke();
                    playerStates.PlayJump();
                }
            }
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;

        }
        if (context.canceled)
        {
            jumpInput = false;

            //RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
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

    public void PlayWalk()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Jump", false);
        animator.SetBool("Fall", false);
    }

    public void PlayJump()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", true);
        animator.SetBool("Fall", false);
    }

    public void PlayFall()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Fall", true);
    }
}
