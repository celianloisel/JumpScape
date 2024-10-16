using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [SerializeField] private int _width, _height;
    
    [SerializeField] private Tile _tilePrefab;
    
    [SerializeField] private Transform _cam;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(12.6f * x, 12.6f * y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
    }	
}