using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour , IActivable
{
    SpriteRenderer render;
    Collider2D col;

    public ComputerController pcControl;

    public Sprite enabledBlock;
    public Sprite disabledBlock;

    public void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Start()
    {
        pcControl.AddActivable(this);
    }

    public void Activate()
    {
        col.enabled = false;
        render.sprite = disabledBlock;
        StartCoroutine(DisabledBlockColor());
    }

    public void Deactivate()
    {
        col.enabled = true;
        render.sprite = enabledBlock;
        StartCoroutine(EnabledBlockColor());
    }

    public void OnDestroy()
    {
        pcControl.RemoveActivable(this);
    }

    IEnumerator DisabledBlockColor()
    {
        var alphaBlock = new Color(1, 1, 1, 100f / 255f);

        while (render.color != alphaBlock)
        {
            render.color = Color.Lerp(render.color, alphaBlock, 0.15f);
            yield return null;
        }
    }

    IEnumerator EnabledBlockColor()
    {
        var alphaBlock = Color.white;
        render.color = new Color(1, 1, 1, 100f / 255f);

        while (render.color != alphaBlock)
        {
            render.color = Color.Lerp(render.color, alphaBlock, 0.15f);
            yield return null;
        }
    }
}
