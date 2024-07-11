using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawnManager : MonoBehaviour
{
    public AllyType allyType;

    [SerializeField] private GameObject[] allyList;

    private GameObject spawnedAlly;
    public Transform joinPosition;

    public bool isEnemiesDead;

    void Start()
    {
        SpawnRandomAlly();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemiesDead)
        {
            if (other.transform.CompareTag("Player"))
            {
                other.gameObject.GetComponentInParent<PlayerManager>().AllyJoinGroup(allyType, joinPosition);
                
                Destroy(spawnedAlly);
            }
        }
    }
    

    private void SpawnRandomAlly()
    {
        int randomIndex = Random.Range(0, 4);

        spawnedAlly = Instantiate(allyList[randomIndex], transform.position, transform.rotation);

        SetAllyType(randomIndex);
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
