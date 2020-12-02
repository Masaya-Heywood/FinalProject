using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Manager : MonoBehaviour
{

    private Transform player;
    private PlayerController playerControler;
    private hpManager hp;
    public float explodeDistance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        hp = GameObject.Find("hpFull").GetComponent<hpManager>();
        playerControler = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < explodeDistance) {
            Instantiate(((GameObject)Resources.Load("EnemyExplodeEffect")), this.transform.position, Quaternion.identity);
            playerControler.takeExplodeDamage();
            
            Destroy(this.gameObject);
        }
    }
}
