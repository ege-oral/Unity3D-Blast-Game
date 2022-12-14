using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board.Generate
{
    public class GenerateBoard : MonoBehaviour
    {
        BoardManager boardManager;

        private void Start() => boardManager = GetComponent<BoardManager>();

        public void FillTheBoard()
        {   
            for(int row = 0; row < boardManager.NumberOfRows; row++)
            {
                for(int column = 0; column < boardManager.NumberOfColumns; column++)
                {
                    GameObject tile = Instantiate(boardManager.tiles[Random.Range(0, boardManager.NumberOfColors)], 
                                                  new Vector3(column, row, 0f), 
                                                  Quaternion.Euler(0f,0f,180f));
                    boardManager.grid[row, column] = tile;
                    tile.transform.parent = boardManager.allTileInstances.transform;
                }
            }
        }
    }
}
