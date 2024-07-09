using System.Collections;
using UnityEngine;

public class Soldier : Ally
{
    void Start()
    {
        characterName = "Soldier";
        damage = 10;
        attackSpeed = 4f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 7.0f;
    }

    protected override void Attack()
    {
        base.Attack();
    }
}
