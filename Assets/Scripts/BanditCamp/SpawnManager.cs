using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<GameObject> spawnableTiles;
    [SerializeField] private List<GameObject> enemyPrefabList;
    [SerializeField] private CampSpawner campPrefab;

    private void Update() {

    }

    private void InstantiateCamps()
    {
        foreach (var tile in spawnableTiles)
        {
            int randomEnemyNumber = Random.Range(0, enemyPrefabList.Count);

            Vector3 position = new Vector3(
                tile.transform.position.x,
                0,
                tile.transform.position.z
            );

            CampSpawner tempTile = Instantiate(
                campPrefab,
                position,
                Quaternion.identity
            );

            tempTile.SetEnemyPrefabNumber(enemyPrefabList[randomEnemyNumber], Random.Range(5, 10));
        }

    }




}
