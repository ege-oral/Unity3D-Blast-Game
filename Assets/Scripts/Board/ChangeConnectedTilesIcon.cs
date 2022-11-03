using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile.TileObject;

namespace Board.ChangeTilesIcon
{
    public class ChangeConnectedTilesIcon : MonoBehaviour
    {
        public void ChangeAllConnectedTilesIcon(List<GameObject> connectedTiles, int i, int A, int B, int C)
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
