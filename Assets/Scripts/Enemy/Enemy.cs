using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected EnemyAI enemyAI;

    private void Awake()
    {
        targetTag = "Player";

        characterName = "Enemy";
        damage = 10;
        attackSpeed = 1.0f;
        health = 100.0f;
        attackRadius = 5.0f;
        nextAttackTime = 0f;

        enemyAI = GetComponent<EnemyAI>();
    }

}
