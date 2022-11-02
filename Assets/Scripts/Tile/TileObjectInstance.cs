using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile.TileScriptableObject;

namespace Tile.TileObject
{
    public class TileObjectInstance : MonoBehaviour
    {
        Rigidbody tileRigidBody;
        BoardManager boardManager;
        
        public TileSO tileSO;

        public bool isVisited;
        public string tileColor;
        public GameObject nextConnectedTile;     // Next connection to same color tile.
        public GameObject previousConnectedTile;  // Previous connection to same color tile.

        private bool blastTheTile;
        public Material materialDefault;
        public Material materialA;
        public Material materialB;
        public Material materialC;

        
        
        private void Awake()
        {
            tileRigidBody = GetComponent<Rigidbody>();
            boardManager = FindObjectOfType<BoardManager>();

            CopyScriptableObjectContent();
            
        }
        private void FixedUpdate() 
        {
            // We don't allow any positive upward velocity.
            Mathf.Clamp(tileRigidBody.velocity.y, -10f, 0);
        }

        void Update()
        {
            if(blastTheTile)
            {
                // Destroy, every connection that tile has.

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
                Destroy(gameObject);
            }
        }

        private void OnMouseDown() 
        {
            // If tile has at least 2 connection and if the new grid is created.
            if((nextConnectedTile != null || previousConnectedTile != null) && boardManager.canClickAgain)
            {
                blastTheTile = true;
            }
        }

        private void CopyScriptableObjectContent()
        {
            isVisited = tileSO.isVisited;
            tileColor = tileSO.tileColor.ToString();
            nextConnectedTile = tileSO.nextConnectedTile;
            previousConnectedTile = tileSO.previousConnectedTile;
            blastTheTile = tileSO.blastTheTile;
            materialDefault = tileSO.materialDefault;
            materialA = tileSO.materialA;
            materialB = tileSO.materialB;
            materialC = tileSO.materialC;
        }
        
    }
}
