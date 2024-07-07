using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
