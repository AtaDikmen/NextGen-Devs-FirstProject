using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    public float dashSpeed = 15f;
    public float dashCooldown = 2f;

    void Start()
    {
        enemyType = EnemyTypes.Bear;
        characterName = "Bear";
        damage = 15;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 2.0f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        transform.LookAt(target);
        animator.SetTrigger("Attack");
        target.GetComponent<Entity>().TakeDamage(damage);
    }
}
