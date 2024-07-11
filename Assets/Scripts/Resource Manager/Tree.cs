using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float inactiveDuration = 5f;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;
    BoxCollider boxColliderInChildren;
    private int woodRange;
    private void Awake() {
        woodRange = ResourceManager.Instance.woodRange;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderInChildren = GetComponentInChildren<BoxCollider>();
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
        currentHealth = 0;
        ResourceManager.Instance.AddWood(Random.Range(woodRange / 2, woodRange));
        StartCoroutine(SetInactiveTemp());
    }

    private IEnumerator SetInactiveTemp()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        boxColliderInChildren.enabled = false;
        yield return new WaitForSeconds(inactiveDuration);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        boxColliderInChildren.enabled = true;
        currentHealth = maxHealth;
    }
}
