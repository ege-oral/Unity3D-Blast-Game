using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board.Generate;
using Tile.FindConnections;

namespace Board.Shuffle
{
    public class ShuffleBoard : MonoBehaviour
    {
        BoardManager boardManager;
        GenerateBoard generateBoard;
        FindConnectedTiles findConnectedTiles;

        private void Start() 
        {
            boardManager = GetComponent<BoardManager>();
            generateBoard = GetComponent<GenerateBoard>();
            findConnectedTiles = GetComponent<FindConnectedTiles>();
        }

        public void ShuffleTheTiles()
        {
            for(int i = 0; i < boardManager.grid.GetLength(0); i++)
            {
                for(int j = 0; j < boardManager.grid.GetLength(1); j++)
                {
                    Destroy(boardManager.grid[i,j]);
                }
            }

            // After shuffling all tiles we generate another board and find connected tiles.
            generateBoard.FillTheBoard();
            findConnectedTiles.FindAllConnectedTiles();
        }
    }
}
