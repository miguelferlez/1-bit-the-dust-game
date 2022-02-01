using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float speed;

    public Transform[] posMovement;
    private Transform target;
    private int targetIndex;

    public void Awake()
    {
        transform.position = posMovement[0].position;
        targetIndex = 1;
        target = posMovement[targetIndex];
    }

    public void Update()
    {
        if (target.position == transform.position)
        {
            targetIndex++;
            if (targetIndex == posMovement.Length)
            {
                targetIndex = 0;
            }
            target = posMovement[targetIndex];
        }

        Moving(target);
    }

    public void Moving(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            col.transform.SetParent(gameObject.transform);
        }
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            col.transform.SetParent(null);
        }
    }
}