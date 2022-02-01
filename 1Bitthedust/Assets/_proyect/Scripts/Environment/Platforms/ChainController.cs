using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : MonoBehaviour
{

    private PlayerController player;
    public float balanceForce;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            player = col.gameObject.GetComponent<PlayerController>();

            ConnectToChain();

            col.transform.SetParent(gameObject.transform);
            col.gameObject.transform.position = transform.position;
        }
    }

    public void Update()
    {
        if (player != null)
        {
            if (player)
            {
                DisconectToChain();
                player.speed *= balanceForce;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            DisconectToChain();
        }
    }


    public void ConnectToChain()
    {
        var playerRb = player.GetComponent<Rigidbody2D>();
        var playerCol = player.GetComponent<Collider2D>();

        player.speed = 0;

        playerCol.isTrigger = true;
        player.PlayerMovement();
    }

    public void DisconectToChain()
    {
        var playerRb = player.GetComponent<Rigidbody2D>();
        var playerCol = player.GetComponent<Collider2D>();

        player.speed = 1;
        playerRb.isKinematic = false;
        playerCol.isTrigger = false;

        player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
