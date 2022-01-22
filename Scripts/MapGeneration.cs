using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using Vector2 = UnityEngine.Vector2;

public class MapGeneration
{
    
    //public float[][] worldCoordinates;
    public int maxWorldSize = 10000;
    public int maxWorldX = 100;
    public int maxWorldY = 100;
    public GameObject[] worldTiles;// = GameObject.Find();
    [SerializeField] public GameObject tile;
    [SerializeField] public Sprite blankTileSprite;
    
    ///TERRAIN STUFF///
    public int lastTerrainNumberInput;
    [SerializeField] public Sprite TundraSprite;
    [SerializeField] public Sprite GrasslandSprite;
    [SerializeField] public Sprite PlainsSprite;
    [SerializeField] public Sprite HillsSprite;
    [SerializeField] public Sprite MountainsSprite;
    [SerializeField] public Sprite ValleyRiverSprite;
    [SerializeField] public Sprite SavannahSprite;
    [SerializeField] public Sprite DesertSprite;
        
    //Tile potentialTile = new Tile();
    Tile newTile = new Tile();
    Tile previousTile = new Tile();
        
    //use an 2darraylist or an array[][] 
        
    //make a new datatype that holds a tile coordinate or something
    
    public GameObject getObjectAtLocation(Vector2 inputLocation)
    {
        
        GameObject gameObjectAtLocation = null;
        
        //goes through game objects until it finds one matching the location of the inputLocation
        //then, it sets the found object to gameObjectAtLocation
        for(int i = 0; i < worldTiles.Length; i++)
        {
            
            //make a list of all occupied spots instead of Armybuildingobjectlist
            string objectTag = worldTiles[i].name;
            Vector2 foundObjectVector2 = GameObject.Find(objectTag).transform.position;
            
            if(inputLocation == foundObjectVector2)
            {
                
                gameObjectAtLocation = worldTiles[i];

            }

            else
            {

                gameObjectAtLocation = null;
                Debug.Log("No object was found when searching for object at location! Mouse Location: " + inputLocation);

            }

        }
        
        //will return null if something goes wrong/cant find an object
        //this might cause a bug
        return gameObjectAtLocation;

    }
    
    public GameObject[] GenerateTiles()
    {

        GameObject[] tiles = new GameObject[maxWorldSize];
        
        for (int i = 0; i < tiles.Length; i++)
        {

            tiles[i] = GameObject.Instantiate(tile);
            //tiles[i].GetComponent<Tile>().tileColumn = i;
            

        }

        //store tiles array in worldTiles array
        for (int i = 0; i < tiles.Length; i++)
        {

            worldTiles[i] = tiles[i];

        }

    }

    public GameObject GenerateTile()
    {

        return GameObject.Instantiate(tile);

    }

    public GameObject[,] GenerateMapTiles(int maxWorldX, int maxWorldY)
    {
        
        ArrayList worldArrayList = new ArrayList(); 
        //Tile tileInformation = new Tile();
        GameObject[,] worldCoordinates = new GameObject[maxWorldX, maxWorldY];//[maxWorldY];
        Vector2 worldTilePosition;
        GameObject currentTile;
        
        //for every x, do the for loop inside the x loop
        for (int x = 0; x < maxWorldX; x++)
        {

            //fills every column with a tile GameObject
            for(int y = 0; y < maxWorldY; y++)
            {

               
                worldCoordinates[x,y] = GenerateTile();
            }

        }
        
        //place tiles into a grid
        for (int x = 0; x < maxWorldX; x++)
        {

            for (int y = 0; y < maxWorldY; y++)
            {
                //you need to specifically choose the kind fo random you want, i chose the one in Unity.Mathematics
                Random randomNumber = new Random();
                
                currentTile = worldCoordinates[x, y];
                worldTilePosition = currentTile.transform.position;
                
                
                worldTilePosition.x = x * 1.5f;
                worldTilePosition.y = y * 1.5f;
                
                Tile newTileComponent = ChooseTerrain(randomNumber.NextInt(0 , 143));
                
                currentTile.GetComponent<Tile>().TileNumberID = maxWorldX + "000" + maxWorldY;

                //RANDOM NUMBER THAT CREATES RANDOM TERRAIN

            }

        }
            
    }

    public Tile ChooseTerrain(int randomInput)
    {

        Tile currentTile = new Tile();
        currentTile.TileNumberID = -1;
        currentTile.TileTerrainID = "test";
        currentTile.tileColumn = -1;
        currentTile.tileRow = -1;

        if (randomInput >= 0 && randomInput <= 15)
        {

            currentTile.TileTerrainID = "Tundra";
            currentTile.tileSprite = TundraSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        if (randomInput >= 16 && randomInput <= 31)
        {

            currentTile.TileTerrainID = "Grasslands";
            currentTile.tileSprite = GrasslandSprite;

            if (lastTerrainNumberInput <= 16 && lastTerrainNumberInput <= 31 & randomInput <= 7 && randomInput <= 22)
            {
                
                
                
            }
            
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        if (randomInput >= 32 && randomInput <= 47)
        {

            currentTile.TileTerrainID = "Plains";
            currentTile.tileSprite = PlainsSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        if (randomInput >= 48 && randomInput <= 63)
        {

            currentTile.TileTerrainID = "Hills";
            currentTile.tileSprite = HillsSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        //make it detect if the mountain region is at least 3x3 tiles, then put a mountain peak in the middle?
        if (randomInput >= 64 && randomInput <= 79)
        {

            currentTile.TileTerrainID = "Mountains";
            currentTile.tileSprite = MountainsSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        if (randomInput >= 80 && randomInput <= 95)
        {

            currentTile.TileTerrainID = "Hills";
            currentTile.tileSprite = HillsSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }

        if (randomInput >= 96 && randomInput <= 111)
        {

            currentTile.TileTerrainID = "Valley/River";
            currentTile.tileSprite = ValleyRiverSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        if (randomInput >= 112 && randomInput <= 127)
        {

            currentTile.TileTerrainID = "Savannah";
            currentTile.tileSprite = SavannahSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        if (randomInput >= 128 && randomInput <= 143)
        {

            currentTile.TileTerrainID = "Desert";
            currentTile.tileSprite = DesertSprite;
            lastTerrainNumberInput = randomInput;
            
            return currentTile;

        }
        
        return currentTile;

    }

    //hwo do i mkae a function that just does something and doesnt return anythign?
    public bool CheckNewTile()
    {

        bool claimedTile = false;

        if()
        {
            
            
            
        }

        if (newTile.TileState == Tile.TileState.CLAIMED)
        {

            claimedTile = true;

            return claimedTile;
            //FindNextTile();

        }

        return claimedTile;

    }

        //the output from this will be the nextTile argument in GenerateRegion()
    public GameObject FindNextTile()
    {
            
        for () {
                
                
                
        }

        CheckNewTile();
            
    }
    
    //returns a new map with the newly generated region and replaces the old map
    public GameObject[][] GenerateRegion(GameObject firstTile, GameObject nextTile)
    {
        
        nextTile = FindNextTile();
        
        firstTile = nextTile;

    }

}