using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    protected Animator animator;
    [SerializeField] protected MyAudioManagerSO audioManager;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    protected string characterName;
    protected int damage;
    protected float attackSpeed;
    protected float currentHealth;
    protected float maxHealth;
    protected float attackRadius;
    protected float nextAttackTime;
    protected float speed;
    protected float rotationSpeed;
    protected float runMultiplier;
    protected Transform target;
    protected string targetTag;
    public bool isAlive = true;
    public HealthBar healthBar;
    public bool isTargetFound;
    public bool isAttacking;


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
            isTargetFound = true;
        }
        else
        {
            target = null;
            isTargetFound = false;
            SetAttackAnim(false);
        }
    }

    protected virtual void Attack()
    {
        OnShotSFX();
        SetAttackAnim(true);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Destroy(bullet, 4);

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        bullet.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

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

    protected virtual void SetAttackAnim(bool _isAttacking)
    {

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

    protected virtual void OnShotSFX()
    {

    }

    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }
}
