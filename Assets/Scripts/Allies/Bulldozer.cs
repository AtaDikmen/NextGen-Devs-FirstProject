using System.Net.Sockets;
using UnityEngine;

public class Bulldozer : Ally
{
    public GameObject rocketPrefab;

    void Start()
    {
        allyName = "Bulldozer";
        damage = 100;
        attackSpeed = 0.1f;
        health = 150.0f;
        attackRadius = 10.0f;
    }

    protected override void Attack()
    {
        FireRocket();
    }

    private void FireRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 direction = (targetEnemy.position - transform.position).normalized;

        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * 20f, ForceMode.Impulse);
        }

        Rocket rocketScript = rocket.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.damage = damage;
            rocketScript.explosionRadius = 5.0f;
        }

        Debug.Log(allyName + " fired a rocket at " + targetEnemy.name + " causing an explosion with " + damage + " damage.");
    }
}
