using UnityEngine;

public class Region
{
    //the game will generate regions that are basically giant squares equal to 20 x 20 squares
    //I could eventually figure out how to make it only spawn 10 buildings per certain amount of white noise generation
    //store all squares in arrays
    //make it infinite for an infinte mode
    //for now, maps will be equal to 16 regions in the shape of a cube or whatever shaper desired
    //generated using Perlin white noise

    [SerializeField] public Vector2 topLeftVector2;
    [SerializeField] public Vector2 topRightVector2;
    [SerializeField] public Vector2 bottomLeftVector2;
    [SerializeField] public Vector2 bottomRightVector2;
    
    GameObject[] civilianUnitList = GameObject.FindGameObjectsWithTag("Civilian");

    public int regionalCivilianPopulation;
    public int regionalPolicePopulation;

    public float regionalPolicePercentage = Random.Range(1.6f, 4.0f);

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject regionObject = GameObject.CreatePrimitive(PrimitiveType.Plane);

    [SerializeField] public Vector2 regionCentralPointOnWorldMap;

    public Region(Vector2 topLeftVector2Input, Vector2 topRightVector2Input, 
        Vector2 bottomLeftVector2Input, Vector2 bottomRightVector2Input)
    {
        
        //Actual as in what is in the world map
        Transform regionLocationActualTransform = regionObject.transform;
        Vector2 regionLocationActual = regionObject.transform.position;
        
        topLeftVector2 = topLeftVector2Input;
        topRightVector2 = topRightVector2Input;
        bottomLeftVector2 = bottomLeftVector2Input;
        bottomRightVector2 = bottomRightVector2Input;
        regionLocationActualTransform.Translate(regionLocationActual - regionCentralPointOnWorldMap);

    }

    /*
    //need to make this work for double digit decimals
    public int RoundFloat(float inputFloat)
    {

        int returnInteger = 0;
        string inputFloatAsString = inputFloat.ToString();
        int inputFloatStringLength = inputFloatAsString.Length;
        char[] inputFloatCharArray = inputFloatAsString.ToCharArray();
        char inputFloatTenths = inputFloatCharArray[2];
        char inputFloatOnes = inputFloatCharArray[0];
        
        for(int i = 0; i < inputFloatStringLength; i++) {
            
            if(inputFloatCharArray[2] >= 5)
            {
                
                inputFloatCharArray[2] = '0';
                inputFloatCharArray[0] = inputFloatCharArray[0] + 1;
                Debug.Log("The float will be rounded up!" + inputFloatTenths);

            }

            else
            {
                
                Debug.Log("The float will be rounded down!" + inputFloatTenths);
                
            }

        }

        return returnInteger;

    }*/

    void Start()
    {

        //0.022 is the average police-civilian ratio for the US
        //make it range from 1.6-4.0, with 7.5 for the hardest difficulty (police per thousand in
        //the District of Columbia)

        PlayerController playerController = player.GetComponent<PlayerController>();
        
        //game difficulty starts at 0 for easiest
        int gameDifficulty = playerController.difficultyLevel;
        
        float policeDifficultyScaling = 0.022f * gameDifficulty;
        
        //this is be
        regionalCivilianPopulation = civilianUnitList.Length;
        //
        regionalPolicePopulation = 
            Mathf.RoundToInt(regionalCivilianPopulation * policeDifficultyScaling);

        //for Buildings in this Area, setRegion to be equal to this Region and set this region's percentages to be the
        //Building's percentages (police, civilian, army)
        //find units within the bounds of the region and set the region boundaries as their boundaries and setRegion
        //if you set the units to be children of the region's GameObject, then they would be bound to its 'grid'

    }

    void Update()
    {
        
        
        
    }
}


