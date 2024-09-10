using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarMap : MonoBehaviour
{
    [SerializeField]
    Tilemap landTiles;
    [SerializeField]
    int[,] aStarMap = new int[MapGeneration.mapWidth, MapGeneration.mapHeight];

    public void GenerateAStarMap()
    {
        for(int i = 0; i < aStarMap.GetLength(0); i++)
        {
            for (int j = 0; j < aStarMap.GetLength(1); j++)
            {
                aStarMap[i, j] = landTiles.GetTile(new Vector3Int(i, j, 0)) != null ? 0 : 1;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        for (int i = 0; i < aStarMap.GetLength(0); i++)
        {
            for (int j = 0; j < aStarMap.GetLength(1); j++)
            {
                if (aStarMap[i, j] == 1)
                {
                    Gizmos.DrawCube(new Vector3(i - 49.5f, j - 49.5f, 0), Vector3.one);
                }
            }
        }
    }
}
