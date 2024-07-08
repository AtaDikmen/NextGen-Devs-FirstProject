using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    public float dashSpeed = 15f;
    public float dashCooldown = 2f;
    private float lastDashTime = -10f;
    private Vector3 initialPosition;
    private bool isDashing;

    void Start()
    {
        characterName = "Bear";
        damage = 15;
        attackSpeed = 1.0f;
        health = 80.0f;
        attackRadius = 2.0f;
    }

    protected override void Attack()
    {
        // Special attack of Bear
        enemyAI.Stop();

        if (!isDashing)
        {
            isDashing = true;
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        Vector3 targetPosition = target.transform.position;
        float dashTime = 0.2f;
        float elapsedTime = 0;
        initialPosition = transform.position;

        while (elapsedTime < dashTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < dashCooldown)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, elapsedTime / dashCooldown);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}
