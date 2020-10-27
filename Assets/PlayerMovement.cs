using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer thisSprite;
    public Rigidbody2D rb2D;
    public Vector3 mousePosition = new Vector2(0, 0);
    private Vector3 mouseDirection = new Vector2(0, 0);
    private Vector3 constantMousePosition = new Vector2(0, 0);
    public float initalSpeed = 200f;
    public float constantSpeed = 50f;
    private float actualInitialSpeed = 0f;
    //private bool movement = false;


    // Start is called before the first frame update
    void Start()
    {
        actualInitialSpeed = initalSpeed - constantSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Inital Movement -- We can edit this movement to be on pressing the WASD keys instead of mouse button. We wouldn't need a mouseDirection if we do.
        if (Input.GetMouseButton(1))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseDirection = (mousePosition - transform.position).normalized;
            //rb2D.AddForce(mouseDirection * actualInitialSpeed);
            rb2D.velocity = mouseDirection * (actualInitialSpeed * Time.deltaTime); //change this because players can keep spam clicking for more speed than holding?
        }


        // follow up movement
        float dist = Vector3.Distance(mousePosition, transform.position);
        if (dist > 2)
        {
            Vector3 dir = (mousePosition - transform.position).normalized;
            rb2D.velocity = dir * (constantSpeed * Time.deltaTime);
        }
        else if (dist <= .5)
        {
            rb2D.velocity = Vector3.zero;
        }
        //Debug.Log(dist);


        //Movement Execution



        //Rotation

        constantMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 diff = constantMousePosition - transform.position;
        diff.Normalize();

        float RotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotationZ - 90);

        //Movement
    }
}