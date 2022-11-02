using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile.TileScriptableObject;
using Tile.TileBlast;
using Board;

namespace Tile.TileObject
{
    public class TileObjectInstance : MonoBehaviour
    {
        Rigidbody tileRigidBody;
        BoardManager boardManager;
        
        // Tile scriptable object reference.
        public TileSO tileSO;

        public string tileColor;
        public Material materialDefault;
        public Material materialA;
        public Material materialB;
        public Material materialC;
        
        public bool isVisited;
        private bool blastTheTile;

        public GameObject nextConnectedTile;      // Next connection to same color tile.
        public GameObject previousConnectedTile;  // Previous connection to same color tile.


        private void Awake()
        {
            tileRigidBody = GetComponent<Rigidbody>();
            boardManager = FindObjectOfType<BoardManager>();

            // The scriptable object content is copied to the current tile.
            CopyScriptableObjectContent();
        }

        private void FixedUpdate() 
        {
            // We don't allow any positive upward velocity.
            Mathf.Clamp(tileRigidBody.velocity.y, -10f, 0);
        }

        private void Update()
        {
            if(blastTheTile)
            {
                // Destroy, every connection that this tile has.
                TileBlastHandler.BlastAllConnectedTiles(boardManager, gameObject, nextConnectedTile, previousConnectedTile);
            }
        }

        private void OnMouseDown() 
        {
            // If tile has at least 2 connection and if the new grid creation completed.
            if((nextConnectedTile != null || previousConnectedTile != null) && boardManager.canClickAgain)
            {
                blastTheTile = true;
            }
        }

        private void CopyScriptableObjectContent()
        {
            tileColor = tileSO.tileColor.ToString();
            materialDefault = tileSO.materialDefault;
            materialA = tileSO.materialA;
            materialB = tileSO.materialB;
            materialC = tileSO.materialC;

            nextConnectedTile = tileSO.nextConnectedTile;
            previousConnectedTile = tileSO.previousConnectedTile;

            isVisited = tileSO.isVisited;
            blastTheTile = tileSO.blastTheTile;
        }
        
    }
}
