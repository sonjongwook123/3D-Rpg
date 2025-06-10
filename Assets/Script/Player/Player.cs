using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerData data;
    public int currentHP, currentMP;
    private enum State { Moving, Attacking, Idle }
    private State currentState = State.Moving;
    private Transform targetEnemy;
    public Animator anim;
    public event Action OnAttack;

    void Start()
    {
        currentHP = data.maxHP;
        currentMP = data.maxMP;
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    public void Init(PlayerData data)
    {
        this.data = data;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                FindNearestEnemy();
                if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.position) <= data.attackRange)
                {
                    currentState = State.Attacking;
                    StartCoroutine(AttackRoutine());
                }

                if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.position) > data.attackRange)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, data.moveSpeed * Time.deltaTime);
                }

                if (targetEnemy == null)
                {
                    currentState = State.Idle;
                }
                break;
            case State.Attacking:
                if (targetEnemy == null || Vector3.Distance(transform.position, targetEnemy.position) > data.attackRange)
                {
                    currentState = State.Moving;
                    StopCoroutine(AttackRoutine());
                }
                break;
            case State.Idle:
                FindNearestEnemy();
                if (targetEnemy != null)
                {
                    currentState = State.Moving;
                }
                break;
        }
    }

    IEnumerator AttackRoutine()
    {
        while (currentState == State.Attacking)
        {
            if (targetEnemy != null)
            {
                Enemy enemy = targetEnemy.GetComponent<Enemy>();
                enemy.TakeDamage(data.attackPower);
                // 파티클 이펙트 
                ParticleSystem ps = GetComponent<ParticleSystem>();
                if (ps != null) ps.Play();
                // 사운드 이펙트
                AudioSource audio = GetComponent<AudioSource>();
                if (audio != null) audio.Play();
                if (anim != null) anim.SetBool("Attack", true);
                OnAttack?.Invoke();
            }
            yield return new WaitForSeconds(data.attackDelay);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FindNearestEnemy()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float closestDistance = Mathf.Infinity;
        targetEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemy.transform;
            }
        }
    }

    public void GainExp(int exp)
    {
        data.currentExp += exp;
        if (data.currentExp >= data.expToLevelUp)
        {
            data.currentExp -= data.expToLevelUp;
            data.expToLevelUp += 50;
            data.attackPower += 5;
            data.level++;
        }
    }
}
