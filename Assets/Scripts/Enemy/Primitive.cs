using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primitive : Enemy
{
    protected override void Start()
    {
        enemyType = EnemyTypes.Primitive;
        characterName = "Primitive";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 5f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        transform.LookAt(target);
        OnShotSFX();
        animator.SetTrigger("Attack");
        base.Attack();
    }

    protected override void OnShotSFX()
    {
        AudioClip shotSFX = Resources.Load<AudioClip>("ShotArrow");

        AudioManager.Instance.PlaySFX(shotSFX);
    }
}
