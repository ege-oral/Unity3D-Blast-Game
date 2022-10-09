using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoardManager : MonoBehaviour
{    
    [SerializeField] CinemachineVirtualCamera lookCamera;

    [SerializeField][Range(2, 10)] private int numberOfRows;
    [SerializeField][Range(2, 10)] private int numberOfCollums;
    [SerializeField][Range(1, 6)] private int numberOfColors; 

    [SerializeField] GameObject[] tiles;

    void Start()
    {
        BoardStartSize();
        FillTheBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BoardStartSize()
    {
        if(numberOfRows % 2 == 1)
        {
            Vector3 newPos = new Vector3(transform.position.x, numberOfRows / 2, 1f);  
            transform.position = newPos;
            lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
        }
        else
        {
            Vector3 newPos = new Vector3(transform.position.x, (numberOfRows / 2) - 0.5f, 1f);
            transform.position = newPos;
            lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
        }

        if(numberOfCollums % 2 == 1)
        {
            Vector3 newPos = new Vector3(numberOfCollums / 2, transform.position.y, 1f);  
            transform.position = newPos;
            lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
        }

        else
        {
            Vector3 newPos = new Vector3((numberOfCollums / 2) - 0.5f, transform.position.y, 1f); 
            transform.position = newPos;
            lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
        }
        transform.localScale = new Vector3(numberOfCollums, numberOfRows, 1f);  
    }

    private void FillTheBoard()
    {   
        for(int row = 0; row < numberOfRows; row++)
        {
            for(int collum = 0; collum < numberOfCollums; collum++)
            {
                Instantiate(tiles[Random.Range(0,numberOfColors)], new Vector3(collum, row, 0f), Quaternion.identity);
            }
        }
    }
}
