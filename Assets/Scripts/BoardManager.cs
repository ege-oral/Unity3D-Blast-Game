using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoardManager : MonoBehaviour
{    

    [Header("Game Rules")]
    [SerializeField][Range(2, 10)] private int numberOfRows;
    [SerializeField][Range(2, 10)] private int numberOfCollums;
    [SerializeField][Range(1, 6)] private int numberOfColors; 
    [SerializeField] private int A;
    [SerializeField] private int B;
    [SerializeField] private int C;

    GameObject [,] grid;
    [SerializeField] GameObject[] tiles;
    [SerializeField] GameObject allTileInstances;
    [SerializeField] CinemachineVirtualCamera lookCamera;

    List<int> arr = new List<int>();
    public bool isPlayerClicked = false;
    public bool canClickAgain = true;

    private void Awake() 
    {
        grid = new GameObject[numberOfRows, numberOfCollums];

        BoardStartSize();
        FillTheBoardOnLoad();
        FindSameTiles();
    }

    private void Update() {

        if(isPlayerClicked && canClickAgain)
        {
            canClickAgain = false;
            CheckIfGridChanged();
            StartCoroutine(FillMissingPlaces());
            
            isPlayerClicked = false;
        }
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

    private void FillTheBoardOnLoad()
    {   
        for(int row = 0; row < numberOfRows; row++)
        {
            for(int collum = 0; collum < numberOfCollums; collum++)
            {
                GameObject tile = Instantiate(tiles[Random.Range(0,numberOfColors)], 
                                              new Vector3(collum, row, 0f), 
                                              Quaternion.Euler(0f,0f,180f));
                grid[row, collum] = tile;
                tile.transform.parent = allTileInstances.transform;
            }
        }
    }

    private void FindSameTiles()
    {
        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                List<GameObject> connectedList = new List<GameObject>();
                GameObject currentTile = grid[row, col];
                if(!currentTile.GetComponent<Tile>().isVisited)
                {
                    string tileColor = currentTile.GetComponent<Tile>().tileColor;
                    ExploreGrid(grid, row, col, tileColor, connectedList);

                    for(int i = 0; i < connectedList.Count - 1; i++)
                    {
                        connectedList[i].gameObject.GetComponent<Tile>().nextConnectedTile = connectedList[i + 1];
                        connectedList[i + 1].GetComponent<Tile>().previousConnectedTile = connectedList[i];
                    }
                }
                print(connectedList.Count);
            }
        }
    }

    private void ExploreGrid(GameObject[,] grid, int row, int col, 
                         string tileColor, List<GameObject> connectedList)
    {
        if(!(row >= 0) || !(row < grid.GetLength(0))) { return; }
        if(!(col >= 0) || !(col < grid.GetLength(1))) { return; }
        if(grid[row,col].GetComponent<Tile>().tileColor != tileColor) { return; }
        if(grid[row,col].GetComponent<Tile>().isVisited) {return; }
        grid[row,col].GetComponent<Tile>().isVisited = true;
        connectedList.Add(grid[row,col]);

        ExploreGrid(grid, row + 1, col, tileColor, connectedList);
        ExploreGrid(grid, row - 1, col, tileColor, connectedList);
        ExploreGrid(grid, row, col + 1, tileColor, connectedList);
        ExploreGrid(grid, row, col - 1, tileColor, connectedList);
    }


    private void CheckIfGridChanged()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                if(grid[i, j] == null)
                {
                    arr.Add(j);
                }
            }
        }
    }

    IEnumerator FillMissingPlaces()
    {
        GameObject newTile = null;
        foreach(int missingCollum in arr)
        {
            newTile = Instantiate(tiles[Random.Range(0,numberOfColors)],
                                             new Vector3(missingCollum, gameObject.transform.position.y * 2 + 2, 0f),
                                             Quaternion.Euler(0f,0f,180f));
            newTile.transform.parent = allTileInstances.transform;
            yield return new WaitForSeconds(0.05f);
        }
        arr.Clear();
        
        yield return new WaitForSeconds(1f);
        CreateNewMatrix();
    }

    
    private void CreateNewMatrix()
    {
        print("yes");
        GameObject[,] newGrid = new GameObject[numberOfRows, numberOfCollums];
        foreach(Transform tile in allTileInstances.transform)
        {   
            int tileXPos = Mathf.RoundToInt(tile.gameObject.transform.position.x);
            int tileYPos = Mathf.RoundToInt(tile.gameObject.transform.position.y);
            tile.gameObject.GetComponent<Tile>().nextConnectedTile = null;
            tile.gameObject.GetComponent<Tile>().previousConnectedTile = null;
            tile.gameObject.GetComponent<Tile>().isVisited = false;
            newGrid[tileYPos, tileXPos] = tile.gameObject;
        }
        grid = newGrid;
        FindSameTiles();
        canClickAgain = true;
        print("now");
    }
}
