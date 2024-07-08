using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    void Start()
    {
        characterName = "Bandit";
        damage = 15;
        attackSpeed = 1.0f;
        health = 80.0f;
        attackRadius = 4.0f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        base.Attack();
    }
}
