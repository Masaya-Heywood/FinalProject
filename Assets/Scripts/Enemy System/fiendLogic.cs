using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fiendLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 3;
    public float leapForce = 0;
    private Unit unitCalcs;
    private Rigidbody2D rb2D; // { get; set; }

    private GameObject playerCharacter;
    private PlayerController player;

    private void Start()
    {
        unitCalcs = gameObject.GetComponent<Unit>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerCharacter = GameObject.Find("Player");
        player = playerCharacter.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (unitCalcs.dist <= 2)
        {
            Leap();
        }
    }
    private void Leap()
    {
        rb2D.AddForce(unitCalcs.pathDirection * leapForce * Time.deltaTime, ForceMode2D.Force);
        //rb2D.velocity = unitCalcs.target.position * (unitCalcs.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bulletNormal") || collision.gameObject.CompareTag("bulletPenetrate"))
        {
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            Destroy(collision.gameObject);

            //die if health is 0
            if (health == 0)
            {
                Destroy(gameObject);
                //player.health++;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bulletBounce"))
        {
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            Destroy(collision.gameObject);

            //die if health is 0
            if (health == 0)
            {
                Destroy(gameObject);
                //player.health++;
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerController>().health--;
            //print(collision.gameObject.GetComponent<PlayerController>().health);
        }
    }
}