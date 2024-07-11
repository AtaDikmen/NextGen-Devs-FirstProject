using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabList;
    private int spawnNumber = 5;
    private bool isUnitsSpawned = false;
    BoxCollider boxCollider;

    private void Awake() {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update() {
        if (!isUnitsSpawned) 
        {
            GameObject enemyPrefab = enemyPrefabList[Random.Range(0, enemyPrefabList.Count)];
            for (int i = 0; i < spawnNumber; i++)
            {
                InstantiateUnits(enemyPrefab);
            }
            isUnitsSpawned = true;
        }
    }

    private void SetEnemyPrefabNumber(int spawnNumber)
    { 
        this.spawnNumber = spawnNumber;
    }

    private void InstantiateUnits(GameObject enemyPrefab)
    {
        Debug.Log("enemy prefab name: " + enemyPrefab.name);
        Vector3 center = boxCollider.transform.position + boxCollider.center;
        Vector3 size = boxCollider.size;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(0, size.z);

        Vector3 position = new Vector3(center.x + randomDirection.x * randomDistance, 0, center.z + randomDirection.z * randomDistance);

        Instantiate(enemyPrefab, position, Quaternion.identity);
    }


}
