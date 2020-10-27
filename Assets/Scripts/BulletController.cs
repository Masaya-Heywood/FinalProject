using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //speed of the bullet
    private Vector3 speed; 

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // 移動する
        transform.localPosition += speed;
    }

    // 弾を発射する時に初期化するための関数
    public void Init(float angle, float speed)
    {
        // change the bullet angle to vector
        var direction = GetDirection(angle);

        // calculate the velocity from the direction and speed
        this.speed = direction * speed;

        // point the bullet to the direction
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // destory after 2 seconds
        Destroy(gameObject, 2);
    }


    //cuntion to the the angle and return the vector value of it
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


