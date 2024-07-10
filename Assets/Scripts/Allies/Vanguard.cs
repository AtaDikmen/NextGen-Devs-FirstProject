using UnityEngine;

public class Vanguard : Ally
{
    void Start()
    {
        characterName = "Vanguard";
        damage = 20;
        attackSpeed = 0.6f;
        maxHealth = 80.0f;
        currentHealth = maxHealth;
        attackRadius = 4f;
    }

    protected override void Attack()
    {
        FireShotgun();
    }

    private void FireShotgun()
    {
        OnShotSFX();

        int bulletCount = 5;
        float spreadAngle = 70f;

        Vector3 targetDirection = (target.position - transform.position).normalized;
        float angleStep = spreadAngle / (bulletCount - 1);
        float angleOffset = -spreadAngle / 2;

        transform.rotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));


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
        audioManager.PlaySFX2D.Invoke("ShotShotgun", 0.15f, false);
    }
}
