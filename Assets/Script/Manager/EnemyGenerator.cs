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
            GameObject @Go = Instantiate(EnemyPrefab, new Vector3(Random.Range(-7f, 7f), 1f, Random.Range(-7, 7)), Quaternion.identity);
            @Go.GetComponent<Enemy>().Init(stageNum, themeNum);
            enemyList.Add(@Go);
        }
    }
}