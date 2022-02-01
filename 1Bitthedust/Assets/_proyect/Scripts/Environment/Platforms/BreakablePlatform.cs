using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    Animator anim;
    AnimationState state;
    Collider2D col;
    SpriteRenderer render;
    Coroutine cor;

    bool isTouched;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        render = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        anim.SetBool("isTouched", isTouched);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            if (cor == null)
            {
                isTouched = true;
                cor = StartCoroutine(StartShake());
            }
        }
    }

    IEnumerator StartShake()
    {
        yield return new WaitForSeconds(2f);
        render.color = new Color(0, 0, 0, 0);
        col.enabled = false;
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeBack());
    }

    IEnumerator FadeBack()
    {
        isTouched = false;
       
        Color Alpha = new Color(1, 1, 1, 1);

        while(render.color != Alpha)
        {
            render.color = Color.Lerp(render.color, Alpha, 0.025f);
            if (render.color == Alpha)
            {
                col.enabled = true;
            }
        cor = null;
        }
            yield return null;
    }
}
