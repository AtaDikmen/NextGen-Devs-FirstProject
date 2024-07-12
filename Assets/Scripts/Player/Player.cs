using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //defaultStats
    private float defaultHealth;
    private float defaultAttackSpeed;
    private float defaultSpeed;


    private PlayerManager playerManager;

    private List<Ally> allies = new List<Ally>();

    //Rifle
    private int bulletsFired = 0;
    private int maxBullets = 20;
    private bool isReloading = false;

    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();

        targetTag = "Enemy";

        characterName = "Player";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 300f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
        speed = 5f;
        rotationSpeed = 20f;

        defaultHealth = maxHealth;
        defaultAttackSpeed = attackSpeed;
        defaultSpeed = speed;
    }
    void Start()
    {
        // Register allies as observers
        foreach (var ally in FindObjectsOfType<Ally>())
        {
            allies.Add(ally);
        }
    }

    public void UpgradeAttackSpeed(float _attackSpeed)
    {
        defaultAttackSpeed += _attackSpeed;
        attackSpeed = defaultAttackSpeed;
    }
    public void UpgradeSpeed(float _speed)
    {
        defaultSpeed += _speed;
        speed = defaultSpeed;
    }

    public void UpgradeHealth(float _health)
    {
        defaultHealth += _health;
        currentHealth += _health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Base"))
        {
            Debug.Log("Player entered base");
            gameManager.SetPlayerInsideBase(true);
        }
        else if(other.CompareTag("Outside"))
        {
            Debug.Log("Player left base");
            gameManager.SetPlayerInsideBase(false);
        }
    }

    protected override void Attack()
    {
        if (playerManager.currentWeapon == WeaponType.pistol)
        {
            bulletPrefab = bulletPrefabs[0];

            attackSpeed = defaultAttackSpeed;
            base.Attack();
        }
        else if (playerManager.currentWeapon == WeaponType.rifle)
        {
            bulletPrefab = bulletPrefabs[1];

            RifleFire();
        }
        else if (playerManager.currentWeapon == WeaponType.shotgun)
        {
            bulletPrefab = bulletPrefabs[2];

            OnShotSFX();
            ShotgunFire();
        }

        SetAttackAnim(true);
    }

    private void RifleFire()
    {
        attackSpeed = defaultAttackSpeed * 0.6f;

        if (!isReloading)
        {
            if (bulletsFired < maxBullets)
            {
                bulletsFired++;
                base.Attack();
            }
            else
            {
                StartCoroutine(Reload());
                isAttacking = false;
            }
        }
    }

    private void ShotgunFire()
    {
        attackSpeed = defaultAttackSpeed * 0.6f;

        int bulletCount = 5;
        float spreadAngle = 70f;

        Vector3 targetDirection = (target.position - transform.position).normalized;
        float angleStep = spreadAngle / (bulletCount - 1);
        float angleOffset = -spreadAngle / 2;

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 lookDirection = (targetPosition - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));


        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleOffset + (angleStep * i);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * targetDirection;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * 10f, ForceMode.Impulse);
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = damage;
                bulletScript.targetTag = targetTag;
            }
        }
    }

    protected override void OnShotSFX()
    {
        if(playerManager.currentWeapon == WeaponType.pistol)
            audioManager.PlaySFX2D.Invoke("ShotPistol", 0.15f, false);
        else if(playerManager.currentWeapon == WeaponType.rifle)
            audioManager.PlaySFX2D.Invoke("ShotRifle", 0.15f, false);
        else if (playerManager.currentWeapon == WeaponType.shotgun)
            audioManager.PlaySFX2D.Invoke("ShotShotgun", 0.15f, false);
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(3f);

        bulletsFired = 0;
        isReloading = false;
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

        isAttacking = _isAttacking;
    }

    public List<Ally> GetAllies()
    {
        return allies;
    }
}
