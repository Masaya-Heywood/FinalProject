using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //player's movement speed
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // get the key input information
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // move the player according to the key input
        var velocity = new Vector3(h, v) * speed;
        transform.localPosition += velocity*Time.deltaTime;

        //Rotate player according to the mouse position
        var pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
        transform.localRotation = rotation;


    }
}
