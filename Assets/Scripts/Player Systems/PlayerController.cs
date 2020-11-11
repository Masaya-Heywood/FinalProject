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

    //animation for knife
    private Animator animKnife;


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
    private int collectNum = 3;
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

    // Start is called before the first frame update
    void Start()
    {
        
        bulletPrefab = ((GameObject)Resources.Load("bulletNormal")).GetComponent<BulletController>();
        rb = this.GetComponent<Rigidbody2D>();
        animKnife = (GameObject.Find("sampleKnife")).GetComponent<Animator>();
        knife = GameObject.Find("sampleKnife");
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
        transLowerBody.position = this.transform.position;
        //Debug for text system
        if (Input.GetKeyDown(KeyCode.T)) {
            //display dialogue
            GameObject.Find("SampleDialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            GameObject.Find("DialogueBox").GetComponent<RectTransform>().localPosition = new Vector3(-16, -178, 0);
        }
        if (Input.GetKeyDown(KeyCode.N)) {
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
            highLight[weaponNum - 1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasWeapon[1])
        {

            bulletPrefab = ((GameObject)Resources.Load("bulletPenetrate")).GetComponent<BulletController>();
            throwWeaponPrefab = ((GameObject)Resources.Load("throwWeapon2")).GetComponent<ThrowWeaponController>();
            knife.SetActive(false);
            weaponNum = 2;
            eraseHighLight();
            highLight[weaponNum - 1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasWeapon[2])
        {
            bulletPrefab = ((GameObject)Resources.Load("bulletBounce")).GetComponent<BulletController>();
            throwWeaponPrefab = ((GameObject)Resources.Load("throwWeapon3")).GetComponent<ThrowWeaponController>();
            knife.SetActive(false);
            weaponNum = 3;
            eraseHighLight();
            highLight[weaponNum - 1].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && hasWeapon[3])
        {
            throwWeaponPrefab = ((GameObject)Resources.Load("throwWeapon4")).GetComponent<ThrowWeaponController>();
            knife.SetActive(true);
            weaponNum = 4;
            eraseHighLight();
            highLight[weaponNum - 1].SetActive(true);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            spriteLowerBody.color = new Color(0, 0, 0, 1);
        }
        else
        {
            spriteLowerBody.color = new Color(0, 0, 0, 0);
        }

        //player movement with veolocity
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) { 
            rb.velocity = new Vector3(speed, speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 315);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) { 
            rb.velocity = new Vector3(-speed, -speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) { 
            rb.velocity = new Vector3(speed, -speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 225);
        }
        else if (Input.GetKey(KeyCode.A)) { 
            rb.velocity = new Vector3(-speed, 0, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.D)) { 
            rb.velocity = new Vector3(speed, 0, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetKey(KeyCode.W)) { 
            rb.velocity = new Vector3(0, speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S)) { 
            rb.velocity = new Vector3(0, -speed, 0);
            transLowerBody.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKeyUp(KeyCode.A)) 
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        else if (Input.GetKeyUp(KeyCode.D))  
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        else if (Input.GetKeyUp(KeyCode.W))
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        else if (Input.GetKeyUp(KeyCode.S))
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);





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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get the weapons
        if (collision.tag == "itemWeapon2") {
            spriteWeapon[1].SetActive(true);
            hasWeapon[1] = true;
            Destroy(collision.gameObject);
            ammoWeapon[1].SetActive(true);
        }
        if (collision.tag == "itemWeapon3")
        {
            spriteWeapon[2].SetActive(true);
            hasWeapon[2] = true;
            Destroy(collision.gameObject);
            ammoWeapon[2].SetActive(true);
        }
        if (collision.tag == "itemWeapon4")
        {
            spriteWeapon[3].SetActive(true);
            hasWeapon[3] = true;
            Destroy(collision.gameObject);
            ammoWeapon[3].SetActive(true);
        }

        if (hasWeapon[0] && collision.tag == "ammo1") {
            if(ammoNum[0] + collectNum < maxAmmo)
                ammoNum[0] += collectNum;
            ammoNumText[0].text = ammoNum[0].ToString();
            Destroy(collision.gameObject);
        }
        if (hasWeapon[1] && collision.tag == "ammo2")
        {
            if (ammoNum[2] + collectNum < maxAmmo)
                ammoNum[1] += collectNum;
            ammoNumText[1].text = ammoNum[1].ToString();
            Destroy(collision.gameObject);
        }
        if (hasWeapon[2] && collision.tag == "ammo3")
        {
            if (ammoNum[2] + collectNum < maxAmmo)
                ammoNum[2] += collectNum;
            ammoNumText[2].text = ammoNum[2].ToString();
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


}
