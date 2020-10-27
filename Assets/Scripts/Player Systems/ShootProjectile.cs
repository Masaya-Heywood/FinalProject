using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public Rigidbody2D rb2DBullet;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //gameObject.GetComponent<AudioSource>().Play();

            GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

            newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
