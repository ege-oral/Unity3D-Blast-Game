using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board;
using Board.Shuffle;
using Tile.TileObject;
using Tile.ChangeTilesIcon;

namespace Tile.FindConnections
{    
    public class FindConnectedTiles : MonoBehaviour
    {
        BoardManager boardManager;
        ShuffleBoard shuffleBoard;
        ChangeConnectedTilesIcon changeConnectedTilesIcon;

        private void Start() 
        {
            boardManager = GetComponent<BoardManager>();
            shuffleBoard = GetComponent<ShuffleBoard>();
            changeConnectedTilesIcon = GetComponent<ChangeConnectedTilesIcon>();
        }

        public void FindAllConnectedTiles()
        {
            // If every int value in tileConnections is 1 that means every tile has only one connection.
            // Which is the tile itself so we need to shuffle.
            List<int> tileConnections = new List<int>();
            for(int row = 0; row < boardManager.grid.GetLength(0); row++)
            {
                for(int col = 0; col < boardManager.grid.GetLength(1); col++)
                {
                    List<GameObject> connectedTiles = new List<GameObject>();
                    TileObjectInstance _currentTile = boardManager.grid[row, col].GetComponent<TileObjectInstance>();
                    if(!_currentTile.isVisited)
                    {
                        string tileColor = _currentTile.tileColor.ToString();
                        ExploreGridBoard(boardManager.grid, row, col, tileColor, connectedTiles);

                        for(int i = 0; i < connectedTiles.Count - 1; i++)
                        {
                            connectedTiles[i].gameObject.GetComponent<TileObjectInstance>().nextConnectedTile = connectedTiles[i + 1];
                            connectedTiles[i + 1].GetComponent<TileObjectInstance>().previousConnectedTile = connectedTiles[i];
                            changeConnectedTilesIcon.ChangeAllConnectedTilesIcon(connectedTiles, i, boardManager.A, boardManager.B, boardManager.C);
                        }
                    }
                    tileConnections.Add(connectedTiles.Count);
                }
            }
            // Is board need shuffle ?
            // If every int value in tileConnections is 1 that means every tile has only one connection.
            // Which is the tile itself so we need to shuffle.
            if(tileConnections.TrueForAll(x => x.Equals(tileConnections[0])))
            {
                shuffleBoard.ShuffleTheTiles();
            }
        }

        public void ExploreGridBoard(GameObject[,] grid, int row, int col, string tileColor, List<GameObject> connectedTiles)
        {
            // We use DFS algorithm in here.
            // If the row is within the grid area.
            // If the column is within the grid area.
            // If the tile color matches with the given parameter.
            // If the tile is NOT visited.
            // We sets the isVisited value to true, then adding the gameObject to connectedTiles list.
            // Finally we recursively call this function again with different grid and column arguments.

            if(!(row >= 0) || !(row < grid.GetLength(0))) { return; }
            if(!(col >= 0) || !(col < grid.GetLength(1))) { return; }

            TileObjectInstance _tile = grid[row,col].GetComponent<TileObjectInstance>();
            
            if(_tile.tileColor.ToString() != tileColor.ToString()) { return; }
            if(_tile.isVisited) {return; }

            _tile.isVisited = true;
            connectedTiles.Add(_tile.gameObject);

            ExploreGridBoard(grid, row + 1, col, tileColor, connectedTiles);
            ExploreGridBoard(grid, row - 1, col, tileColor, connectedTiles);
            ExploreGridBoard(grid, row, col + 1, tileColor, connectedTiles);
            ExploreGridBoard(grid, row, col - 1, tileColor, connectedTiles);
        }
    }
}

