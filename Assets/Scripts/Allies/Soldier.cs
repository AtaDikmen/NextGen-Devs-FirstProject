using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : Ally
{
    private int bulletsFired = 0;
    private int maxBullets = 20;
    private bool isReloading = false;

    void Start()
    {
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
        audioManager.PlaySFX2D.Invoke("ShotRifle", 0.15f, false);
    }
}
