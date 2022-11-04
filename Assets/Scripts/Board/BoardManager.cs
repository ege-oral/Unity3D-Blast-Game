using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board.Generate;
using Board.BoardSize;
using Board.GridBoard;
using Tile.FindConnections;
using Tile.FindAndFillTiles;

namespace Board
{
    public class BoardManager : MonoBehaviour
    {    
        [Header("Game Rules")]
        [SerializeField][Range(2, 10)] private int numberOfRows = 8;     // M
        [SerializeField][Range(2, 10)] private int numberOfColumns = 8;  // N
        [SerializeField][Range(1, 6)] private int numberOfColors = 4;    // K

        public int NumberOfRows { get { return numberOfRows; } }
        public int NumberOfColumns { get { return numberOfColumns; } }
        public int NumberOfColors { get { return numberOfColors; } }

        [SerializeField] private int a = 4;
        [SerializeField] private int b = 7;
        [SerializeField] private int c = 9;

        public int A { get { return a; } }
        public int B { get { return b; } }
        public int C { get { return c; } }

        public GameObject[,] grid;
        public GameObject[] tiles;               // Contains tile prefabs.
        public GameObject allTileInstances;      // The object that holds all the tiles inside.
        public GameObject lastCreatedTile = null;
        
        public bool canClickAgain = true;
        public bool isTileClicked = false;
        public bool shuffle = false;
        
        GenerateBoard generateBoard;
        UpdateGridBoard updateGridBoard;
        FindConnectedTiles findConnectedTiles;
        BoardAndCameraSize boardAndCameraSize;
        FindAndFillMissingTiles findAndFillMissingColumns;

        private void Start() 
        {
            grid = new GameObject[numberOfRows, numberOfColumns];

            generateBoard = GetComponent<GenerateBoard>();    
            updateGridBoard = GetComponent<UpdateGridBoard>();
            boardAndCameraSize = GetComponent<BoardAndCameraSize>();
            findConnectedTiles = GetComponent<FindConnectedTiles>();
            findAndFillMissingColumns = GetComponent<FindAndFillMissingTiles>();

            // We adjust camera position for dynamic sizing grid.
            boardAndCameraSize.BoardAndCameraStartSize();
            generateBoard.FillTheBoard();
            findConnectedTiles.FindAllConnectedTiles();
        }

        private void Update() 
        {
            GameProcess();
        }

        private void GameProcess()
        {
            if(isTileClicked && canClickAgain)
            {
                canClickAgain = false;
                findAndFillMissingColumns.FindAndFillAllMissingColumns();
            }

            // After last created tile place it's position we create a new grid board with new values. 
            if(lastCreatedTile != null && lastCreatedTile.transform.position.y <= numberOfRows - 1)
            {
                updateGridBoard.CreateNewGridBoard();
                lastCreatedTile = null;
                canClickAgain = true;
                isTileClicked = false;
            }
        }
    }
}
