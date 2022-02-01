using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnlock
{
    void Open();
    void Close();
}

public class KeyController : MonoBehaviour
{
    public List<IUnlock> unlock = new List<IUnlock>();

    public AudioSource keyAudio;
    Light light;

    bool enabled;
    bool isOpen;

    public void Init()
    {
        light = GetComponentInChildren<Light>();
        light.enabled = true;
        enabled = true;
        gameObject.SetActive(true);
    }

    public void Finish()
    {
        light.enabled = false;
        enabled = false;
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {

            isOpen = true;
            foreach (var item in unlock)
            {
                item.Open();
            }
            StartCoroutine(SoundAndDisable());
        }
    }

    public void AddKey(IUnlock unlockObj)
    {
        unlock.Add(unlockObj);
    }

    public void RemoveKey(IUnlock unlockObj)
    {
        unlock.Remove(unlockObj);
    }

    IEnumerator SoundAndDisable()
    {
        keyAudio.Play();
        var render = GetComponent<SpriteRenderer>();
        render.enabled = false;
        yield return new WaitForSeconds(keyAudio.clip.length);
        gameObject.SetActive(false);
    }
}

