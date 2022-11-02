using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tile.TileObject;

namespace Board.FindTiles
{    
    public class FindConnectedTiles : MonoBehaviour
    {
        public static void FindAllConnectedTiles(GameObject[,] grid, ref bool canClickAgain, ref bool isTileClicked, int A, int B, int C)
        {
            // If every int value in theNumberOfConnectionThatTileHas is 1 that means every tile has only one connection.
            // Which is the tile itself so we need to shuffle.
            List<int> theNumberOfConnectionThatTileHas = new List<int>();
            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int col = 0; col < grid.GetLength(1); col++)
                {
                    List<GameObject> connectedTiles = new List<GameObject>();
                    GameObject currentTile = grid[row, col];
                    if(!currentTile.GetComponent<TileObjectInstance>().isVisited)
                    {
                        string tileColor = currentTile.GetComponent<TileObjectInstance>().tileColor.ToString();
                        ExploreGrid(grid, row, col, tileColor, connectedTiles);

                        for(int i = 0; i < connectedTiles.Count - 1; i++)
                        {
                            connectedTiles[i].gameObject.GetComponent<TileObjectInstance>().nextConnectedTile = connectedTiles[i + 1];
                            connectedTiles[i + 1].GetComponent<TileObjectInstance>().previousConnectedTile = connectedTiles[i];

                            ChangeTilesIcon(connectedTiles, i, A, B, C);
                        }
                    }
                    theNumberOfConnectionThatTileHas.Add(connectedTiles.Count);
                }
            }
            //CheckIfTilesNeedShuffle(theNumberOfConnectionThatTileHas);
            
            canClickAgain = true;
            isTileClicked = false;
        }

        public static void ExploreGrid(GameObject[,] grid, int row, int col, 
                                string tileColor, List<GameObject> connectedTiles)
        {
            // We use DFS algorithm in here.
            // If the row is within the grid area.
            // If the collum is within the grid area.
            // If the tile color matches with the given parameter.
            // If the tile is NOT visited.
            // We sets the isVisited value to true, then adding the gameObject to connectedTiles list.
            // Finally we recursively call this function again with different grid and collum arguments.

            if(!(row >= 0) || !(row < grid.GetLength(0))) { return; }
            if(!(col >= 0) || !(col < grid.GetLength(1))) { return; }

            TileObjectInstance _tile = grid[row,col].GetComponent<TileObjectInstance>();
            
            if(_tile.tileColor.ToString() != tileColor.ToString()) { return; }
            if(_tile.isVisited) {return; }

            _tile.isVisited = true;
            connectedTiles.Add(_tile.gameObject);

            ExploreGrid(grid, row + 1, col, tileColor, connectedTiles);
            ExploreGrid(grid, row - 1, col, tileColor, connectedTiles);
            ExploreGrid(grid, row, col + 1, tileColor, connectedTiles);
            ExploreGrid(grid, row, col - 1, tileColor, connectedTiles);
        }
        
        private static void ChangeTilesIcon(List<GameObject> connectedTiles, int i, int A, int B, int C)
        {
            int numberOfTileInConnectedList = connectedTiles.Count;

            TileObjectInstance _currentTile = connectedTiles[i].GetComponent<TileObjectInstance>();

            MeshRenderer currentTileMeshRenderer = connectedTiles[i].GetComponent<MeshRenderer>();
            MeshRenderer nextTileMeshRenderer = connectedTiles[i + 1].GetComponent<MeshRenderer>();

            if(numberOfTileInConnectedList <= A)
            {
                currentTileMeshRenderer.material = _currentTile.materialDefault;
                nextTileMeshRenderer.material = _currentTile.materialDefault;
            }
            else if(numberOfTileInConnectedList > A && numberOfTileInConnectedList <= B)
            {
                currentTileMeshRenderer.material = _currentTile.materialA;
                nextTileMeshRenderer.material = _currentTile.materialA;
            }
            else if(numberOfTileInConnectedList > B && numberOfTileInConnectedList <= C)
            {
                currentTileMeshRenderer.material = _currentTile.materialB;
                nextTileMeshRenderer.material = _currentTile.materialB;
            }
            else
            {
                currentTileMeshRenderer.material = _currentTile.materialC;
                nextTileMeshRenderer.material = _currentTile.materialC;
            }
        }
    }


}

