using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isVisited = false;
    [SerializeField] public string tileColor;
    public GameObject nextConnectedTile = null;
    public GameObject previousConnectedTile = null;


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

    void Update()
    {
        if(blastTheTile)
        {
            boardManager.isPlayerClicked = true;
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
        if((nextConnectedTile != null || previousConnectedTile != null) && boardManager.canClickAgain)
            blastTheTile = true;
    }
}
