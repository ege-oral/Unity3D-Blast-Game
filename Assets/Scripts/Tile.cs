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

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        if(tileRigidBody.velocity.y > 0f && !preventBounce)
        {
            preventBounce = true;
            tileRigidBody.AddForce(new Vector3(0f, -2f, 0f), ForceMode.Impulse);
        }
    }

}
