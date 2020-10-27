using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public float projDmg = 1; //projectile damage to other objects
    public int projDura = 1; //projectile durability, its own health. Use this to instantly destroy bullets on contact with another object that. Different projectiles can have different durabilities, allowing us to make them pierce through walls and enemies and other features.
    public float timer = 5f;
    private void Update()
    {
        if (projDura <= 0)
        {
            Destroy(gameObject);
        }

        //timer, use this timer instead of the inbuilt timer for the destroy function incase case we want to anything else to also track time.
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //This function seems like it could be useful in the future
    public Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }
}
