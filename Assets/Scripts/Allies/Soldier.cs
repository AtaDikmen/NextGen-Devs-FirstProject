using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : Ally
{
    private int bulletsFired = 0;
    private int maxBullets = 20;
    private bool isReloading = false;

    protected override void Start()
    {
        base.Start();

        allyType = AllyType.soldier;


        characterName = "Soldier";
        damage = 5;
        attackSpeed = 4f;
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        attackRadius = 7.0f;
    }
    

    protected override void Attack()
    {

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

    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetTrigger("reload");

        yield return new WaitForSeconds(3f);

        bulletsFired = 0;
        isReloading = false;
    }

    protected override void SetAttackAnim(bool _isAttacking)
    {
        isAttacking = _isAttacking;
        animator.SetBool("isAttacking", isAttacking);
    }

    protected override void OnShotSFX()
    {
        AudioClip shotSFX = Resources.Load<AudioClip>("ShotRifle");

        AudioManager.Instance.PlaySFX(shotSFX);
    }

    public override void UpgradeAlly()
    {
        damage *= 2;
        attackSpeed = 6f;
        maxHealth = 200f;
        currentHealth = maxHealth;

        Debug.Log("Soldier UPGRADED!");
    }
}
