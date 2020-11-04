using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeaponController : MonoBehaviour
{
    //speed of the bullet
    private Vector3 speed;
    private Rigidbody2D rb;

    private Collider2D col;

    // Intitialize the bullet 
    public void Init(float angle, float speed)
    {
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<Collider2D>();

        // change the bullet angle to vector
        var direction = GetDirection(angle);

        // calculate the velocity from the direction and speed
        this.speed = direction * speed;

        // point the bullet to the direction
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        rb.velocity = this.speed;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            col.isTrigger = true;
            rb.velocity = Vector2.zero;
        }
    }


}


