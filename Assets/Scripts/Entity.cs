using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    protected string characterName;
    protected int damage;
    protected float attackSpeed;
    protected float currentHealth;
    protected float maxHealth;
    protected float attackRadius;
    protected float nextAttackTime;
    protected Transform target;
    protected string targetTag;
    public bool isAlive = true;
    public HealthBar healthBar;

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
        //transform.LookAt(target);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 direction = (target.position - transform.position).normalized;

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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Debug.Log(characterName + " is Dead");
            OnDead();
            isAlive = false;
            Destroy(gameObject); // for now
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.ShowHealthBarTemporarily();
        float healthNormalized = currentHealth / maxHealth;
        healthBar.SetHealth(healthNormalized);
    }

    public virtual void OnDead()
    {

    }
}
