using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public string targetTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("TreeInner") && targetTag == "Enemy")
        {
            other.gameObject.GetComponentInParent<Tree>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
