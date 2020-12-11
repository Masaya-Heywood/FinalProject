using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerController>().takeExplodeDamage();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "wall") {
            Destroy(this.gameObject);
        }
    }
}
