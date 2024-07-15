using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AllyType
{
    rookie,
    soldier,
    vanguard,
    bulldozer
}

public class Ally : Entity
{
    public NavMeshAgent agent;

    public AllyType allyType;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        targetTag = "Enemy";

        characterName = "Ally";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
        agent.speed = 5f;
        rotationSpeed = 20f;
    }

    public virtual void UpgradeAlly()
    {

    }
}
