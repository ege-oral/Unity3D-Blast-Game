using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Board.Generate;
using Board.FindTiles;
using Board.GridBoard;
using Tile.TileObject;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {    
        [Header("Game Rules")]
        [SerializeField][Range(2, 10)] private readonly int numberOfRows = 8;     // M
        [SerializeField][Range(2, 10)] private readonly int numberOfCollums = 8;  // N
        [SerializeField][Range(1, 6)] private readonly int numberOfColors = 4;    // K

        public int NumberOfRows { get { return numberOfRows; } }
        public int NumberOfCollums { get { return numberOfCollums; } }
        public int NumberOfColors { get { return numberOfColors; } }

        [SerializeField] private readonly int a = 4;
        [SerializeField] private readonly int b = 7;
        [SerializeField] private readonly int c = 9;

        public int A { get { return a; } }
        public int B { get { return b; } }
        public int C { get { return c; } }


        public GameObject [,] grid;
        public GameObject[] tiles;                             // Contains tile prefabs.
        public GameObject allTileInstances;                    // The object that holds all the tiles inside.
        public CinemachineVirtualCamera lookCamera;

        List<int> missingCollums = new List<int>();
        GameObject lastCreatedTile = null;

        public bool canClickAgain = true;
        public bool isTileClicked = false;
        public bool shuffle = false;
        
        GenerateBoard generateBoard;
        FindConnectedTiles findConnectedTiles;
        UpdateGridBoard updateGridBoard;

        private void Start() 
        {
            grid = new GameObject[numberOfRows, numberOfCollums];

            // We adjust camera position for dynamic sizing grid.
            BoardAndCameraStartSize();

            updateGridBoard = GetComponent<UpdateGridBoard>();
            
            generateBoard = GetComponent<GenerateBoard>();    
            generateBoard.FillTheBoard();
            
            findConnectedTiles = GetComponent<FindConnectedTiles>();
            findConnectedTiles.FindAllConnectedTiles();
        }

        private void Update() 
        {
            if(isTileClicked && canClickAgain)
            {
                canClickAgain = false;
                FindMissingCollumPositions();
                StartCoroutine(FillMissingPlaces());
            }

            if(lastCreatedTile != null && lastCreatedTile.transform.position.y <= numberOfRows - 1)
            {
                updateGridBoard.CreateNewMatrix();
                lastCreatedTile = null;
            }
        }

        private void FindMissingCollumPositions()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    if(grid[i, j] == null)
                    {
                        missingCollums.Add(j);
                    }
                }
            }
        }

        IEnumerator FillMissingPlaces()
        {
            GameObject newTile = null;
            foreach(int missingCollum in missingCollums)
            {
                newTile = Instantiate(tiles[Random.Range(0,numberOfColors)],
                                                new Vector3(missingCollum, gameObject.transform.position.y * 2 + 2, 0f),
                                                Quaternion.Euler(0f,0f,180f));
                newTile.transform.parent = allTileInstances.transform;
                yield return new WaitForSeconds(0.1f);
            }
            missingCollums.Clear();

            // We keeps track of last created tile.
            // Later we will need this object's value position.
            lastCreatedTile = newTile;
        }

        // private void CreateNewMatrix()
        // {
        //     GameObject[,] newGrid = new GameObject[numberOfRows, numberOfCollums];
        //     // We resets every gameObject's connections, visited value and material values.
        //     // Then create another grid.
        //     foreach(Transform tile in allTileInstances.transform)
        //     {   
        //         int tileXPos = Mathf.RoundToInt(tile.gameObject.transform.position.x);
        //         int tileYPos = Mathf.RoundToInt(tile.gameObject.transform.position.y);
        //         tile.gameObject.GetComponent<TileObjectInstance>().nextConnectedTile = null;
        //         tile.gameObject.GetComponent<TileObjectInstance>().previousConnectedTile = null;
        //         tile.gameObject.GetComponent<TileObjectInstance>().isVisited = false;
        //         tile.gameObject.GetComponent<MeshRenderer>().material = tile.gameObject.GetComponent<TileObjectInstance>().materialDefault;
        //         newGrid[tileYPos, tileXPos] = tile.gameObject;
        //     }

        //     grid = (GameObject[,])newGrid.Clone();
        //     findConnectedTiles.FindAllConnectedTiles();
        // }

        private void BoardAndCameraStartSize()
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
    }
}
