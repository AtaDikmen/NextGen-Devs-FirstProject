using System.Collections;
using UnityEngine;

public class Soldier : Ally
{
    void Start()
    {
        allyName = "Soldier";
        damage = 10;
        attackSpeed = 4f;
        health = 80.0f;
        attackRadius = 7.0f;
    }

    protected override void Attack()
    {
        base.Attack();
        //StartCoroutine(FireBullets());
    }

    private IEnumerator FireBullets()
    {
        float burstInterval = 0.05f; 
        float seriesInterval = 0.1f;

        for (int i = 0; i < 7; i++)
        {
            base.Attack();
            yield return new WaitForSeconds(burstInterval);
        }

        yield return new WaitForSeconds(seriesInterval); 
    }
}
