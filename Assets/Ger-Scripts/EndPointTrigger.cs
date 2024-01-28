using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    public LayerMask objectsLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == ObjectBehavior.OBJECTSTAG)
            {
                ObjectBehavior obj = collision.gameObject.GetComponent<ObjectBehavior>();

                if(obj != null)
                {
                    if(obj.objectType != ObjectType.Clown)
                    {
                        Destroy(obj.gameObject);                        
                    }
                }

                GroundObject ground = collision.GetComponentInParent<GroundObject>();

                if (ground != null)
                {
                    SpawnController.Instance.SpawnGround(ground.endPos.position);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == ObjectBehavior.OBJECTSTAG)
            {
                GroundObject ground = collision.GetComponentInParent<GroundObject>();

                if (ground != null)
                {

                    Destroy(ground.transform.gameObject);
                }
            }
        }
    }
}
