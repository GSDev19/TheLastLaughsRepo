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
                ObjectBehavior obj = collision.GetComponent<ObjectBehavior>();

                if(obj != null)
                {
                    if(obj.objectType != ObjectType.Ground)
                    {
                        Destroy(obj.gameObject);
                    }
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
                GroundObject obj = collision.GetComponent<GroundObject>();

                if (obj != null)
                {
                    if (obj.objectType == ObjectType.Ground)
                    {
                        Destroy(obj.gameObject);
                        SpawnController.Instance.SpawnGround();
                    }
                }
            }
        }
    }
}
