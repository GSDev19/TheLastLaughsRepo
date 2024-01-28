using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    protected Rigidbody2D RB;
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (CollisionCheck.CheckIfClown(gameObject) == false)
        {
            RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
        }
    }
    private void Update()
    {
        if(CollisionCheck.CheckIfClown(gameObject) == false)
        {
            RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
        }
    }
}
