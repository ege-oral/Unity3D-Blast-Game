using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board.Generate
{
    public class GenerateBoard : MonoBehaviour
    {
        public static void FillTheBoard(GameObject[,] grid, int numberOfRows, int numberOfCollums, int numberOfColors, GameObject[] tiles, GameObject allTileInstances)
        {   
            for(int row = 0; row < numberOfRows; row++)
            {
                for(int collum = 0; collum < numberOfCollums; collum++)
                {
                    GameObject tile = Instantiate(tiles[Random.Range(0,numberOfColors)], 
                                                new Vector3(collum, row, 0f), 
                                                Quaternion.Euler(0f,0f,180f));
                    grid[row, collum] = tile;
                    tile.transform.parent = allTileInstances.transform;
                }
            }
        }
    }
}
