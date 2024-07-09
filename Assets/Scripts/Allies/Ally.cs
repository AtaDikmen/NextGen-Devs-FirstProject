using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity
{
    private void Awake()
    {
        targetTag = "Enemy";

        characterName = "Ally";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
    }

}
