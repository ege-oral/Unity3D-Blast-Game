using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatrix : MonoBehaviour
{
    // TO DO change 5,5.
    int row;
    int collum;
    public GameObject[,] grid = new GameObject[10,10];

    void Awake()
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
                    Explore(grid, row, col, tileColor, connectedList);

                    for(int i = 0; i < connectedList.Count - 1; i++)
                    {
                        connectedList[i].gameObject.GetComponent<Tile>().nextConnectedTile = connectedList[i + 1];
                        connectedList[i + 1].GetComponent<Tile>().previousConnectedTile = connectedList[i];
                    }

                }
            }
        }
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
