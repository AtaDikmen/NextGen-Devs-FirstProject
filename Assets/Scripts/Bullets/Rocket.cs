using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int damage;
    public float explosionRadius;

    private bool isExplode;

    private void Start()
    {
        if (isExplode) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.gameObject.GetComponent<Entity>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        isExplode = true;

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }
}
