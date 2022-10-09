using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoardManager : MonoBehaviour
{    
    [SerializeField][Range(2, 10)] private int rows;
    [SerializeField][Range(2, 10)] private int collums;

    [SerializeField] CinemachineVirtualCamera lookCamera;



    void Start()
    {
        BoardStartSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BoardStartSize()
    {
        if(rows % 2 == 1)
        {
            Vector3 newPos = new Vector3(rows / 2, collums / 2, 1f);  
            transform.position = newPos;
            lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
        }
        else
        {
            Vector3 newPos = new Vector3((rows / 2) - 0.5f, (collums / 2) - 0.5f, 1f);  
            transform.position = newPos;
            lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
        }
        transform.localScale = new Vector3(rows, collums, 1f);  
    }
}
