using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer render;
    Collider2D col;
    Rigidbody2D rb;
    EnemyController control;
    EnemyHealth health;

    public void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        control = GetComponent<EnemyController>();
        health = GetComponent<EnemyHealth>();
    }

    public void Update()
    {
        control.Update();
        health.Update();
    }
}
