using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer render;

    public float speed;
    private float currentSpeed;
    public int force;

    float distance;

    public Transform[] posMovement;
    private Transform target;
    private int targetIndex;

    bool isDead;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        if (posMovement.Length == 0)
        {
            return;
        }
        else
        {
            transform.position = posMovement[0].position;
            targetIndex = 1;
            target = posMovement[targetIndex];
        }
        currentSpeed = speed;
    }

    #region ENEMY_MOVEMENT
    public void Update()
    {
        if (posMovement.Length == 0)
        {
            return;
        }
        else
        {
            distance = target.position.x - transform.position.x;
            if (Mathf.Abs(distance) < 0.01f)
            {
                targetIndex++;
                render.flipX = false;
                if (targetIndex == posMovement.Length)
                {
                    render.flipX = true;
                    targetIndex = 0;
                }
                target = posMovement[targetIndex];
            }
            Movement(target);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
        }
    }

    public void Movement(Transform target)
    {
        var force = target.position.x - transform.position.x;
        rb.AddForce(new Vector2(Mathf.Sign(force), 0) * speed, ForceMode2D.Impulse);
    }
    #endregion
}
