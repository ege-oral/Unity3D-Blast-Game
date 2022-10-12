using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isVisited = false;
    [SerializeField] public string tileColor;
    public GameObject nextConnectedTile = null;        // Next connection to same color tile.
    public GameObject previousConnectedTile = null;    // Previous connection to same color tile.

    Rigidbody tileRigidBody;
    BoardManager boardManager;

    [SerializeField] public Material materialDefault;
    [SerializeField] public Material materialA;
    [SerializeField] public Material materialB;
    [SerializeField] public Material materialC;
    
    private bool blastTheTile = false;

    
    void Start()
    {
        tileRigidBody = GetComponent<Rigidbody>();
        boardManager = FindObjectOfType<BoardManager>();
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
                _nextConnectedTile = _nextConnectedTile.GetComponent<Tile>().nextConnectedTile;
                Destroy(_nextConnectedTile);
            }

            GameObject _previousConnectedTile = previousConnectedTile;
            while(_previousConnectedTile != null)
            {
                _previousConnectedTile = _previousConnectedTile.GetComponent<Tile>().previousConnectedTile;
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
}
