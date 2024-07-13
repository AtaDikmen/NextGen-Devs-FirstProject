using BigRookGames.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //Audio Clips
    private AudioClip pistolSFX;
    private AudioClip rifleSFX;
    private AudioClip shotGunSFX;

    //defaultStats
    private float defaultHealth;
    private float defaultAttackSpeed;
    private float defaultSpeed;

    private PlayerManager playerManager;

    private List<Ally> allies = new List<Ally>();

    //Rocket
    public GameObject rocketPrefab;
    [SerializeField] private GunfireController rocketController;


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
        damage = 15;
        attackSpeed = 1.0f;
        maxHealth = 300f;
        currentHealth = maxHealth;
        attackRadius = 6f;
        nextAttackTime = 0f;
        speed = 5f;
        rotationSpeed = 20f;

        defaultHealth = maxHealth;
        defaultAttackSpeed = attackSpeed;
        defaultSpeed = speed;
    }
    protected override void Start()
    {
        base.Start();

        pistolSFX = Resources.Load<AudioClip>("ShotPistolV1");
        rifleSFX = Resources.Load<AudioClip>("ShotRifle");
        shotGunSFX = Resources.Load<AudioClip>("ShotShotgun");

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
            base.Attack();
        }
        else if (playerManager.currentWeapon == WeaponType.rifle)
        {
            RifleFire();
        }
        else if (playerManager.currentWeapon == WeaponType.shotgun)
        {
            OnShotSFX();
            ShotgunFire();
        }
        else if (playerManager.currentWeapon == WeaponType.rocket)
        {
            FireRocket();
        }

        SetAttackAnim(true);
    }
    private void FireRocket()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        rocketController.target = target;
        rocketController.FireWeapon();

        Rocket rocketScript = rocketPrefab.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.damage = damage;
            rocketScript.explosionRadius = 5.0f;
        }
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
        if (playerManager.currentWeapon == WeaponType.pistol)
            audioManager.PlaySFX(pistolSFX);
        else if (playerManager.currentWeapon == WeaponType.rifle)
            audioManager.PlaySFX(rifleSFX);
        else if (playerManager.currentWeapon == WeaponType.shotgun)
            audioManager.PlaySFX(shotGunSFX);
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(3f);

        bulletsFired = 0;
        isReloading = false;
    }

    protected override void SetAttackAnim(bool _isAttacking)
    {
        if (_isAttacking)
            animator.SetTrigger("Shot");

        isAttacking = _isAttacking;
    }

    public void SetWeaponStat(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.pistol:
                bulletPrefab = bulletPrefabs[0];
                damage = 15;
                attackSpeed = defaultAttackSpeed;
                attackRadius = 6f;
                break;
            case WeaponType.rifle:
                bulletPrefab = bulletPrefabs[1];
                damage = 10;
                attackSpeed = defaultAttackSpeed * 1.3f;
                attackRadius = 7f;
                break;
            case WeaponType.shotgun:
                bulletPrefab = bulletPrefabs[2];
                damage = 10;
                attackSpeed = defaultAttackSpeed * 0.7f;
                attackRadius = 4f;
                break;
            case WeaponType.rocket:
                damage = 65;
                attackSpeed = 0.3f;
                attackRadius = 10f;
                break;
            default:
                break;
        }
    }
}
