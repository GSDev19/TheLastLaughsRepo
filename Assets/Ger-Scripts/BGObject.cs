using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObject : MonoBehaviour
{
    private Rigidbody2D RB;
    public Transform endPos;
    public float bgSpeed = -7f;

    private void Awake()
    {
        RB = GetComponentInChildren<Rigidbody2D>();
    }
    private void Start()
    {
        RB.velocity = new Vector3(bgSpeed, 0, 0);
    }
    private void Update()
    {
        RB.velocity = new Vector3(bgSpeed, 0, 0);
    }
}
