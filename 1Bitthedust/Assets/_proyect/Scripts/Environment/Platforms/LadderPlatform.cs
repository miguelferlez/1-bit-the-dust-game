using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlatform : MonoBehaviour
{
    Collider2D col;

    public bool isPlayer;
    public bool isInput;

    public void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && isPlayer)
        {
            isInput = true;
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            isInput = false;
        }

        if (isInput)
        {
            col.isTrigger = true;
            //isPlayer = false;
        }
        else
        {
            col.isTrigger = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isPlayer = true;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        isPlayer = false;
    }
}
