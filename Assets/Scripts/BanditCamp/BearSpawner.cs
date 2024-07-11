using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabList;
    [SerializeField] private int spawnNumber;
    
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

    private void InstantiateUnits()
    {
        Vector3 center = sphereCollider.transform.position + sphereCollider.center;
        float radius = sphereCollider.radius;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(0, radius);

        Vector3 position = new Vector3(center.x + randomDirection.x * randomDistance, 0, center.z + randomDirection.z * randomDistance);

        Instantiate(enemyPrefabList[0], position, Quaternion.identity);
    }
    
    
}
