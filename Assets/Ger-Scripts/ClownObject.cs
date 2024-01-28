using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownObject : ObjectBehavior
{
    public float minFollowOffset = 1f;
    public float maxFollowOffset = 3f;
    public float currentOffset = 0f;

    public bool picked = false;
    public bool shouldFollowPlayer = false;

    public EntityBoolStates entityBoolStates;

    public int currentLayer = 0;
    private SpriteRenderer spriteRenderer;

   private void OnEnable()
    {
        PlayerMovement.OnPlayerJump += HandlePlayerJump;
        PlayerMovement.OnPlayerGrounded += WalkAnimation;
        PlayerMovement.OnPlayerFalling += FallAnimation;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerJump -= HandlePlayerJump;
        PlayerMovement.OnPlayerGrounded -= WalkAnimation;
        PlayerMovement.OnPlayerFalling -= FallAnimation;
    }

    private void HandlePlayerJump()
    {
        if (picked && shouldFollowPlayer)
        {
            entityBoolStates.PlayJump();
        }
    }

    private void Start()
    {
        picked = false;
        shouldFollowPlayer = false;
        entityBoolStates.animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SetOffset();
    }

    private void Update()
    {
        if(picked == false)
        {
            RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
        }
        else
        {
            if( Mathf.Abs( transform.position.x - PlayerMovement.Instance.transform.position.x) > currentOffset)
            {
                shouldFollowPlayer =true;
            }
        }

        if(shouldFollowPlayer == true)
        {
            transform.position = new Vector3(PlayerMovement.Instance.transform.position.x - currentOffset, PlayerMovement.Instance.transform.position.y);
        }
    }

    private void WalkAnimation()
    {
        if (shouldFollowPlayer == true)
        {
            entityBoolStates.PlayWalk();
        }
    }
    private void JumpAnimation()
    {
        Debug.Log("JUMPCOWNout");
        if (shouldFollowPlayer == true)
        {
            Debug.Log("JUMPCOWN");
            entityBoolStates.PlayJump();
        }
    }

    private void FallAnimation()
    {
        if (shouldFollowPlayer == true)
        {
            entityBoolStates.PlayFall();
        }
    }
    private void SetOffset()
    {
        float randomValue = Random.Range(minFollowOffset, maxFollowOffset);
        currentOffset = randomValue;

        currentLayer = Random.Range(0, 1000);
        spriteRenderer.sortingOrder = currentLayer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (CollisionCheck.CheckIfPlayer(collision.gameObject))
            {
                picked = true;
                GameManager.Instance.AddClown(this);
            }
        }
    }

}
