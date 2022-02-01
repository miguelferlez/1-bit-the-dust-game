using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer render;
    Light light;

    public Collider2D deathCollider;
    public Collider2D hitCollider;

    float lastTimeCollision;
    bool isDead;

    public GameObject deadPrefab;
    private ParticleSystem deadParticles;

    public AudioSource deadAudio;
    float originalPitch;
    float pitchRange = 0.1f;

    public int value;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light>();
    }

    public void Update()
    {
        anim.SetBool("isDead", isDead);
    }

    public void EnemyAudio()
    {
        originalPitch = deadAudio.pitch;
        deadAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
        deadAudio.Play();
    }

    #region ENEMY_DAMAGE
    public void OnCollisionEnter2D(Collision2D col)
    {
        var playerHealth = col.gameObject.GetComponent<PlayerHealth>();

        if (col.gameObject.layer == 8)
        {
            if (col.otherCollider == deathCollider)
            {

                Debug.Log("Player touched deathCollider!");
                if (lastTimeCollision == Time.time)
                {
                    playerHealth.currentHealth++;
                }

                Die();

                ScoreManager.AddScore(value);
            }
            else if (col.otherCollider == hitCollider)
            {
                Debug.Log("Player touched hitCollider!");
                if (lastTimeCollision == Time.time)
                {
                    return;
                }

                playerHealth.TakeDamage(1);
            }
        }

        lastTimeCollision = Time.time;
    }

    public void Die()
    {
        isDead = true;
        StartCoroutine(DeathEnemy());

        deathCollider.enabled = false;
        hitCollider.enabled = false;
        rb.simulated = false;

        var enemyParticles = ObjectPool.Instance.GetObject(deadPrefab);
        enemyParticles.SetActive(true);
        enemyParticles.transform.position = transform.position;
    }

    IEnumerator DeathEnemy()
    {
        Color deadColor = new Color(1, 1, 1, 0);
        float startTime = Time.time;
        light.enabled = false;

        EnemyAudio();
        while (isDead)
        {
            render.color = Color.Lerp(render.color, deadColor, (Time.time - startTime) / 100f);
            if (render.color == deadColor)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(deadAudio.clip.length);
        transform.parent.gameObject.SetActive(false);
    }
    #endregion 
}
