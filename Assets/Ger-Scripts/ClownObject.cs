using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

public class ClownObject : ObjectBehavior
{
    public float minFollowOffset = 1f;
    public float maxFollowOffset = 3f;
    public float currentOffset = 0f;

    public bool picked = false;
    public bool shouldFollowPlayer = false;

    private void Start()
    {
        picked = false;
        shouldFollowPlayer = false;
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

    private void SetOffset()
    {
        float randomValue = Random.Range(minFollowOffset, maxFollowOffset);
        currentOffset = randomValue;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == PlayerMovement.PLAYERTAG)
            {
                picked = true;
                GameManager.Instance.AddClown(this);
            }
        }
    }

}
