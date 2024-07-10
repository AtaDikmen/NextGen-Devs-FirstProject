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
        characterName = "Soldier";
        damage = 10;
        attackSpeed = 4f;
        health = 100f;
        attackRadius = 7.0f;
    }
    

    protected override void Attack()
    {

        if (!isReloading)
        {
            if (bulletsFired < maxBullets)
            {
                base.Attack();
                bulletsFired++;
                Debug.Log(bulletsFired);
            }
            else
            {
                Debug.Log("RELOAD");
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
}
