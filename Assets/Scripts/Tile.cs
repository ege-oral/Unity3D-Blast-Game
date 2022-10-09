using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Rigidbody tileRigidBody;
    
    private bool preventBounce = false;
    
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
        
    }

    private void OnCollisionEnter(Collision other) 
    {

        if(other.gameObject.tag == "Tile" && other.gameObject.tag == "Floor")
            preventBounce = true;
    }

    private void OnMouseDown() 
    {
        print(gameObject.name);    
    }

}
