using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public string tileColor;
    public bool isVisited = false;

    public GameObject nextConnectedTile;
    public GameObject previousConnectedTile;



    Rigidbody tileRigidBody;
    private bool preventBounce = false;
    private bool blast = false;
    
    void Start()
    {
        tileRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(preventBounce)
        {
            tileRigidBody.velocity = Vector3.zero;
        }
    }
    void Update()
    {
        if(blast)
        {
            GameObject tmp = nextConnectedTile;
            while(tmp != null)
            {
                tmp = tmp.GetComponent<Tile>().nextConnectedTile;
                Destroy(tmp);
            }
            GameObject tmp2 = previousConnectedTile;
            while(tmp2 != null)
            {
                tmp2 = tmp2.GetComponent<Tile>().previousConnectedTile;
                Destroy(tmp2);
            }
            Destroy(nextConnectedTile);
            Destroy(previousConnectedTile);
            Destroy(gameObject);
   

        }
    }

    private void OnCollisionEnter(Collision other) 
    {

        if(other.gameObject.tag == "Tile" && other.gameObject.tag == "Floor")
            preventBounce = true;
    }

    private void OnMouseDown() 
    {
        print(gameObject.name);
        blast = true;
    }

}
