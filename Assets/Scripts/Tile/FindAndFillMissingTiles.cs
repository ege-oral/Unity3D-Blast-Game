using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Board;

namespace Tile.FindAndFillTiles
{
    public class FindAndFillMissingTiles : MonoBehaviour
    {
        BoardManager boardManager;
        private List<int> missingColumns = new List<int>();
        

        private void Start() => boardManager = GetComponent<BoardManager>();

        public void FindAndFillAllMissingColumns()
        {
            FindMissingTilesColumnPositions();

            // After finding all missing columns, we fill all of them.
            StartCoroutine(FillMissingTileColumnPositions());
        }

        private void FindMissingTilesColumnPositions()
        {
            for (int i = 0; i < boardManager.grid.GetLength(0); i++)
            {
                for(int j = 0; j < boardManager.grid.GetLength(1); j++)
                {
                    if(boardManager.grid[i, j] == null)
                    {
                        missingColumns.Add(j);
                    }
                }
            }
        }

        private IEnumerator FillMissingTileColumnPositions()
        {
            GameObject newlyCreatedTile = null;
            foreach(int missingColumn in missingColumns)
            {
                newlyCreatedTile = Instantiate(boardManager.tiles[Random.Range(0, boardManager.NumberOfColors)],
                                      new Vector3(missingColumn, gameObject.transform.position.y * 2 + 2, 0f),
                                      Quaternion.Euler(0f,0f,180f));

                newlyCreatedTile.transform.parent = boardManager.allTileInstances.transform;
                yield return new WaitForSeconds(0.1f);
            }
            // After filling the missing columns we clear missingColumns List.
            missingColumns.Clear();

            // We keeps track of last created tile.
            // Later we will need this object's value position.
            boardManager.lastCreatedTile = newlyCreatedTile;
        }
    }
}
