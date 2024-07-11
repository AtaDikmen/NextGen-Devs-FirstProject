using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GridManager : Singleton<GridManager>
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;
    [SerializeField] private int borderSize = 5;
    private int innerBorderSize;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject edgeTreePrefab;
    [SerializeField] private List<GameObject> environmentPrefabList;
    [SerializeField] List<GameObject> instatiatedTiles;
    [SerializeField] private List<Material> edgeMaterialList;


    public Grid<Tile> _grid;
    private void Awake() {
        innerBorderSize = borderSize / 2;
        _grid = new Grid<Tile>(width, height, cellSize);
    }

    public void InstantiateMap()
    {
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                InstantiateGrid(x, z);
            }
        }
    }

    private void InstantiateGrid(int x, int z)
    {
        Vector3 position = new Vector3(
            x * 2.26f, 
            0, 
            (z + x * 0.5f - x / 2) * 2.62f);
            
        GameObject tempPrefab = Instantiate(
            tilePrefab, 
            position,
            Quaternion.identity
            );

        tempPrefab.transform.SetParent(transform, false);
        instatiatedTiles.Add(tempPrefab);
        tempPrefab.isStatic = true;

        InstantiateBorderTree(x, z, borderSize, innerBorderSize, tempPrefab);
    }


    private void InstantiateBorderTree(int x, int z, int borderSize, int innerBorderSize, GameObject tempPrefab)
    {
        if (x < borderSize || x > width - borderSize - 1 || z < borderSize || z > height - borderSize -1)
        {
            tempPrefab.GetComponent<MeshRenderer>().material = edgeMaterialList[0];
            GameObject edgeTreeTemp = Instantiate(
                edgeTreePrefab,
                Vector3.zero,
                Quaternion.identity
            );
            edgeTreeTemp.transform.SetParent(tempPrefab.transform, false);
            edgeTreeTemp.gameObject.isStatic = true;
            edgeTreeTemp.gameObject.transform.GetChild(0).gameObject.isStatic = true;
            edgeTreeTemp.gameObject.transform.GetChild(1).gameObject.isStatic = true;

            edgeTreeTemp.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
            edgeTreeTemp.transform.position = new Vector3(tempPrefab.transform.position.x, 0, tempPrefab.transform.position.z - 1.25f);
        }

        if (x >= borderSize && x < borderSize + innerBorderSize && z >= borderSize && z <= height - borderSize -1)
        {
            tempPrefab.GetComponent<MeshRenderer>().material = edgeMaterialList[1];
        }

        if (x >= width - (borderSize + innerBorderSize) && x <= width - borderSize - 1 && z >= borderSize && z <= height - borderSize -1)
        {
            tempPrefab.GetComponent<MeshRenderer>().material = edgeMaterialList[1];
        }

        if (z >= borderSize && z < borderSize + innerBorderSize && x >= borderSize && x <= width - borderSize -1)
        {
            tempPrefab.GetComponent<MeshRenderer>().material = edgeMaterialList[1];
        }
        
        if (z >= height - (borderSize + innerBorderSize) && z <= height - borderSize - 1 && x >= borderSize && x <= width - borderSize -1)
        {
            tempPrefab.GetComponent<MeshRenderer>().material = edgeMaterialList[1];
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
