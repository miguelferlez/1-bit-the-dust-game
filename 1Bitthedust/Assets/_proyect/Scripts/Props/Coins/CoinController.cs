using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int value;

    public GameObject deadPrefab;
    private ParticleSystem deadParticles;

    public AudioSource splashAudio;
    float originalPitch;
    float pitchRange = 0.2f;

    Collider2D col;

    public void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            ScoreManager.AddScore(value);
            Finish();
        }

        if(col.gameObject.layer == 11)
        {
            Finish();
        }
    }

    public void Finish()
    {
        col.enabled = false;
        var coinParticles = ObjectPool.Instance.GetObject(deadPrefab);
        coinParticles.SetActive(true);
        coinParticles.transform.position = transform.position;
        StartCoroutine(SoundAndDisable());
    }

    public void CoinAudio()
    {
        originalPitch = splashAudio.pitch;
        splashAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
        splashAudio.Play();
    }

    IEnumerator SoundAndDisable()
    {
        CoinAudio();
        var render = GetComponent<SpriteRenderer>();
        render.enabled = false;
        yield return new WaitForSeconds(splashAudio.clip.length);
        gameObject.SetActive(false);
    }
}
