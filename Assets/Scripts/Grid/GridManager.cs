using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GridManager : Singleton<GridManager>
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;
    [SerializeField] private GameObject tilePrefab;
    List<GameObject> instatiatedTiles;

    public Grid<Tile> _grid;
    private void Awake() {
        _grid = new Grid<Tile>(width, height, cellSize);
    }

    public void InstantiateMap()
    {
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 position = new Vector3(
                    x * 2.26f, 
                    0, 
                    (z + x * 0.5f - x / 2) * 2.62f);
                    
                GameObject tempPrefab = Instantiate(
                    tilePrefab, 
                    position,
                    Quaternion.identity);
                tempPrefab.transform.SetParent(transform, false);
                
                instatiatedTiles.Add(tempPrefab);
            }
        }
    }

    public void DestroyMap()
    {
        foreach (var tile in instatiatedTiles)
        {
            DestroyImmediate(tile);
        }
    }
}
