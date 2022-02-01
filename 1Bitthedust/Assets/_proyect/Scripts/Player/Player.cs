using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    SpriteRenderer render;
    Collider2D col;
    Rigidbody2D rb;
    PlayerController control;
    PlayerHealth health;

    public void Awake()
    {
        Instance = this;

        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        control = GetComponent<PlayerController>();
        health = GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        control.Update();
        health.Update();
    }
}
