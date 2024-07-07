using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int damage;
    public float explosionRadius;
    //public GameObject explosionEffect;

    private bool isExplode;

    void OnCollisionEnter(Collision collision)
    {
        if (isExplode) return;
        
        //Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                // nearbyObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(nearbyObject.gameObject);
            }
        }

        isExplode = true;

        Destroy(gameObject);
    }
}
