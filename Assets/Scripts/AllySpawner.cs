using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public AllyType allyType;

    [SerializeField] private GameObject[] allyList;

    private GameObject spawnedAlly;
    public Transform joinPosition;

    [SerializeField] private TextMeshProUGUI rescueText;

    public bool isSpawned = false;
    public bool isEnemiesDead;

    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(!isSpawned)
        {
            if(spawnedAlly != null)
            {
                Destroy(spawnedAlly);
            }

            SpawnRandomAlly();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemiesDead)
        {
            if (other.transform.CompareTag("Player"))
            {
                other.gameObject.GetComponentInParent<PlayerManager>().AllyJoinGroup(allyType, joinPosition);
                boxCollider.enabled = false;
                StartCoroutine(JoinTextCoroutine());
                Destroy(spawnedAlly);
            }
        }
    }

    private IEnumerator JoinTextCoroutine()
    {
        rescueText.text = "The " + allyType.ToString() + " joined your group!";

        yield return new WaitForSeconds(2);

        rescueText.text = "";
    }


    private void SpawnRandomAlly()
    {
        int randomIndex = Random.Range(0, 4);

        spawnedAlly = Instantiate(allyList[randomIndex], transform.position, transform.rotation);

        SetAllyType(randomIndex);

        isSpawned = true;
        boxCollider.enabled = true;
    }

    private void SetAllyType(int index)
    {
        if (index == 0)
            allyType = AllyType.rookie;
        if (index == 1)
            allyType = AllyType.soldier;
        if (index == 2)
            allyType = AllyType.vanguard;
        if (index == 3)
            allyType = AllyType.bulldozer;
    }
}
