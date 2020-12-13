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
    public AudioClip shakeSound;

    private AudioSource audioSource;

    private GameObject particlePrefab;

    public Image hpImage;

    private float endTimer = -1;
    private Animator anim;
    public GameObject hpBar;
    private bool endFlag = false;
    private bool startEnd = false;
    private bool startDeath = false;
    public GameObject[] destroyObjects;
    public GameObject mainCamera;

    public GameObject projectile;
    private float countP = 0;
    private float nextProjectile = 1.5f;
    public float projectileVelocity = 1.5f;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
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
        if (endFlag)
            return;

       
        if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 2.1f && !startDeath)
            {
                startDeath = true;
                anim.SetBool("death", true);
                audioSource.Stop();
                audioSource.PlayOneShot(hitSound);
            }

            if (endTimer <= 0)
            {
                
                
                endFlag = true;
                anim.SetBool("death", false);
                Destroy(this.gameObject);
            }
            
            return;
        }
        timer -= Time.deltaTime;
        countP += Time.deltaTime;
        if (countP >= nextProjectile)
        {
            nextProjectile = Random.Range(2, 5);
            countP = 0;
            var obj = Instantiate(projectile, this.transform.position, Quaternion.identity);
            var dir = GetAngle(this.transform.position, player.transform.position);
            var v = new Vector2(Mathf.Cos(dir / 180 * Mathf.PI), Mathf.Sin(dir / 180 * Mathf.PI));
            obj.GetComponent<Rigidbody2D>().velocity = v * projectileVelocity;
        }
        if (timer <= 0)
        {
            timer = spawnSpan;
            var spawnedEnemy = Instantiate(enemyType, transform.position, transform.rotation);
            spawnedEnemy.GetComponent<Unit>().target = player.GetComponent<Transform>();
            spawnedEnemy.GetComponent<Unit>().mySpawner = null;

        }
        if (health <= 0 && !startEnd)
        {
            startEnd = true;
            //SceneManager.LoadScene("EndScene");
            
            endTimer = 6.1f;
            playerCharacter.GetComponent<PlayerController>().defeatBoss();
            
            hpBar.SetActive(false);
            audioSource.PlayOneShot(shakeSound);
            mainCamera.GetComponent<CameraManager>().endShake();

            for (int i = 0; i < destroyObjects.Length; i++)
            {
                Destroy(destroyObjects[i]);
            }
            GameObject[] objects1 = GameObject.FindGameObjectsWithTag("enemy1");
            for (int i = 0; i < objects1.Length; i++)
            {
                objects1[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bulletNormal") || collision.gameObject.CompareTag("bulletPenetrate"))
        {
            //audioSource.PlayOneShot(hitSound);
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            hpImage.fillAmount -= collision.gameObject.GetComponent<BulletController>().bulletDamage / totalHealth;


            if (collision.gameObject.CompareTag("bulletNormal"))
                Destroy(collision.gameObject);

            var m = new Vector3(Mathf.Cos((collision.gameObject.transform.eulerAngles.z - 90) * Mathf.Deg2Rad),
                            Mathf.Sin((collision.gameObject.transform.eulerAngles.z - 90) * Mathf.Deg2Rad),
                            0);

            Instantiate(particlePrefab, 0.6f * m + this.gameObject.transform.position, Quaternion.identity);



            //die if health is 0
            if (health == 0)
            {
                Instantiate(((GameObject)Resources.Load("deadEnemy1")), this.transform.position, Quaternion.identity);
                playerCharacter.GetComponent<PlayerController>().defeatEnemy();
                //Destroy(gameObject);
                //player.health++;
            }
        }

        if (collision.gameObject.CompareTag("blade"))
        {
            audioSource.PlayOneShot(hitSound);
            health -= 1;
            hpImage.fillAmount -= 1 / totalHealth;
            Instantiate(particlePrefab, this.transform.position, Quaternion.identity);


        }

        if (collision.gameObject.CompareTag("bulletBounce"))
        {
            //audioSource.PlayOneShot(hitSound);
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            hpImage.fillAmount -= collision.gameObject.GetComponent<BulletController>().bulletDamage / totalHealth;

            Destroy(collision.gameObject);
            var m = new Vector3(Mathf.Cos((collision.gameObject.transform.eulerAngles.z - 90) * Mathf.Deg2Rad),
                            Mathf.Sin((collision.gameObject.transform.eulerAngles.z - 90) * Mathf.Deg2Rad),
                            0);

            Instantiate(particlePrefab, 0.6f * m + this.gameObject.transform.position, Quaternion.identity);




        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        

    }

    //function for getting the angle between two positions
    float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
}