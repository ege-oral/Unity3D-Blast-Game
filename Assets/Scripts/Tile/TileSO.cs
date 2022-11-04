using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile.TileScriptableObject
{
    [CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
    public class TileSO : ScriptableObject
    {
        public enum TileColor
        {
            Yellow,
            Blue,
            Green,
            Pink,
            Purple,
            Red
        }
        public TileColor tileColor;

        public Material materialDefault;
        public Material materialA;
        public Material materialB;
        public Material materialC;

        [HideInInspector] public bool isVisited = false;
        [HideInInspector] public bool blastTheTile = false;
        
        [HideInInspector] public GameObject nextConnectedTile = null;      // Next connection to same color tile.
        [HideInInspector] public GameObject previousConnectedTile = null;  // Previous connection to same color tile.
    }
}
