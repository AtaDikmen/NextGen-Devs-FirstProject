using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primitive : Enemy
{
    void Start()
    {
        enemyType = EnemyTypes.Primitive;
        characterName = "Primitive";
        damage = 15;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 3.0f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        base.Attack();
    }
}
