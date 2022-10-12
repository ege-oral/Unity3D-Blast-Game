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
    [SerializeField] private int A = 4;
    [SerializeField] private int B = 7;
    [SerializeField] private int C = 9;

    GameObject [,] grid;
    [SerializeField] GameObject[] tiles;
    [SerializeField] GameObject allTileInstances;
    [SerializeField] CinemachineVirtualCamera lookCamera;

    List<int> missingCollums = new List<int>();
    public bool isPlayerClicked = false;
    public bool canClickAgain = true;
    public bool shuffle = false;

    GameObject lastCreatedTile = null;

    private void Awake() 
    {
        grid = new GameObject[numberOfRows, numberOfCollums];

        BoardAndCameraStartSize();
        FillTheBoard();
        FindSameTiles();
    }

    private void Update() 
    {

        if(isPlayerClicked && canClickAgain)
        {
            canClickAgain = false;
            FindMissingCollumPositions();
            StartCoroutine(FillMissingPlaces());
        }
        if(lastCreatedTile != null && lastCreatedTile.transform.position.y <= numberOfRows - 1)
        {
            CreateNewMatrix();
            lastCreatedTile = null;
        }
    }


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

    private void FillTheBoard()
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
        List<int> theNumberOfConnectionThatTileHas = new List<int>();
        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                List<GameObject> connectedTiles = new List<GameObject>();
                GameObject currentTile = grid[row, col];
                if(!currentTile.GetComponent<Tile>().isVisited)
                {
                    string tileColor = currentTile.GetComponent<Tile>().tileColor.ToString();
                    ExploreGrid(grid, row, col, tileColor, connectedTiles);

                    for(int i = 0; i < connectedTiles.Count - 1; i++)
                    {
                        connectedTiles[i].gameObject.GetComponent<Tile>().nextConnectedTile = connectedTiles[i + 1];
                        connectedTiles[i + 1].GetComponent<Tile>().previousConnectedTile = connectedTiles[i];

                        ChangeTilesIcon(connectedTiles, i);
                    }
                }
                theNumberOfConnectionThatTileHas.Add(connectedTiles.Count);
            }
        }
        CheckIfTilesNeedShuffle(theNumberOfConnectionThatTileHas);
        
        
        canClickAgain = true;
        isPlayerClicked = false;
    }

    private void ExploreGrid(GameObject[,] grid, int row, int col, 
                         string tileColor, List<GameObject> connectedTiles)
    {
        if(!(row >= 0) || !(row < grid.GetLength(0))) { return; }
        if(!(col >= 0) || !(col < grid.GetLength(1))) { return; }

        Tile _tile = grid[row,col].GetComponent<Tile>();
        
        if(_tile.tileColor.ToString() != tileColor.ToString()) { return; }
        if(_tile.isVisited) {return; }

        _tile.isVisited = true;
        connectedTiles.Add(_tile.gameObject);

        ExploreGrid(grid, row + 1, col, tileColor, connectedTiles);
        ExploreGrid(grid, row - 1, col, tileColor, connectedTiles);
        ExploreGrid(grid, row, col + 1, tileColor, connectedTiles);
        ExploreGrid(grid, row, col - 1, tileColor, connectedTiles);
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
            yield return new WaitForSeconds(0.2f);
        }
        missingCollums.Clear();
        lastCreatedTile = newTile;
    }

    private void CreateNewMatrix()
    {
        GameObject[,] newGrid = new GameObject[numberOfRows, numberOfCollums];
        foreach(Transform tile in allTileInstances.transform)
        {   
            int tileXPos = Mathf.RoundToInt(tile.gameObject.transform.position.x);
            int tileYPos = Mathf.RoundToInt(tile.gameObject.transform.position.y);
            tile.gameObject.GetComponent<Tile>().nextConnectedTile = null;
            tile.gameObject.GetComponent<Tile>().previousConnectedTile = null;
            tile.gameObject.GetComponent<Tile>().isVisited = false;
            tile.gameObject.GetComponent<MeshRenderer>().material = tile.gameObject.GetComponent<Tile>().materialDefault;
            newGrid[tileYPos, tileXPos] = tile.gameObject;
        }

        grid = (GameObject[,])newGrid.Clone();
        FindSameTiles();
    }

    private void ChangeTilesIcon(List<GameObject> connectedTiles, int i)
    {
        int numberOfTileInConnectedList = connectedTiles.Count;

        Tile _currentTile = connectedTiles[i].GetComponent<Tile>();

        MeshRenderer currentTileMeshRenderer = connectedTiles[i].GetComponent<MeshRenderer>();
        MeshRenderer nextTileMeshRenderer = connectedTiles[i + 1].GetComponent<MeshRenderer>();

        if(numberOfTileInConnectedList <= A)
        {
            currentTileMeshRenderer.material = _currentTile.materialDefault;
            nextTileMeshRenderer.material = _currentTile.materialDefault;
        }
        else if(numberOfTileInConnectedList > A && numberOfTileInConnectedList <= B)
        {
            currentTileMeshRenderer.material = _currentTile.materialA;
            nextTileMeshRenderer.material = _currentTile.materialA;
        }
        else if(numberOfTileInConnectedList > B && numberOfTileInConnectedList <= C)
        {
            currentTileMeshRenderer.material = _currentTile.materialB;
            nextTileMeshRenderer.material = _currentTile.materialB;
        }
        else
        {
            currentTileMeshRenderer.material = _currentTile.materialC;
            nextTileMeshRenderer.material = _currentTile.materialC;
        }
    }

    private void CheckIfTilesNeedShuffle(List<int> theNumberOfConnectionThatTileHas)
    {
        if(theNumberOfConnectionThatTileHas.TrueForAll(x => x.Equals(theNumberOfConnectionThatTileHas[0])))
            shuffle = true;
        else
            shuffle = false;

        if(shuffle)
            ShuffleTheTiles();
    }

    private void ShuffleTheTiles()
    {
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                Destroy(grid[i,j]);
            }
        }
        FillTheBoard();
        FindSameTiles();
    }
}
