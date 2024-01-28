using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;

public class EnemyObject : ObjectBehavior
{
    public int maxHits = 20;
    public int minHits = 1;

    public int necessaryHits = 2;

    public TextMeshProUGUI hitsText;

    private void Start()
    {
        SetRandomHitAmount();
    }
    public void SetRandomHitAmount()
    {
        necessaryHits = Random.Range(minHits, maxHits + 1);
        hitsText.text = necessaryHits.ToString();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == PlayerMovement.PLAYERTAG)
            {
                GameManager.Instance.HandleEnemyHit(this);
            }
        }
    }
}
