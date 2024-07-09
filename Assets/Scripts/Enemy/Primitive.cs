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
        health = 100f;
        attackRadius = 3.0f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        transform.LookAt(target);
        animator.SetTrigger("Attack");
    }
}
