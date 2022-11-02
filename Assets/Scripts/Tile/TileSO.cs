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

        internal bool isVisited = false;
        internal bool blastTheTile = false;
        
        internal GameObject nextConnectedTile = null;      // Next connection to same color tile.
        internal GameObject previousConnectedTile = null;  // Previous connection to same color tile.
        
    }
}
