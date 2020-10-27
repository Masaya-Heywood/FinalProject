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

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = ((GameObject)Resources.Load("bullet")).GetComponent<BulletController>();
    }

    // Update is called once per frame
    void Update()
    {


        // move the player according to the key input
        if(Input.GetKey(KeyCode.A))
            transform.localPosition += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.localPosition += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            transform.localPosition += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.localPosition += new Vector3(0, -1, 0) * speed * Time.deltaTime;



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
        if (Input.GetMouseButtonDown(0) && (shotTimer > shotInterval))
        {

            //reset the timer
            shotTimer = 0;

            //shoot bullet
            ShootNWay(angle, shotAngleRange, shotSpeed, shotCount);

        }else shotTimer += Time.deltaTime;

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
