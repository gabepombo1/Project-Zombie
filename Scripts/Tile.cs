
    using UnityEditor.Animations;
    using UnityEditor.SceneManagement;
    using UnityEngine;
    public class Tile : Component
    {

        public int tileColumn; // (x coordinate)
        public int tileRow; // (y coordinate)
        [SerializeField] public Sprite tileSprite;
        [SerializeField] public string TileTerrainID;
        [SerializeField] public string TileNumberID;
        public enum TileState
        {
            
            CLAIMED,
            
            UNCLAIMED

        }

        //what would be in this constructor?
        public Tile()
        {
            this.tileXCoord = tileXCoord;
            //tileXCoord;
            //tileYCoord;

        }

    }