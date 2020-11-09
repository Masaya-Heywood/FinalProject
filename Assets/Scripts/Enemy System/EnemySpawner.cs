using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int maxEnemies = 2;
    public int currentEnemies = 0;
    public GameObject player;
    public GameObject enemyType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentEnemies < maxEnemies && collision.gameObject == player)
        {
            var spawnedEnemy = Instantiate(enemyType, transform.position, transform.rotation);
            spawnedEnemy.GetComponent<Unit>().target = player.GetComponent<Transform>();
            spawnedEnemy.GetComponent<Unit>().mySpawner = gameObject;
            currentEnemies++;
        }
    }
}