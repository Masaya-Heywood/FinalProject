using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int maxEnemies = 2;
    public int currentEnemies = 0;
    private GameObject player;
    public GameObject enemyType;
    public GameObject visualInput;
    private bool inputFilled = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        if (visualInput != null)
        {
            inputFilled = true;
        }
    }

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

    /// <summary>
    /// This isn't as needed anymore, but might be useful for adding an extra object that we want the spawner to be attached to
    /// </summary>
    private void Update()
    {
        if (visualInput == null && inputFilled == true)
        {
            Destroy(gameObject);
        }
    }
}