using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActivable
{
    void Activate();
    void Deactivate();
}

public class ComputerController : MonoBehaviour {

    public List<IActivable> activables = new List<IActivable>();

    SpriteRenderer render;

    public Sprite deactiveComputer;
    public Sprite activeComputer;

    public AudioSource deactiveAudio;
    public AudioSource activeAudio;

    bool isActive;

    public void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = deactiveComputer;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 8 )
        {
            if (isActive)
            {
                render.sprite = deactiveComputer;
                deactiveAudio.Play();
                isActive = false;
                foreach (var item in activables)
                {
                    item.Deactivate();
                }
            }
            else
            {           
                render.sprite = activeComputer;
                activeAudio.Play();
                isActive = true;
                foreach (var item in activables)
                {
                      item.Activate();
                }
            }
        }
    }

    public void AddActivable(IActivable objRandom)
    {
        activables.Add(objRandom);
    }

    public void RemoveActivable(IActivable objRandom)
    {
        activables.Remove(objRandom);
    }

}
