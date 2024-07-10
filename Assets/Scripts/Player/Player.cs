using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IRunnable
{
    private List<Ally> allies = new List<Ally>();
    private bool isRunning = false;
    private void Awake()
    {
        targetTag = "Enemy";

        characterName = "Player";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
        speed = 5f;
        rotationSpeed = 20f;
        runMultiplier = 2f;
    }
    void Start()
    {
        // Register allies as observers
        foreach (var ally in FindObjectsOfType<Ally>())
        {
            allies.Add(ally);
        }
    }

    public void AddAlly(Ally ally)
    {
        allies.Add(ally);
    }

    public void RemoveAlly(Ally ally)
    {
        allies.Remove(ally);
    }

    public void StartRunning()
    {
        if (!isRunning)
        {
            isRunning = true;
            speed *= runMultiplier;
            foreach (var observer in allies)
            {
                observer.StartRunning();
            }
        }
    }

    public void StopRunning()
    {
        if (isRunning)
        {
            isRunning = false;
            speed /= runMultiplier;
            foreach (var observer in allies)
            {
                observer.StopRunning();
            }
        }
    }
}
