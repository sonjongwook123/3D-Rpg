using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
    public enum ItemType { HealthPotion, AttackScroll }
    public ItemType itemType;
    public string itemName;
    public int value;
    public float duration; // 효과 지속 시간
}

public class ItemSystem : MonoBehaviour
{
    public Player player;
    public ItemData data;
    public List<ItemData> dataList;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void PurchaseItem()
    {
        if (GameManager.Instance.gameData.Gold >= 100)
        {
            GameManager.Instance.gameData.Gold -= 100;
            dataList.Add(data);
        }
    }

    public void UseItem()
    {
        if (dataList.Count > 0)
        {
            StartCoroutine(ApplyAttackBoost(dataList[0].value, dataList[0].duration));
            // 파티클 이펙트
            //ParticleSystem ps = player.GetComponent<ParticleSystem>();
            //if (ps != null) ps.Play();
            // 사운드 이펙트
            //AudioSource audio = player.GetComponent<AudioSource>();
            //if (audio != null) audio.Play();
            dataList.Remove(dataList[0]);
        }
    }

    IEnumerator ApplyAttackBoost(int boost, float duration)
    {
        player.data.attackPower += boost;
        yield return new WaitForSeconds(duration);
        player.data.attackPower -= boost;
    }
}
