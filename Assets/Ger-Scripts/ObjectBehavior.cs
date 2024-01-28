using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public static string OBJECTSTAG = "Object";
    public ObjectType objectType;
    protected Rigidbody2D RB;
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        if(objectType != ObjectType.Clown)
        {
            RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
        }
    }
    private void Update()
    {
        if (objectType != ObjectType.Clown)
        {
            RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
        }

    }
}

public enum ObjectType
{
    Blocker,
    Enemy,
    NPC,
    Clown,
}
