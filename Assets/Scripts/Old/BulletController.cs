using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //speed of the bullet
    private Vector3 speed;
    private Rigidbody2D rb;
    public int bulletDamage = 1;

    // Intitialize the bullet 
    public void Init(float angle, float speed)
    {
        rb = this.GetComponent<Rigidbody2D>();

        // change the bullet angle to vector
        var direction = GetDirection(angle);

        // calculate the velocity from the direction and speed
        this.speed = direction * speed;

        // point the bullet to the direction
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        rb.velocity = this.speed;

        // destory after 2 seconds
        Destroy(gameObject, 2);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "bulletNormal" && collision.gameObject.tag == "wall")
            Destroy(this.gameObject);
    }


}


