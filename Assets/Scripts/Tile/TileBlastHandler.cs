using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile.TileObject;
using Board;

namespace Tile.TileBlast
{
    public class TileBlastHandler : MonoBehaviour
    {   
        public void BlastAllConnectedTiles(BoardManager boardManager, GameObject currentTile, 
                                                  GameObject nextConnectedTile, GameObject previousConnectedTile)
        {
            boardManager.isTileClicked = true;

            GameObject _nextConnectedTile = nextConnectedTile;
            while(_nextConnectedTile != null)
            {
                _nextConnectedTile = _nextConnectedTile.GetComponent<TileObjectInstance>().nextConnectedTile;
                Destroy(_nextConnectedTile);
            }

            GameObject _previousConnectedTile = previousConnectedTile;
            while(_previousConnectedTile != null)
            {
                _previousConnectedTile = _previousConnectedTile.GetComponent<TileObjectInstance>().previousConnectedTile;
                Destroy(_previousConnectedTile);
            }

            Destroy(nextConnectedTile);
            Destroy(previousConnectedTile);
            Destroy(currentTile);
        }
    }
}
