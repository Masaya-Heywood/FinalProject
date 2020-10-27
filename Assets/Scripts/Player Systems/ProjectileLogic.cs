using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float projDmg = 1; //projectile damage to other objects
    public int projDura = 1; //projectile durability, its own health
    public float time = 5f;
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
