using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    private GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor");
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = pos + new Vector3(0,0,10);
    }
}
