using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : MonoBehaviour
{
    public ObjectType objectType;
    private Rigidbody2D RB;
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
    }
    private void Update()
    {
        RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
    }
}
