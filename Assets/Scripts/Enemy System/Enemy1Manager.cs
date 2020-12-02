using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Manager : MonoBehaviour
{
    private float enemyTimer = 0;
    private float nextAttack = 3.0f;
    private float animTimer = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        enemyTimer = nextAttack;
        
    }

    // Update is called once per frame
    void Update()
    {
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
