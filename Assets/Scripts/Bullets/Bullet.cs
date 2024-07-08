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
            Debug.Log(targetTag);

            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
