using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHealth : MonoBehaviour
{
    public int health = 3;
    public float timeAmount = .001f;
    private float timer;
    public bool tookDamage = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    public AudioClip hitSound;
    public Image hpImage;
    public GameObject hpBarEmpty;
    private int maxHealth;

    private void Start()
    {
        timer = timeAmount;
        maxHealth = health;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       

    }
    private void Update()
    {
        if (tookDamage == true)
        {
            if (timer > 0)
            {
                spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
                timer -= Time.deltaTime;
            } else if (timer < 0)
            {
                
                spriteRenderer.color = new Color(1f, 1, 1f, 1f);
                tookDamage = false;
                timer = timeAmount;
            }
        }
        if (health == 0)
        {
            if (hpBarEmpty != null)
                Destroy(hpBarEmpty);
            GameObject.Find("Player").GetComponent<PlayerController>().playBreakSound();
            Destroy(gameObject);
         }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "blade")
        {
            audioSource.PlayOneShot(hitSound);
            health -= 1;
            if(hpImage != null)
                hpImage.fillAmount -= 1.0f / maxHealth;
            tookDamage = true;
        }

        if (collision.gameObject.tag == "bulletNormal" || collision.gameObject.tag == "bulletBounce" || collision.gameObject.tag == "bulletPenetrate") {

            if (hpImage != null)
                hpImage.fillAmount -= 1.0f / maxHealth;
            audioSource.PlayOneShot(hitSound);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bulletBounce")
        {

            audioSource.PlayOneShot(hitSound);
            health -= 1;
            if (hpImage != null)
                hpImage.fillAmount -= 1.0f / maxHealth;
            tookDamage = true;

        }
    }
}