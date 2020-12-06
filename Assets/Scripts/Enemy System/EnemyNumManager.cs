using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNumManager : MonoBehaviour
{
    private int enemyNum = 0;
    public int totalEnemy = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getEnemyNum()
    {
        return enemyNum;
    }

    public void increaseEnemyNum() {
        enemyNum++;
    }

    public void decreaseEnemyNum()
    {
        enemyNum--;
    }
}
