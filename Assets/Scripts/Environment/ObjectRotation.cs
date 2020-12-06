using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    float rotateTime = 0;
    float changeSpeed = .01f;
    int rotateAmount = 40;
    //float scaleAmount = something;

    private void Update()
    {

        if (rotateTime <= changeSpeed)
        {
            rotateTime += Time.deltaTime;
        }
        else if (rotateTime >= changeSpeed)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, rotateAmount * Time.deltaTime));



            //Portion of script for Cassie to write. These lines of code should scale the object up and down continuously.
            //if (game object scale > a certainAmount)
            //{
            //    game object scale -= new Vector3(something, something, something)
            //} else if (game object scale  < a certainAmount)
            //{
            //    game object scale += new Vector3(something, something, something);
            //}
            rotateTime = 0;
        }

    }
}