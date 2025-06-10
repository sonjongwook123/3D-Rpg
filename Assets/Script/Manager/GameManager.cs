using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public GameData gameData;
    public int enemyCount;
    public Transform Player;
    public event Action StageChanged;
    public event Action ValueChanged;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        Player.GetComponent<Player>().Init(gameData.PlayerData);
        SetGame();
        ValueChanged?.Invoke();
    }

    public void SetGame()
    {
        StageChanged?.Invoke();
        Player.position = new Vector3(0, 1, 0);
        GetComponent<EnemyGenerator>().CreateEnemy(gameData.Stage, gameData.Theme);
        if (gameData.MaxStage >= gameData.Stage)
        {
            gameData.Stage += 1;
        }
        gameData.Theme += 1;

        if (gameData.MaxStage < gameData.Stage)
        {
            gameData.MaxStage = gameData.Stage;
        }
        if (gameData.Theme > 2)
        {
            gameData.Theme = 0;
        }
        enemyCount = 10;
    }

    public void DeadEnemy()
    {
        enemyCount -= 1;
        gameData.Gold += 1 * gameData.Stage;
        ValueChanged?.Invoke();
        if (enemyCount <= 0)
        {
            ChangStage(gameData.Stage);
        }
    }

    void ChangStage(int stageNum)
    {
        gameData.Stage = stageNum;
        SetGame();
    }

    public void ChangeStageFromUI(InputField inputField)
    {
        int stage = int.Parse(inputField.text);
        if (stage < gameData.MaxStage)
        {
            gameData.Stage = stage;
            SetGame();
        }
    }

}

[System.Serializable]
public class GameData
{
    public int Stage;
    public int MaxStage;
    public int Theme;
    public double Gold;
    public PlayerData PlayerData;
}

[System.Serializable]
public class PlayerData
{
    public int level = 1;
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackDelay = 1f;
    public int maxHP = 100, maxMP = 100;
    public int currentExp, expToLevelUp = 100;
    public int attackPower = 10;
}