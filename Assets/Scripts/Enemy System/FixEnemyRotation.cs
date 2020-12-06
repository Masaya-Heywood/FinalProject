using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixEnemyRotation : MonoBehaviour
{
    Vector3 def;
    private Transform playerPos;

    void Awake()
    {
        playerPos = GameObject.Find("Player").transform;
        def = transform.localRotation.eulerAngles;
    }

    void LateUpdate()
    {


        Vector3 player = playerPos.transform.position;
        Vector3 enemy = this.transform.position;
        var enemyRot = this.transform.localRotation.eulerAngles;

        var rot = GetAngle(player,enemy) + 90;
        enemyRot.z = rot;

        transform.localRotation = Quaternion.Euler(enemyRot);

        //ロ


    }

    //function for getting the angle between two positions
    float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

}
