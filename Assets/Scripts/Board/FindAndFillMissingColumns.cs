using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Board.FindAndFillColumns
{
    public class FindAndFillMissingColumns : MonoBehaviour
    {
        BoardManager boardManager;

        private List<int> missingColumns = new List<int>();
        

        private void Start() => boardManager = GetComponent<BoardManager>();

        public void FindAndFillAllMissingColumns()
        {
            FindMissingCollumPositions();
            // After finding all missing columns, we fill all of them.
            StartCoroutine(FillMissingCollumPositions());
            
        }

        private void FindMissingCollumPositions()
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

        private IEnumerator FillMissingCollumPositions()
        {
            GameObject newTile = null;
            foreach(int missingColumn in missingColumns)
            {
                newTile = Instantiate(boardManager.tiles[Random.Range(0, boardManager.NumberOfColors)],
                                                new Vector3(missingColumn, gameObject.transform.position.y * 2 + 2, 0f),
                                                Quaternion.Euler(0f,0f,180f));
                newTile.transform.parent = boardManager.allTileInstances.transform;
                yield return new WaitForSeconds(0.1f);
            }
            missingColumns.Clear();

            // We keeps track of last created tile.
            // Later we will need this object's value position.
            boardManager.lastCreatedTile = newTile;
        }
    }
}
