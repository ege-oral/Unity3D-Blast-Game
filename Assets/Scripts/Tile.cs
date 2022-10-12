using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isVisited = false;
    [SerializeField] public string tileColor;
    public GameObject nextConnectedTile = null;
    public GameObject previousConnectedTile = null;

    private bool blast = false;

    Rigidbody tileRigidBody;
    private bool preventBounce = false;

    [SerializeField] public Material materialDefault;
    [SerializeField] public Material materialA;
    [SerializeField] public Material materialB;
    [SerializeField] public Material materialC;
    

    BoardManager boardManager;
    
    void Start()
    {
        tileRigidBody = GetComponent<Rigidbody>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    private void FixedUpdate() 
    {
        if(preventBounce)
        {
            tileRigidBody.velocity = Vector3.zero;
            return;

        }
    }
    void Update()
    {
        if(blast)
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

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Tile" || other.gameObject.tag == "Floor")
        {
            //preventBounce = true;
        }
    }

    private void OnMouseDown() 
    {
        if((nextConnectedTile != null || previousConnectedTile != null) && boardManager.canClickAgain)
            blast = true;
    }

    private void ChangeIcon()
    {

    }

}
