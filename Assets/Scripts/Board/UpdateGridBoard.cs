using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Board.FindTiles;
using Tile.TileObject;

namespace Board.GridBoard
{
    public class UpdateGridBoard : MonoBehaviour
    {
        BoardManager boardManager;
        FindConnectedTiles findConnectedTiles;

        private void Start() 
        {
            boardManager = GetComponent<BoardManager>();
            findConnectedTiles = GetComponent<FindConnectedTiles>();
        }

        public void CreateNewGridBoard()
        {
            GameObject[,] newGrid = new GameObject[boardManager.NumberOfRows, boardManager.NumberOfColumns];
            // We resets every gameObject's connections, visited value and material values.
            // Then create another grid.
            foreach(Transform tile in boardManager.allTileInstances.transform)
            {   
                TileObjectInstance _currentTile = tile.gameObject.GetComponent<TileObjectInstance>();
                Vector3 _currentTilesPosition = tile.transform.position;

                int tileXPos = Mathf.RoundToInt(_currentTilesPosition.x);
                int tileYPos = Mathf.RoundToInt(_currentTilesPosition.y);

                _currentTile.nextConnectedTile = null;
                _currentTile.previousConnectedTile = null;
                _currentTile.isVisited = false;
                
                tile.gameObject.GetComponent<MeshRenderer>().material = _currentTile.materialDefault;
                newGrid[tileYPos, tileXPos] = _currentTile.gameObject;
            }

            boardManager.grid = (GameObject[,])newGrid.Clone();
            findConnectedTiles.FindAllConnectedTiles();
        }

    }
}
