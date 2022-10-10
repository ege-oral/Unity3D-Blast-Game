using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatrix : MonoBehaviour
{
    //public Dictionary<Vector2, GameObject> grid = new Dictionary<Vector2, GameObject>();
    public GameObject[,] grid = new GameObject[5,5];

    void Start()
    {
        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                List<GameObject> connectedList = new List<GameObject>();
                GameObject currentTile = grid[row, col];
                string tileColor = currentTile.GetComponent<Tile>().tileColor;
                Explore(grid, row, col, tileColor, connectedList);
                foreach(GameObject g in connectedList)
                    print(g);
                print("test");

            }
        }
    }

    void Update()
    {
        
    }

    private void FindSameTiles()
    {
        // foreach(KeyValuePair<Vector2, GameObject> kvp in grid)
        // {
        //     print(kvp.Key.x);
        //     Explore(kvp);
        // }
    }

    private void Explore(GameObject[,] grid, int row, int col, 
                         string tileColor, List<GameObject> connectedList)
    {
        if(!(row >= 0) || !(row < grid.GetLength(0))) { return; }
        if(!(col >= 0) || !(col < grid.GetLength(1))) { return; }
        if(grid[row,col].GetComponent<Tile>().tileColor != tileColor) { return; }
        if(grid[row,col].GetComponent<Tile>().isVisited) {return; }
        grid[row,col].GetComponent<Tile>().isVisited = true;
        connectedList.Add(grid[row,col]);

        Explore(grid, row + 1, col, tileColor, connectedList);
        Explore(grid, row - 1, col, tileColor, connectedList);
        Explore(grid, row, col + 1, tileColor, connectedList);
        Explore(grid, row, col - 1, tileColor, connectedList);

        
    }
}
