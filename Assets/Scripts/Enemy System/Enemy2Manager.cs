using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Manager : MonoBehaviour
{

    private Transform player;
    private PlayerController playerControler;
    private hpManager hp;
    public float explodeDistance = 3.0f;
    private EnemyNumManager enemyNumManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        hp = GameObject.Find("hpFull").GetComponent<hpManager>();
        playerControler = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyNumManager = GameObject.Find("EnemyNumManager").GetComponent<EnemyNumManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.x - this.transform.position.x) > 15)
        {
            enemyNumManager.decreaseEnemyNum();
            Destroy(this.gameObject);
        }
        if (Mathf.Abs(player.position.y - this.transform.position.y) > 15)
        {
            enemyNumManager.decreaseEnemyNum();
            Destroy(this.gameObject);
        }
        if (Vector3.Distance(player.position, this.transform.position) < explodeDistance) {
            //Instantiate(((GameObject)Resources.Load("EnemyExplodeEffect")), this.transform.position, Quaternion.identity);
            Instantiate(((GameObject)Resources.Load("Enemy2Dead")), this.transform.position, Quaternion.identity);
            playerControler.takeExplodeDamage();
            enemyNumManager.decreaseEnemyNum();
            Destroy(this.gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Instantiate(((GameObject)Resources.Load("Enemy2Dead")), this.transform.position, Quaternion.identity);
            playerControler.takeExplodeDamage();

            Destroy(this.gameObject);
        }
    }
}
