using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{   

    [SerializeField] public GameObject player;
    
    public SceneSearchContext search = new SceneSearchContext();

    public Vector2[][] worldPositions = { }; //new Vector2[200][100]};

    [SerializeField] public int numberOfRegions;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// UI STUFF
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    [SerializeField] public Button zombieButton;
    
    //need to find a way to automatically do this rather than insert every button manually
    public Button[] buttonList = { };

    public Toggle[] toggleList = { };

    public DropdownMenu[] dropdownMenuList = { };

    public Slider[] sliderList = { };

    //difficulty level will default to 0, can go up to 10
    public int difficultyLevel = 0;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public GameObject[] nonInteractableObjectList = 
        GameObject.FindGameObjectsWithTag("NonInteractable");
    public Vector2[] nonInteractableLocationList;

    public GameObject[] armyBuildingObjectList = 
        GameObject.FindGameObjectsWithTag("ArmyBuilding");
    public Vector2[] armyBuildingLocationList;

    public GameObject[] infestedBuildingObjectList = 
        GameObject.FindGameObjectsWithTag("InfestedBuilding");
    public Vector2[] infestedBuildingLocationList;

    public GameObject[] civilianBuildingObjectList = 
        GameObject.FindGameObjectsWithTag("CivilianBuilding");
    public Vector2[] civilianBuildingLocationList;

    public GameObject[] allBuildingsList;
    
    //will interrupt all building attempts
    public GameObject[] nonDestructableObjectsList;
    
    [SerializeField] public GameObject emptyGameObject;

    [SerializeField] public GameObject InfestedBuilding;
    [SerializeField] public GameObject CivilianBuilding;
    [SerializeField] public GameObject ZombieBuilding;
    [SerializeField] public GameObject ArmyBuilding;
    
    public Vector2 mouseLocation = Input.mousePosition;

    /////////////////////////////////////////////////////////////////////////////////
    /// ENUM
    ////////////////////////////////////////////////////////////////////////////////
    
    public PressState pressState = PressState.NONE;

    public LocationState locationState = LocationState.AWAKE;
    public enum PressState
    {
        
        NONE,
        
        ZOMBIE,
        
        INFESTATION,
        
        MENU

    }
    
    public enum LocationState
    {
        
        AWAKE,
        
        EMPTY,
        
        OCCUPIED,
        
        CIVILIAN_OCCUPIED,
        
        ARMY_OCCUPIED,
        
        INFESTED_OCCUPIED7

    }
    
    public enum DifficultyLevel
    {
        
        EASY,
        
        MEDIUM,
        
        HARD

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void createObjectLists()
    {
        
        //<= or <???
        //creates a list of locations occupied by objects tagged "NonInteractable"
        for (int i = 0; i <= nonInteractableObjectList.Length; i++)
        {

            nonInteractableLocationList[i] = nonInteractableObjectList[i].transform.position;

        }
        
        //make army building list
        for (int i = 0; i <= armyBuildingObjectList.Length; i++)
        {

            armyBuildingLocationList[i] = armyBuildingObjectList[i].transform.position;

        }
        
        //make infested building list
        for (int i = 0; i <= infestedBuildingObjectList.Length; i++)
        {

            infestedBuildingLocationList[i] = infestedBuildingObjectList[i].transform.position;

        }

        //make
        for (int i = 0; i <= civilianBuildingObjectList.Length; i++)
        {

            civilianBuildingLocationList[i] = civilianBuildingObjectList[i].transform.position;

        }
        
    }

    /*public bool listMatch(GameObject[] firstListInput, GameObject[] secondListInput)
    {
        bool isTrue = false;
        int firstListLength = firstListInput.Length;
        int secondListLength = secondListInput.Length;
        GameObject[] largestList;
        GameObject[] smallestList;


        if(firstListLength > secondListLength || firstListLength == secondListLength)
        {

            largestList = firstListInput;
            smallestList = secondListInput;

        }
        else
        {

            largestList = secondListInput;
            smallestList = firstListInput;

        }
        
        int largestListLength = largestList.Length;
        
        //compare first item
        for (int i = 0; i < largestListLength; i++) {
            
            //this doesnt work because you need to compare more of the 
            if(firstListInput[0] == secondListInput[i] || secondListInput[0])
            {

                isTrue = true;

            }
            
            else
            {
                
                isTrue = false;
                
            }
            
        }

        return isTrue;

    }*/


    public GameObject getObjectAtLocation(Vector2 inputLocation)
    {
        
        GameObject gameObjectAtLocation = null;
        
        //goes through game objects until it finds one matching the location of the inputLocation
        //then, it sets the found object to gameObjectAtLocation
        for(int i = 0; i < nonDestructableObjectsList.Length; i++)
        {
            
            //make a list of all occupied spots instead of Armybuildingobjectlist
            string objectTag = nonDestructableObjectsList[i].name;
            Vector2 foundObjectVector2 = GameObject.Find(objectTag).transform.position;
            
            if(inputLocation == foundObjectVector2)
            {
                
                gameObjectAtLocation = nonDestructableObjectsList[i];

            }

            else
            {

                gameObjectAtLocation = null;
                Debug.Log("No object was found when searching for object at location! Mouse Location: " + inputLocation);

            }

        }
        
        //will return null if something goes wrong/cant find an object
        return gameObjectAtLocation;

    }

    public bool buildingIsConquered(GameObject buildingInput)
    {

        bool isConquered = false;
        Building building = buildingInput.GetComponent<Building>();

        if(building.buildingState == Building.BuildingState.CONQUERED)
        {

            isConquered = true;

        }

        return isConquered;

    }

    public bool locationIsInList(Vector2 locationInput, Vector2[] inputList)
    {

        int inputListLength = inputList.Length;
        bool inList = false;
        
        for(int i = 0; i < inputListLength; i++)
        {

            if(locationInput == inputList[i])
            {

                inList = true;

            }

        }

        return inList;

    }

    //creates a list of locations occuiped by objects tagged with "NonInteractable"
    public void createNonInteractableLocationList()
    {
        
        //<= or <?
        for (int i = 0; i <= nonInteractableObjectList.Length; i++)
        {

            nonInteractableLocationList[i] = nonInteractableObjectList[i].transform.position;

        }

    }
    public void setPressState(Button buttonInput)
    {
        
        //find a way to use tag because that is better to use I think
        switch (buttonInput.name)
        {
            
            case "ZombieTile":
                pressState = PressState.ZOMBIE;
                break;
            case "InfestationTile":
                pressState = PressState.INFESTATION;
                break;
            
        }
        
    }
    
    //decides if the location is suitable for placing an object
    public void setLocationState(Vector2 mouseLocationInput)
    {

        bool match = false;
        
        //createObjectLists
        createNonInteractableLocationList();

        for(int i = 0; i <= nonInteractableObjectList.Length + civilianBuildingLocationList.Length + 
            armyBuildingLocationList.Length + infestedBuildingLocationList.Length; i++){
            
            //if mousePosition is the same as a position that is already occupied and the mouseState is zombie, then return OCCUPIED
            //if(mouseLocationInput == nonInteractableLocationList[i])
            if(locationIsInList(mouseLocationInput, nonInteractableLocationList))
            {

                locationState = LocationState.OCCUPIED;
                Debug.Log("Location is OCCUPIED by a NON-INTERACTABLE object. Mouse location: " + mouseLocationInput);

            }

            else if(locationIsInList(mouseLocationInput, civilianBuildingLocationList))
            {

                locationState = LocationState.CIVILIAN_OCCUPIED;
                Debug.Log("Location is OCCUPIED by a CIVILIAN building. Mouse location: " + mouseLocationInput);
                
            }

            else if(locationIsInList(mouseLocationInput, armyBuildingLocationList))
            {

                locationState = LocationState.ARMY_OCCUPIED;
                Debug.Log("Location is OCCUPIED by an ARMY building. Mouse location: " + mouseLocationInput);
                
            }
            
            else if(locationIsInList(mouseLocationInput, infestedBuildingLocationList))
            {

                locationState = LocationState.INFESTED_OCCUPIED;
                Debug.Log("Location is OCCUPIED by an INFESTED building. Mouse location: " + mouseLocationInput);
                
            }
            //if mousePosition is the same as something that is empty, then return EMPTY
        }
        
    }

    public void PlaceSquare()
    {
        
        //convert mouse location to snap to nearest square
        setLocationState(mouseLocation);
        GameObject objectAtLocation = getObjectAtLocation(mouseLocation);
        
        if (locationState == LocationState.EMPTY)
        {

            Instantiate(ZombieBuilding, mouseLocation, ZombieBuilding.transform.rotation);
            //build zombie

        }

        //FIND A WAY TO ACCESS BUILDING COMPONENT FROM THE CURRENT BUIDLING
        if (locationState == LocationState.CIVILIAN_OCCUPIED && 
            pressState == PressState.INFESTATION && buildingIsConquered(objectAtLocation))
        {
            
            //change to/build infeseted building
            
        }

        if (locationState == LocationState.OCCUPIED)
        {
            
            //Console.WriteLine("Location is occupied by: " );
            
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// VOID METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void InitializeWorldPositions()
    {
        //fill this array with the data manipulated in this method
        Vector2[][] worldPositionArray;
        //you could make the map based on the player's spawn
        float playerXPosition = player.transform.position.x;
        float playerYPosition = player.transform.position.y;
        
        //wont this be off if the player isnt in the exact middle square?
        //might change this all so its consistent
        
        Vector2[] oneArray = new Vector2[10000];
        Vector2[] oneOneArray = new Vector2[10000];
        
        Vector2[] twoArray = new Vector2[5000];
        Vector2[] threeArray = new Vector2[5000];
        Vector2[] fourArray = new Vector2[5000];
        Vector2[] fiveArray = new Vector2[5000];
        
        //two loops, one for one half of the grid
        for (int i = 0; i <= 20000; i++) {
            
                for (int k = 0; k < 50; k++) {

                    for (int j = 0; j < 50; j++) {
                        //mkae an array all the way up and down one way and then make arrays based on each array point
                        //worldPositions[i] = player.
                        //worldPositions[i] = ;new Vector2(playerXPosition - i, playerYPosition - i);
                        //worldPositions[j] = new Vector2(playerXPosition + j)

                        oneArray[i] = new Vector2(playerXPosition - i, playerYPosition);
                        
                        //how many points will there be? that has to be what the 'i' is in twoArray[i]
                        //this will store all the positions as it counts up y to the top of the map (assume x is longways for rectangle)
                        twoArray[i] = new Vector2(oneArray[i].x, oneArray[i].y - j);

                    }
                    
                }
        }
        
    }

    //this will make random regions and try to fit them to the currentWorldMap
    public void fitRegionToWorldMap()
    {
        
        //why does this go to 3000? there are 20,000 units in the world, so 3000, would fit 6 regions
        //this should instead calculate the amount of units based on the number of regions selected!
        
        //20,000 / numberOfRegions = i limit
        
        long numberOfRegionVector2s = 200000 / numberOfRegions;
        
        for(int i = 0; i < numberOfRegionVector2s; i++) {

            for(int j = 0; j < numberOfRegionVector2s; j++) {
                
                
                
            }

        }

    }

    //will eventually make this flexible, but for now will just spawn squares
    public void InitializeRegions(int numberOfRegions)
    {
        
        ///////////////
        //CALCULATIONS
        /*
        int regionArea = 400;
        float worldMapArea = regionArea * numberOfRegions;*/
        float worldMapArea = 200000;
        float regionArea;
        int worldMapYLength = Mathf.RoundToInt(Mathf.Sqrt(worldMapArea));
        int worldMapXLength = worldMapYLength;
        ///////////////
        
        ///////////////
        //WORLD VECTORS
        Vector2 worldMapTopLeftCorner = new Vector2(-worldMapXLength, worldMapYLength);
        Vector2 worldMapTopRightCorner = new Vector2(worldMapXLength, worldMapYLength);
        Vector2 worldMapBottomLeftCorner = new Vector2(-worldMapXLength, -worldMapYLength);
        Vector2 worldMapBottomRightCorner = new Vector2(worldMapXLength, -worldMapYLength);
        int halfNumberOfRegions = numberOfRegions / 2;
        int halfNumberOfRegionsSquared = halfNumberOfRegions * halfNumberOfRegions;
        ///////////////
        
        if (numberOfRegions % 2 != 0) //&& halfNumberOfRegions % 2 != 0)
        {
            //This if statement filters out numbers that 
            
            Debug.Log("Invalid number of regions. " +
                      "Use a number that is perfectly divisible by 2 (% 2 = 0) " +
                      "numberOfRegionsInput: " + numberOfRegions);

        }
        else
        {
            
            
            
        }

    }

    //this might be a better way to do pressSquare
    void OnMouseDown(Collider collider)
    {
        //find the location fo the mouse on click and set mouseLocation equal to that location on click
        //mouseLocation = 
        //is the quaternion correct?
        //Instantiate(emptyGameObject, mouseLocation, emptyGameObject.transform.rotation);
        
        
    }

    void OnMouseUp()
    {
        
        Destroy(emptyGameObject);
        
    }

    private void OnMouseDrag()
    {
        
        //if pressState zombie is true, change ZombieBuilding rotation quaternion based on the mousePosition vector2
            
        
    }

    // Start is called before the first frame update
    void Start()
    {

        Console.WriteLine("Choose Difficulty");

        //initialize (and buttonUp()) list of buttons since it would be faster (TO PROCESS AND TO EDIT)
        //than calling every button inidivdually
        //But, for now I will initialize zombieButton
        //ChangeButtonSprite changeZombieButtonSprite = zombieButton.GetComponent<ChangeButtonSprite>();
        //changeZombieButtonSprite.buttonUp();

    }

    // Update is called once per frame
    void Update()
    {
        
        //OnUIPress(UIObject)
            //setPressState(UIObject)
                //make it set the state from the script thats on the button instead
                
        
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
}
