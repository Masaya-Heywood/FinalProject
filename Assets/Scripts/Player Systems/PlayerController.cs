using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //player's movement speed
    public float speed = 3.0f;



    //variables for bullet

    // bullet prefab
    private BulletController bulletPrefab; 
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

    //if the player is using knife
    private bool useKnife = false;

    //if the player have weapons
    private bool hasWeapon1 = true;
    private bool hasWeapon2 = false;
    private bool hasWeapon3 = false;
    private bool hasWeapon4 = false;

    //sprites in the item slot
    private GameObject spriteWeapon1;
    private GameObject spriteWeapon2;
    private GameObject spriteWeapon3;
    private GameObject spriteWeapon4;

    //the knife object
    private GameObject knife;

    //dialogue manager
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
        bulletPrefab = ((GameObject)Resources.Load("bulletNormal")).GetComponent<BulletController>();
        rb = this.GetComponent<Rigidbody2D>();
        animKnife = (GameObject.Find("sampleKnife")).GetComponent<Animator>();
        knife = GameObject.Find("sampleKnife");
        knife.SetActive(false);
        dialogueManager = GameObject.Find("Dialogue").GetComponent<DialogueManager>();

        spriteWeapon1 = GameObject.Find("slotWeapon1");
        spriteWeapon2 = GameObject.Find("slotWeapon2");
        spriteWeapon2.SetActive(false);
        spriteWeapon3 = GameObject.Find("slotWeapon3");
        spriteWeapon3.SetActive(false);
        spriteWeapon4 = GameObject.Find("slotWeapon4");
        spriteWeapon4.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasWeapon1)
        {
            bulletPrefab = ((GameObject)Resources.Load("bulletNormal")).GetComponent<BulletController>();
            useKnife = false;
            knife.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hasWeapon2)
        {

            bulletPrefab = ((GameObject)Resources.Load("bulletPenetrate")).GetComponent<BulletController>();
            useKnife = false;
            knife.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hasWeapon3)
        {
            bulletPrefab = ((GameObject)Resources.Load("bulletBounce")).GetComponent<BulletController>();
            useKnife = false;
            knife.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && hasWeapon4)
        {
            useKnife = true;
            knife.SetActive(true);
        }



        //player movement with veolocity
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            rb.velocity = new Vector3(-speed, speed, 0);
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            rb.velocity = new Vector3(speed, speed, 0);
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(-speed, -speed, 0);
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            rb.velocity = new Vector3(speed, -speed, 0);

        else if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector3(-speed, 0, 0);
        else if (Input.GetKey(KeyCode.D))
            rb.velocity = new Vector3(speed, 0, 0);
        else if (Input.GetKey(KeyCode.W))
            rb.velocity = new Vector3(0, speed, 0);
        else if (Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(0, -speed, 0);

        else if(Input.GetKeyUp(KeyCode.A))
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
            if (useKnife)
            {
                //start knife animation
                animKnife.SetTrigger("onClick");
            }
            else
            {
                if (shotTimer > shotInterval)
                {

                    //reset the timer
                    shotTimer = 0;

                    //shoot bullet
                    ShootNWay(angle, shotAngleRange, shotSpeed, shotCount);

                }
            }

        }else shotTimer += Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get the weapons
        if (collision.tag == "itemWeapon2") {
            spriteWeapon2.SetActive(true);
            hasWeapon2 = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "itemWeapon3")
        {
            spriteWeapon3.SetActive(true);
            hasWeapon3 = true;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "itemWeapon4")
        {
            spriteWeapon4.SetActive(true);
            hasWeapon4 = true;
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

    
    
}
