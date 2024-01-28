using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {


            if (CollisionCheck.CheckIfGround(collision.gameObject))
            {
                GroundObject ground = collision.GetComponentInParent<GroundObject>();

                if (ground != null)
                {
                    SpawnController.Instance.SpawnGround(ground.endPos.position);
                }
            }

            if (CollisionCheck.CheckIfBG(collision.gameObject))
            {
                BGObject bg = collision.GetComponentInParent<BGObject>();

                if (bg != null)
                {
                    SpawnController.Instance.SpawnBG(bg.endPos.position);
                }
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (CollisionCheck.CheckIfBlocker(collision.gameObject) || CollisionCheck.CheckIfEnemy(collision.gameObject) || CollisionCheck.CheckIfNPC(collision.gameObject))
            {
                Destroy(collision.gameObject);
            }

            if (CollisionCheck.CheckIfBG(collision.gameObject))
            {
                BGObject bg = collision.GetComponentInParent<BGObject>();

                if (bg != null)
                {
                    Destroy(bg.transform.gameObject);
                }
            }

            if (CollisionCheck.CheckIfGround(collision.gameObject))
            {
                GroundObject ground = collision.GetComponentInParent<GroundObject>();

                if (ground != null)
                {
                    Destroy(ground.transform.gameObject);
                }
            }
            if (CollisionCheck.CheckIfBullet(collision.gameObject))
            {
                Destroy(collision.gameObject);
            }

            if (CollisionCheck.CheckIfPlayer(collision.gameObject))
            {
                if (PlayerMovement.Instance.RB.velocity.x < 0 || PlayerMovement.Instance.playerStates.IsFalling)
                {
                    GameManager.Instance.HandleLooseGame();
                }
            }
        }
    }
}
