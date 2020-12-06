using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Manager : MonoBehaviour
{
    private float enemyTimer = 0;
    private float nextAttack = 3.0f;
    private float animTimer = 0;
    private Animator anim;
    private Transform playerPos;
    private EnemyNumManager enemyNumManager;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        anim = this.GetComponent<Animator>();
        enemyTimer = nextAttack;
        enemyNumManager = GameObject.Find("EnemyNumManager").GetComponent<EnemyNumManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Abs(playerPos.position.x - this.transform.position.x));
        if (Mathf.Abs(playerPos.position.x - this.transform.position.x) > 15) {
            enemyNumManager.decreaseEnemyNum();
            Destroy(this.gameObject);
        }
        if (Mathf.Abs(playerPos.position.y - this.transform.position.y) > 15)

        {
            enemyNumManager.decreaseEnemyNum();
            Destroy(this.gameObject);
        }
        if (animTimer > 0) {
            animTimer -= Time.deltaTime;
            if (animTimer <= 0)
                anim.SetBool("isAttack", false);
        }

        if (enemyTimer > 0) {
            enemyTimer -= Time.deltaTime;
            if (enemyTimer <= 0) {
                anim.SetBool("isAttack", true);
                enemyTimer = Random.Range(2.0f, 5.0f);
                animTimer = 1.0f;
            }
        }
    }
}
