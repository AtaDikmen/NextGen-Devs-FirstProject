using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampSpawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    private int spawnNumber;
    private bool isUnitsSpawned = false;
    SphereCollider sphereCollider;

    private void Awake() {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update() {
        if (!isUnitsSpawned) 
        {
            for (int i = 0; i < spawnNumber; i++)
            {
                InstantiateUnits();
            }
            isUnitsSpawned = true;
        }
    }

    public void SetEnemyPrefabNumber(GameObject enemyPrefab, int spawnNumber)
    {
        this.enemyPrefab = enemyPrefab; 
        this.spawnNumber = spawnNumber;
    }

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    private void InstantiateUnits()
    {
        Vector3 center = sphereCollider.transform.position + sphereCollider.center;
        float radius = sphereCollider.radius;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(0, radius);

        Vector3 position = new Vector3(center.x + randomDirection.x * randomDistance, 0, center.z + randomDirection.z * randomDistance);

        Instantiate(enemyPrefab, position, Quaternion.identity);
    }


}
