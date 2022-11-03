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
                int tileXPos = Mathf.RoundToInt(tile.gameObject.transform.position.x);
                int tileYPos = Mathf.RoundToInt(tile.gameObject.transform.position.y);
                tile.gameObject.GetComponent<TileObjectInstance>().nextConnectedTile = null;
                tile.gameObject.GetComponent<TileObjectInstance>().previousConnectedTile = null;
                tile.gameObject.GetComponent<TileObjectInstance>().isVisited = false;
                tile.gameObject.GetComponent<MeshRenderer>().material = tile.gameObject.GetComponent<TileObjectInstance>().materialDefault;
                newGrid[tileYPos, tileXPos] = tile.gameObject;
            }

            boardManager.grid = (GameObject[,])newGrid.Clone();
            findConnectedTiles.FindAllConnectedTiles();
        }

    }
}
