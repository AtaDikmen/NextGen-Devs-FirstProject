using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Bear, Primitive, Bandit
}

public class Enemy : Entity
{
    protected EnemyAI enemyAI;

    protected EnemyTypes enemyType;

    private void Awake()
    {
        targetTag = "Player";

        characterName = "Enemy";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
        nextAttackTime = 0f;

        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
    }


    public override void OnDead()
    {
        switch (enemyType)
        {
            case EnemyTypes.Bear:
                ResourceManager.Instance.AddGold(2);
                break;
            case EnemyTypes.Primitive:
                ResourceManager.Instance.AddGold(5);
                break;
            case EnemyTypes.Bandit:
                ResourceManager.Instance.AddGold(10);
                break;
        }
    }
}
