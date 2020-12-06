using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossEnemyManager : MonoBehaviour
{

    private GameObject player;
    public GameObject enemyType;
    private float timer = 0;

    private float spawnSpan = 10;

    // Start is called before the first frame update
    public float health = 20;
    private float totalHealth;
    public float leapForce = 0;

    private Rigidbody2D rb2D; // { get; set; }

    private GameObject playerCharacter;

    public AudioClip hitSound;
    private AudioSource audioSource;

    private GameObject particlePrefab;

    public Image hpImage;

    private void Start()
    {
        player = GameObject.Find("Player");
        var spawnedEnemy = Instantiate(enemyType, transform.position, transform.rotation);
        spawnedEnemy.GetComponent<Unit>().target = player.GetComponent<Transform>();
        spawnedEnemy.GetComponent<Unit>().mySpawner = null;
        timer = spawnSpan;

        totalHealth = health;
        particlePrefab = ((GameObject)Resources.Load("BloodEffectEnemy"));
        audioSource = this.GetComponent<AudioSource>();

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerCharacter = GameObject.Find("Player");


    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnSpan;
            var spawnedEnemy = Instantiate(enemyType, transform.position, transform.rotation);
            spawnedEnemy.GetComponent<Unit>().target = player.GetComponent<Transform>();
            spawnedEnemy.GetComponent<Unit>().mySpawner = null;

        }
        if (health <= 0) {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bulletNormal") || collision.gameObject.CompareTag("bulletPenetrate"))
        {
            //audioSource.PlayOneShot(hitSound);
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            hpImage.fillAmount -= collision.gameObject.GetComponent<BulletController>().bulletDamage / totalHealth;
            Destroy(collision.gameObject);

            Debug.Log(Mathf.Cos((collision.gameObject.transform.eulerAngles.z)));

            var m = new Vector3(Mathf.Cos((collision.gameObject.transform.eulerAngles.z - 90) * Mathf.Deg2Rad),
                            Mathf.Sin((collision.gameObject.transform.eulerAngles.z - 90) * Mathf.Deg2Rad),
                            0);

            Instantiate(particlePrefab, 0.6f * m + this.gameObject.transform.position, Quaternion.identity);



            //die if health is 0
            if (health == 0)
            {
                Instantiate(((GameObject)Resources.Load("deadEnemy1")), this.transform.position, Quaternion.identity);
                playerCharacter.GetComponent<PlayerController>().defeatEnemy();
                Destroy(gameObject);
                //player.health++;
            }
        }

        if (collision.gameObject.CompareTag("blade"))
        {
            audioSource.PlayOneShot(hitSound);
            health -= 1;
            hpImage.fillAmount -= 1 / totalHealth;
            Instantiate(particlePrefab, this.transform.position, Quaternion.identity);



            //die if health is 0
            if (health == 0)
            {
                playerCharacter.GetComponent<PlayerController>().defeatEnemy();
                Destroy(gameObject);
                //player.health++;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bulletBounce"))
        {

            audioSource.PlayOneShot(hitSound);
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            Destroy(collision.gameObject);

            //die if health is 0
            if (health == 0)
            {
                playerCharacter.GetComponent<PlayerController>().defeatEnemy();
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