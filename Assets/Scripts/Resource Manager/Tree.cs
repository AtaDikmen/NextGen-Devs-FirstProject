using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float inactiveDuration = 5f;

    MeshRenderer meshRenderer;
    SphereCollider sphereCollider;
    private int woodRange;
    private void Awake() {
        woodRange = ResourceManager.Instance.woodRange;
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Tree health: " + currentHealth);
        if (currentHealth <= 0)
        {
            OnDead();
        }
    }

    public virtual void OnDead()
    {
        ResourceManager.Instance.AddWood(Random.Range(woodRange / 2, woodRange));
        StartCoroutine(SetInactiveTemp());
    }

    private IEnumerator SetInactiveTemp()
    {
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(inactiveDuration);
        meshRenderer.enabled = true;
        sphereCollider.enabled = true;
        currentHealth = maxHealth;
    }
}
