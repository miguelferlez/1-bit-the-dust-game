using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    SpriteRenderer render;

    public GameObject chestCoin;
    public int coinsIndex;



    bool isDead;

    public void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        isDead = false;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 8 && !isDead)
        {
            isDead = true;

            InstanceCoins();

            StartCoroutine(ChangeColor());
        }
    }

    IEnumerator ChangeColor()
    {
        Color nullColor = new Color(1, 1, 1, 0);
        while (isDead)
        {
            render.color = Color.Lerp(render.color, nullColor, 0.05f);
            if (render.color == nullColor)
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    public void InstanceCoins()
    {       
        for (int i = 0; i < coinsIndex; i++)
        {
            float speed = Mathf.Lerp(2f, 3.5f, Random.value);
            Vector3 distance = new Vector3(0f, 0.2f, 0f);
            Vector3 force = Vector3.up * speed;
            float angle = Mathf.Lerp(-5, 5, Random.value);

            force = Quaternion.Euler(0, 0, angle) * force;

            var newCoin = Instantiate(chestCoin);
            newCoin.transform.position = transform.position + distance;
            newCoin.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }

    }
}
