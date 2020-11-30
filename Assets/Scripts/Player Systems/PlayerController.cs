using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    //player's movement speed
    public float speed = 3.0f;



    //variables for bullet

    // bullet prefab
    private BulletController bulletPrefab;

    // throw weapon prefab
    private ThrowWeaponController throwWeaponPrefab;
    // the speed of the bullet
    public float shotSpeed; 
    //the angle for the bullet when you are shooting multiple
    //bullets at once
    public float shotAngleRange; 
    //timer for counting interval
    private float shotTimer; 
    //the number of bullets you can shoot at once
    public int shotCount;
    //the interval you can shoot the bullets
    public float shotInterval;

    //player's rigid body2d
    private Rigidbody2D rb;

    //player's animator
    public Animator animator;

    //animation for knife
    private Animator animKnife;

    private float mouseAngle;

    //if the player have weapons
    private bool[] hasWeapon = new bool[4];

    //sprites in the item slot
    private GameObject[] spriteWeapon = new GameObject[4];


    //variables for ammo system
    //ammo text
    private Text[] ammoNumText = new Text[3];
    //highLight
    private GameObject[] highLight = new GameObject[4];
    //ammo num
    private int[] ammoNum = new int[3];
    //the ammo you can collect
    private int collectNum = 6;
    //the max ammo you can have
    private int maxAmmo = 36;
    //weapon in ammo slot
    private GameObject[] ammoWeapon = new GameObject[4];

    //the knife object
    private GameObject knife;

    //the weapon which is currently used
    private int weaponNum = 1;

    //dialogue manager
    private DialogueManager dialogueManager;

    //lower body transform
    private Transform transLowerBody;
    private SpriteRenderer spriteLowerBody;

    private hpManager hitPointManager;

    private float enemyForce = 8.0f;
    private float hitEnemyTimer = 0;

    private float weaponAnimCount = 0;

    private AudioSource audioSource;
    public AudioClip ammoSound;
    public AudioClip shootSound1;
    public AudioClip getWeapon;
    public AudioClip enemyAtackStound;
    public AudioClip openDoor;
    private CameraShake mainCamera;

    public GameObject clearCanvas;

    private GameObject particlePrefab;

    private int defeatEnemyCount = 0;

    private Text enemyCountText;

    private bool talkDoor = false;

    private bool clearGame = false;
    // Start is called before the first frame update
    void Start()
    {
 
        enemyCountText = GameObject.Find("EnemyDefeatText").GetComponent<Text>();
        particlePrefab = ((GameObject)Resources.Load("BloodEffect"));

        audioSource = this.GetComponent<AudioSource>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        hitPointManager = GameObject.Find("hpFull").GetComponent<hpManager>();
        bulletPrefab = ((GameObject)Resources.Load("bulletNormal")).GetComponent<BulletController>();
        rb = this.GetComponent<Rigidbody2D>();
        animKnife = (GameObject.Find("sampleKnife")).GetComponent<Animator>();
        knife = GameObject.Find("sampleKnife");
        //animator = GameObject.Find("Dialogue").GetComponent<DialogueManager>();
        knife.SetActive(false);
        dialogueManager = GameObject.Find("Dialogue").GetComponent<DialogueManager>();
        transLowerBody = GameObject.Find("lowerBody").transform;
        spriteLowerBody = GameObject.Find("lowerBody").GetComponent<SpriteRenderer>();

        hasWeapon[0] = true;
        for (int i = 0; i < 4; i++) {
            ammoWeapon[i] = GameObject.Find("ammoWeapon" + (i + 1));

            spriteWeapon[i] = GameObject.Find("slotWeapon" + (i+1));
            highLight[i] = GameObject.Find("highLight" + (i + 1));
            if (i != 0)
            {
                spriteWeapon[i].SetActive(false);
                highLight[i].SetActive(false);
                
            }

            if (i != 3)
            {
                //initializing ammo variables
                ammoNumText[i] = GameObject.Find("numText" + (i + 1)).GetComponent<Text>();
                ammoNum[i] = 0;
            }

            
        }
        for (int i = 0; i < 3; i++)
            ammoWeapon[i+1].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (clearGame)
            return;

        transLowerBody.position = this.transform.position;
        if (weaponAnimCount > 0)
        {
            weaponAnimCount -= Time.deltaTime;
            if (weaponAnimCount <= 0)
            {
                animator.SetBool("hasPistol", false);
                //Debug.Log("has poistol is false");
            }
        }

        if (hitEnemyTimer > 0)
        {
            hitEnemyTimer -= Time.deltaTime;

            if (hitEnemyTimer <= 0)
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 0.7f, 0.7f);
            }

            return;
        }

        
        if (Input.GetKeyDown(KeyCode.Space)) {
            //display next sentence
            dialogueManager.DisplayNextSentence();
        }
        //delete the code above after testing the dialogue system


        //changing weapons
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasWeapon[0])
        {
            bulletPrefab = ((GameObject)Resources.Load("bulletNormal")).GetComponent<BulletController>();
            knife.SetActive(false);            
            weaponNum = 1;
            eraseHighLight();
            audioSource.PlayOneShot(getWeapon);
            highLight[weaponNum - 1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasWeapon[1])
        {

            bulletPrefab = ((GameObject)Resources.Load("bulletPenetrate")).GetComponent<BulletController>();
            throwWeaponPrefab = ((GameObject)Resources.Load("throwWeapon2")).GetComponent<ThrowWeaponController>();
            knife.SetActive(false);
            weaponNum = 2;
            eraseHighLight();
            audioSource.PlayOneShot(getWeapon);
            highLight[weaponNum - 1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasWeapon[2])
        {
            bulletPrefab = ((GameObject)Resources.Load("bulletBounce")).GetComponent<BulletController>();
            throwWeaponPrefab = ((GameObject)Resources.Load("throwWeapon3")).GetComponent<ThrowWeaponController>();
            knife.SetActive(false);
            weaponNum = 3;
            eraseHighLight();
            audioSource.PlayOneShot(getWeapon);
            highLight[weaponNum - 1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && hasWeapon[3])
        {
            throwWeaponPrefab = ((GameObject)Resources.Load("throwWeapon4")).GetComponent<ThrowWeaponController>();
            knife.SetActive(true);
            weaponNum = 4;
            eraseHighLight();
            audioSource.PlayOneShot(getWeapon);
            highLight[weaponNum - 1].SetActive(true);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            spriteLowerBody.color = new Color(0, 0, 0, 1);
        }
        else
        {
            spriteLowerBody.color = new Color(0, 0, 0, 0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }

        //player movement with veolocity
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 45);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 315);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(-speed, -speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 135);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, -speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 225);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, 0, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 90);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, 0, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 270);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0, -speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 180);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            animator.SetBool("walking", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("walking", false);
        }




        //Rotate player according to the mouse position

        //calculate the screen position of the player
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // calculate the mouse direction accordint to the player
        var direction = Input.mousePosition - screenPos;

        // get the angle of where the mouse cursor is
        var angle = GetAngle(Vector3.zero, direction);

        

        // rotate the player to where the mouse cursor is at
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
        mouseAngle = angle;




        //shooting bullets
        if (Input.GetMouseButtonDown(0))
        {
            if (weaponNum == 4)
            {
                //start knife animation
                animKnife.SetTrigger("onClick");
            }
            else
            {
                if (ammoNum[weaponNum-1] >= shotCount &&  shotTimer > shotInterval)
                {
                    audioSource.PlayOneShot(shootSound1);
                    mainCamera.Shake(0.25f,0.1f);

                    animator.SetBool("hasPistol", true);
                    weaponAnimCount = 0.5f;
                    ammoNum[weaponNum - 1] -= shotCount;
                    ammoNumText[weaponNum - 1].text = ammoNum[weaponNum - 1].ToString();

                    //reset the timer
                    shotTimer = 0;

                    //shoot bullet
                    ShootNWay(angle, shotAngleRange, shotSpeed, shotCount);

                }
            }

        }else shotTimer += Time.deltaTime;

        //throw weapon
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowWeapon(angle, shotAngleRange, shotSpeed);

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //take damage from enemy
        if (collision.gameObject.tag == "enemy1")
        {
            if (clearGame)
                return;
            audioSource.PlayOneShot(enemyAtackStound);            
            hitPointManager.takeDamage(0.2f);
            float f = GetAngle(collision.gameObject.transform.position, this.transform.position);

            //rb.AddForce(GetDirection(f)*enemyForce, ForceMode2D.Impulse);
            rb.velocity = GetDirection(f) * enemyForce;

            hitEnemyTimer = 0.3f;
            
            Invoke("startParticle", 0.3f);
        }

       
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "textDoor")
        {
            
            //Debug for text system
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (defeatEnemyCount >= 5) {
                    clearCanvas.SetActive(true);
                    talkDoor = true;
                    audioSource.PlayOneShot(openDoor);
                    clearGame = true;
                }
                //display dialogue
                if (!talkDoor)
                {
                    GameObject.Find("DoorDialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
                    GameObject.Find("DialogueBox").GetComponent<RectTransform>().localPosition = new Vector3(-16, -178, 0);
                    talkDoor = true;
                }


            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //get the weapons
        if (collision.tag == "itemWeapon2") {
            spriteWeapon[1].SetActive(true);
            hasWeapon[1] = true;
            Destroy(collision.gameObject);
            ammoWeapon[1].SetActive(true);
            audioSource.PlayOneShot(getWeapon);
        }
        if (collision.tag == "itemWeapon3")
        {
            spriteWeapon[2].SetActive(true);
            hasWeapon[2] = true;
            Destroy(collision.gameObject);
            ammoWeapon[2].SetActive(true);
            audioSource.PlayOneShot(getWeapon);
        }
        if (collision.tag == "itemWeapon4")
        {
            spriteWeapon[3].SetActive(true);
            hasWeapon[3] = true;
            Destroy(collision.gameObject);
            ammoWeapon[3].SetActive(true);
            audioSource.PlayOneShot(getWeapon);
        }

        if (hasWeapon[0] && collision.tag == "ammo1") {

            if(ammoNum[0] + collectNum < maxAmmo)
                ammoNum[0] += collectNum;

            ammoNumText[0].text = ammoNum[0].ToString();
            audioSource.PlayOneShot(ammoSound);
            Destroy(collision.gameObject);

        }
        if (hasWeapon[1] && collision.tag == "ammo2")
        {
            if (ammoNum[2] + collectNum < maxAmmo)
                ammoNum[1] += collectNum;

            ammoNumText[1].text = ammoNum[1].ToString();
            audioSource.PlayOneShot(ammoSound);
            Destroy(collision.gameObject);
        }
        if (hasWeapon[2] && collision.tag == "ammo3")
        {
            if (ammoNum[2] + collectNum < maxAmmo)
                ammoNum[2] += collectNum;

            ammoNumText[2].text = ammoNum[2].ToString();
            audioSource.PlayOneShot(ammoSound);
            Destroy(collision.gameObject);
        }
    }


    //function for getting the angle between two positions
    float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    private void startParticle()
    {
        Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
    }

    private void ShootNWay(float angleBase, float angleRange, float speed, int count)
    {
        //player position
        var pos = transform.localPosition; 
        //player rotation
        var rot = transform.localRotation; 

        // when you are shooting multiple bullets
        if (1 < count)
        {
            // loop the same time as the number of bullets
            for (int i = 0; i < count; ++i)
            {
                // calculate the bullent angle
                var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);

                pos += new Vector3(0.0f, 0.0f, 0);
                // instantiate the bullet
                var shot = Instantiate(bulletPrefab, pos, rot);

                // set the bullet angle and speed
                shot.Init(angle, speed);
            }
        }
        // when you are shooting only one bullets
        else if (count == 1)
        {
            // instantiate the bullet
            var shot = Instantiate(bulletPrefab, pos, rot);

            // set the bullet angle and speed
            shot.Init(angleBase, speed);
        }
    }

    private void ThrowWeapon(float angleBase, float angleRange, float speed)
    {
        //return if you don't have the weapon
        if (!hasWeapon[weaponNum - 1] || weaponNum == 1) return;

        
        //player position
        var pos = transform.localPosition;
        //player rotation
        var rot = transform.localRotation;
        
            // instantiate the bullet
        var shot = Instantiate(throwWeaponPrefab, pos, rot);

            // set the bullet angle and speed
        shot.Init(angleBase, speed);

        

        //reset the variables for thrown weapon
        knife.SetActive(false);
        spriteWeapon[weaponNum - 1].SetActive(false);

        //change to default weapon
        weaponNum = 1;

    }

    private void eraseHighLight()
    {
        for (int i = 0; i < 4; i++)
            highLight[i].SetActive(false);
    }

    public float getMouseAngle()
    {
        return mouseAngle;
    }


    //function to the the angle and return the vector value of it
    public Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }

    public void defeatEnemy() {
        defeatEnemyCount++;
        enemyCountText.text = defeatEnemyCount.ToString();

    }

}
