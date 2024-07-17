using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamagable
{
    private AudioManager audioManager;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float inactiveDuration = 5f;

    MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    [SerializeField] private BoxCollider boxColliderInChildren;
    private int woodRange;
    private void Awake() {
        woodRange = ResourceManager.Instance.woodRange;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = maxHealth;

    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public void TakeDamage(float damage)
    {
        //AudioClip treeDamage = Resources.Load<AudioClip>("TreeDamage");
        //audioManager.PlaySFX(treeDamage, 0.1f);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            AudioClip treeCollapse = Resources.Load<AudioClip>("TreeCollapse");
            audioManager.PlaySFX(treeCollapse, 0.1f);

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
