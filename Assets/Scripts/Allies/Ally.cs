using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    protected string allyName;
    protected int damage;
    protected float attackSpeed;
    protected float health;
    protected float attackRadius;
    protected float nextAttackTime;
    protected Transform targetEnemy;

    void Start()
    {
        allyName = "Ally";
        damage = 10;
        attackSpeed = 1.0f;
        health = 100.0f;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
    }

    void Update()
    {
        FindClosestEnemy();

        if (targetEnemy != null && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackSpeed;
        }
    }

    protected void FindClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hitCollider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
        }
        else
        {
            targetEnemy = null;
        }
    }

    protected virtual void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 direction = (targetEnemy.position - transform.position).normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * 10f, ForceMode.Impulse);
        }

        // Bullet'a zarar verme deðerini ata
        //Bullet bulletScript = bullet.GetComponent<Bullet>();
        //if (bulletScript != null)
        //{
        //    bulletScript.damage = damage;
        //}

        Debug.Log(allyName + " is attacking " + targetEnemy.name + " for " + damage + " damage.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
