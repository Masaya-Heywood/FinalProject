using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    float rotateTime = 0;
    float changeSpeed = .25f;
    int rotateAmount = 40;
    float scaleAmount = 0.9f;
    

    private void Update()
    {

        if (rotateTime <= changeSpeed)
        {
            rotateTime += Time.deltaTime;
        }
        else if (rotateTime >= changeSpeed)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, rotateAmount * Time.deltaTime));



            //written by cassie
            //scale the object up and down continuously.
            if(gameObject.transform.localScale.x > scaleAmount)
            {
                gameObject.transform.localScale -= new Vector3(scaleAmount, scaleAmount, 0);
            }else if(gameObject.transform.localScale.x < scaleAmount)
                {
                gameObject.transform.localScale += new Vector3(scaleAmount, scaleAmount, 0);
                }
            rotateTime = 0;
        }
    }
}
