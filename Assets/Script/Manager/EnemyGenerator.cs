using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public List<GameObject> enemyList;

    public void CreateEnemy(int stageNum, int themeNum)
    {
        if (enemyList != null)
        {
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] != null)
                {
                    Destroy(enemyList[i]);
                }
            }
            enemyList = new List<GameObject>();
        }
        for (var i = 0; i < 10; i++)
        {
            GameObject @Go = Instantiate(EnemyPrefab);
            @Go.GetComponent<Enemy>().Init(stageNum, themeNum);
            enemyList.Add(@Go);
        }
    }
}