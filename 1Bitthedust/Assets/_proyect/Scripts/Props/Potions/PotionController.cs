using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    Animator anim;
    SpriteRenderer render;
    Light light;

    bool isUsed;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light>();
    }

    public void Update()
    {
        anim.SetBool("isUsed", isUsed);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<PlayerHealth>();

        if(col.gameObject.layer == 8)
        {
            if(player.currentHealth >= player.maxHealth)
            {
                player.currentHealth = player.maxHealth;
            }
            else
            {
                Debug.Log("Player took a potion!");
                player.currentHealth++;

                isUsed = true;
                StartCoroutine(PotionDestroy());
            }
        }
    }

    IEnumerator PotionDestroy()
    {
        Color deadColor = new Color(1, 1, 1, 0);

        while (isUsed)
        {
            light.enabled = false;
            render.color = Color.Lerp(render.color, deadColor, 0.08f);
            if (render.color == deadColor)
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
