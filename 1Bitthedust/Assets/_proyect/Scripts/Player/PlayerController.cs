using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer render;

    public float speed;
    float currentSpeed;
    public float jumpSpeed;

    float horizontalInputValue;
    float verticalInputValue;
    float velocityLastFrame;

    bool isFacingRight;
    bool isWalking;
    bool IsJumping
    {
        get
        {
            return inputJump || fallJump;
        }
    }
    bool inputJump;
    bool fallJump;

    bool isClimbing;
    float distance = 1f;
    public LayerMask isLadder;

    public GameObject walkPrefab;
    private ParticleSystem walkParticles;

    public AudioSource walkAudio;
    public AudioSource hitAudio;
    float originalPitch;
    float pitchRange = 0.1f;
    bool playSound;

    bool getHit;

    public void Awake()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();

        isFacingRight = !render.flipX;
        currentSpeed = speed;

        walkParticles = Instantiate(walkPrefab).GetComponent<ParticleSystem>();
        walkParticles.transform.SetParent(transform);
        walkParticles.gameObject.SetActive(false);

    }

    #region PLAYER_PHYSX
    public void Update()
    {
        PlayerJumping();
        PlayerMovement();
        PlayerAudio();

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", IsJumping);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, isLadder);
        if (hitInfo.collider != null)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
            if (horizontalInputValue > 0 || horizontalInputValue < 0)
            {
                isClimbing = false;
            }
        }

        if (isClimbing == true && hitInfo.collider != null)
        {
            verticalInputValue = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, 0, speed), 0);
            rb.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    public void FixedUpdate()
    {
        render.flipX = !isFacingRight;
        walkParticles.transform.position = new Vector2(transform.position.x, transform.position.y - 0.05f);        
    }
    #endregion

    #region PLAYER_MOVEMENT
    public void PlayerMovement()
    {
        var particleVel = walkParticles.velocityOverLifetime;
        horizontalInputValue = Input.GetAxis("Horizontal");
       
        if (horizontalInputValue > 0)
        {
            rb.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
            isFacingRight = true;
            if (!isWalking && !playSound)
            {
                playSound = true;
            }
            isWalking = true;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, 0, speed), rb.velocity.y, 0);

            walkParticles.gameObject.SetActive(true);
            particleVel.speedModifier = 1;
           
        }

        if (horizontalInputValue < 0)
        {
            rb.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
            isFacingRight = false;
            if (!isWalking && !playSound)
            {
                playSound = true;
            }
            isWalking = true;
            rb.velocity = new Vector3(Mathf.Clamp(-rb.velocity.x, 0, -speed), rb.velocity.y, 0);

            walkParticles.gameObject.SetActive(true);
            particleVel.speedModifier = -1;
        }

        if (horizontalInputValue == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            if (isWalking && !playSound)
            {
                playSound = true;
            }
            isWalking = false;

            walkParticles.gameObject.SetActive(false);
        }
    }
    #endregion

    #region PLAYER_JUMP
    public void PlayerJumping()
    {
       
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            velocityLastFrame = rb.velocity.y;
            if (!IsJumping && !playSound)
            {
                playSound = true;
            }
            inputJump = true;
        }
       
        if (inputJump)
        {
            if(velocityLastFrame < rb.velocity.y)
            {
                if (IsJumping && !playSound)
                {
                    playSound = true;
                }
                inputJump = false;
            }
            velocityLastFrame = rb.velocity.y;
        }


        if (Mathf.Abs(rb.velocity.y) > 0.5f)
        {
            if (!IsJumping && !playSound)
            {
                playSound = true;
            }
            fallJump = true;
        }
        else
        {
            if (IsJumping && !playSound)
            {
                playSound = true;
            }
            fallJump = false;
        }
    }
    #endregion

    #region PLAYER_COLLISIONS
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            var enemyForce = other.gameObject.GetComponent<EnemyController>().force;

            Vector2 distance = new Vector2(0, 0.05f);

            if(other.collider == enemyHealth.hitCollider)
            {
                if (!getHit && !playSound)
                {
                    playSound = true;
                }
                getHit = true;
                Vector2 hitDirection = other.contacts[0].normal + distance;
                rb.AddForce(hitDirection * enemyForce, ForceMode2D.Impulse);
            }
            else if(other.collider == enemyHealth.deathCollider)
            {
                return;
            }
        }
    }
    #endregion

    #region PLAYER_AUDIO
    public void PlayerAudio()
    {
        if (playSound)
        {
            playSound = false;
            if (isWalking)
            {
                walkAudio.Play();
            }
            else
            {
                walkAudio.Stop();
            }

            if (IsJumping)
            {
                walkAudio.Stop();
            }

            if (getHit)
            {
                originalPitch = hitAudio.pitch;
                hitAudio.pitch = UnityEngine.Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                hitAudio.Play();
                getHit = false;
            }
        }
    }
    #endregion
}
