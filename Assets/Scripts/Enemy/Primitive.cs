using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primitive : Enemy
{
    void Start()
    {
        characterName = "Primitive";
        damage = 15;
        attackSpeed = 1.0f;
        health = 80.0f;
        attackRadius = 3.0f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        base.Attack();
    }
}