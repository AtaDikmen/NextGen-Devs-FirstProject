using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    private int _width;
    private int _height;
    private int _cellSize;
    public T[,] _grid;
    public T this [Vector2Int gridPosition]
    {
        get => _grid[gridPosition.x, gridPosition.y];
        set => _grid[gridPosition.x, gridPosition.y] = value;
    }

    public T this [int x, int y]
    {
        get => _grid[x,y];
        set => _grid[x,y] = value;
    }

    public Grid(int width, int height, int cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _grid = new T[_width, _height];
    }

    public void SetItem(Vector2Int gridPosition, T item)
    {
        _grid[gridPosition.x, gridPosition.y] = item;
    }

    public T GetItem(Vector2Int gridPosition)
    {
        return _grid[gridPosition.x, gridPosition.y];
    }

    public Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.y) * _cellSize + new Vector3(1, 0, 1) * (_cellSize * 0.5f);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        int z = Mathf.FloorToInt(worldPosition.z / _cellSize);

        return new Vector2Int(x, z);   
    }

    public bool IsInDimensions(Vector2Int gridPosition)
    {
        return (gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < _grid.GetLength(0) && gridPosition.y < _grid.GetLength(1));
    }
}
