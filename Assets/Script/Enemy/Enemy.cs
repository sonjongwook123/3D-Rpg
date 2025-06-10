using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    public int attackPower;
    public float speed;
}

public class Enemy : MonoBehaviour
{
    public List<EnemyData> dataList;
    public EnemyData enemyData;

    private int currentHP;

    public void Init(int stageNum, int themeNum)
    {
        this.enemyData = dataList[themeNum];
        currentHP = enemyData.maxHP * stageNum;

        Renderer renderer = GetComponent<Renderer>();

        // 머티리얼이 존재하는지 확인
        if (renderer != null)
        {
            switch (themeNum)
            {
                case 0:
                    {
                        renderer.material.color = Color.yellow;
                        break;
                    }
                case 1:
                    {
                        renderer.material.color = Color.red;
                        break;
                    }
                case 2:
                    {
                        renderer.material.color = Color.blue;
                        break;
                    }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            GameManager.Instance.DeadEnemy();
            //FindObjectOfType<Player>().GainExp(20); // 경험치 보상
            Destroy(gameObject);
        }
    }
}