using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public int health = 3;
    public float timeAmount = .001f;
    private float timer;
    public bool tookDamage = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        timer = timeAmount;
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
            Destroy(gameObject);
         }
    }
}