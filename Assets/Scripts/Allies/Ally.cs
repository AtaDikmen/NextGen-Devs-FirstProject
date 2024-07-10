using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity, IRunnable
{
    private bool isRunning = false;
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
        runMultiplier = 2f;
    }

    public void StartRunning()
    {
        if (!isRunning)
        {
            isRunning = true;
            speed *= runMultiplier;
        }
    }

    public void StopRunning()
    {
        if (isRunning)
        {
            isRunning = false;
            speed /= runMultiplier;
        }
    }
}
