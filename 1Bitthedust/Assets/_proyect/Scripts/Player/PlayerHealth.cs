using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Animator anim;
    SpriteRenderer render;

    public int maxHealth;
    public int currentHealth;

    public bool isDead;

    public GameObject deadPrefab;
    private ParticleSystem deadParticles;

    public AudioSource deadAudio;
    float originalPitch;
    float pitchRange = 0.1f;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;

        deadParticles = Instantiate(deadPrefab).GetComponent<ParticleSystem>();
        deadParticles.transform.SetParent(transform);
        deadParticles.gameObject.SetActive(false);
    }

    private void Start()
    {
        Timer.Instance.LoadTime();
    }

    public void Update()
    {
        anim.SetBool("isDead", isDead);

        deadParticles.transform.position = transform.position;
    }

    #region PLAYER_DAMAGE
    public void Die()
    {
        var playerControl = GetComponent<PlayerController>();
        var playerCol = GetComponent<Collider2D>();
        var playerRb = GetComponent<Rigidbody2D>();
        var playerWalkParticles = playerControl.walkPrefab;
        

        isDead = true;
        StartCoroutine(DeathPlayer());

        playerControl.enabled = false;
        playerControl.walkAudio.GetComponent<AudioSource>().enabled = false;
        playerCol.enabled = false;
        playerRb.simulated = false;

        deadParticles.gameObject.SetActive(true);
        playerWalkParticles.gameObject.SetActive(false);

        originalPitch = deadAudio.pitch;
        deadAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
        deadAudio.Play();

        CheckPointData.Instance.SaveCondition("MustLoadTime", true);
        CheckPointData.Instance.SaveTime();
        HUDManager.Instance.ShowMenu();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DeathPlayer()
    {
        Color deadColor = new Color(1, 1, 1, 0);
        float startTime = Time.time;

        while (isDead)
        {
            render.color = Color.Lerp(render.color, deadColor, (Time.time - startTime) / 200f);
            if (render.color == deadColor)
            {
                break;
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }
    #endregion

    #region PLAYER_DATA
    public void SaveData()
    {
        PlayerHealthData data = new PlayerHealthData(currentHealth, maxHealth);
        JsonSaver.SaveObject(data);
    }

    public void LoadData()
    {
        PlayerHealthData data = JsonSaver.GetFile<PlayerHealthData>();
        currentHealth = data.currentHealth;
        maxHealth = data.maxHealth;
    }
    #endregion
}
