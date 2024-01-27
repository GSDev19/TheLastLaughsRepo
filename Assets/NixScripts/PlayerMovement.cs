using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D RB;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float speed = 8f;
    public float groundSpeed = 8f;
    public float jumpingPower = 16f;
    public float groundCheckRadious = 0.5f;
    bool isjumping = false;

    private void Start()
    {
        isjumping = false;
    }
    void Update()
    {
        ReturnToPosition();
    }

    private void ReturnToPosition()
    {
        if(transform.position.x < GameManager.Instance.playerNormalPosition.position.x)
        {
            Debug.Log("BEHIND");
            RB.velocity = new Vector2(RB.velocity.x + Time.deltaTime * speed, RB.velocity.y);
        }
        else
        {
            transform.position = new Vector3(GameManager.Instance.playerNormalPosition.position.x, transform.position.y);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded())
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpingPower);
        }
        if (context.canceled && RB.velocity.y > 0f)
        {
            RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
    }
}
