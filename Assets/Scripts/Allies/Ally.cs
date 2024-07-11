using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllyType
{
    rookie,
    soldier,
    vanguard,
    bulldozer
}

public class Ally : Entity
{
    public AllyType allyType;

    protected int allyCount;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        targetTag = "Enemy";

        characterName = "Ally";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
        speed = 5f;
        rotationSpeed = 20f;
    }
}
