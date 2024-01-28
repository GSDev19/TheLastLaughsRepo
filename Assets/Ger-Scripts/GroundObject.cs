using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : MonoBehaviour
{
    private Rigidbody2D RB;
    public Transform endPos;

    private void Awake()
    {
        RB = GetComponentInChildren<Rigidbody2D>();
    }
    private void Start()
    {
        RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
    }
    private void Update()
    {
        RB.velocity = new Vector3(GameManager.Instance.objectSpeedX, GameManager.Instance.objectSpeedY, 0);
    }
}
