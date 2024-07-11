using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Primitive : Enemy
{
    void Start()
    {
        enemyType = EnemyTypes.Primitive;
        characterName = "Primitive";
        damage = 15;
        attackSpeed = 1.0f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 3.0f;
    }

    protected override void Attack()
    {
        enemyAI.Stop();
        transform.LookAt(target);
        animator.SetTrigger("Attack");
        //base.Attack();

        OnShotSFX();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletPrefab.transform.rotation);

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        bullet.transform.rotation = lookRotation * Quaternion.Euler(-90, 0, 0);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * 10f, ForceMode.Impulse);
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.targetTag = targetTag;
            bulletScript.damage = damage;
        }
    }
}
