using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Board.BoardSize
{
    public class BoardAndCameraSize : MonoBehaviour
    {
        BoardManager boardManager;
        [SerializeField] CinemachineVirtualCamera lookCamera;

        private void Start() => boardManager = GetComponent<BoardManager>();

        public void BoardAndCameraStartSize()
        {
            if(boardManager.NumberOfRows % 2 == 1)
            {
                Vector3 newPos = new Vector3(transform.position.x, boardManager.NumberOfRows / 2, 1f);  
                transform.position = newPos;
                lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
            }
            else
            {
                Vector3 newPos = new Vector3(transform.position.x, (boardManager.NumberOfRows / 2) - 0.5f, 1f);
                transform.position = newPos;
                lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
            }

            if(boardManager.NumberOfColumns % 2 == 1)
            {
                Vector3 newPos = new Vector3(boardManager.NumberOfColumns / 2, transform.position.y, 1f);  
                transform.position = newPos;
                lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
            }
            else
            {
                Vector3 newPos = new Vector3((boardManager.NumberOfColumns / 2) - 0.5f, transform.position.y, 1f); 
                transform.position = newPos;
                lookCamera.transform.position = new Vector3(newPos.x, newPos.y, -1f);
            }

            transform.localScale = new Vector3(boardManager.NumberOfColumns, boardManager.NumberOfRows, 1f);  
        }
    }
}
