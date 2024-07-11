using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private List<Ally> allies = new List<Ally>();
    private void Awake()
    {
        animator = GetComponent<Animator>();

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

    protected override void SetAttackAnim(bool _isAttacking)
    {
        if (_isAttacking)
            animator.SetTrigger("Shot");
    }

    public List<Ally> GetAllies()
    {
        return allies;
    }
}
