using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    protected string characterName;
    protected int damage;
    protected float attackSpeed;
    protected float health;
    protected float attackRadius;
    protected float nextAttackTime;
    protected Transform target;
    protected string targetTag;


    void Update()
    {
        FindClosestEnemy();

        if (target != null && Time.time >= nextAttackTime)
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
            if (hitCollider.CompareTag(targetTag))
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
            target = closestEnemy;
        }
        else
        {
            target = null;
        }
    }

    protected virtual void Attack()
    {
        transform.LookAt(target);
        
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 direction = (target.position - transform.position).normalized;

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

        Debug.Log(characterName + " is attacking " + target.name + " for " + damage + " damage.");
    }

    protected virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
