using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Building : MonoBehaviour
{
    
    //NEED CONSTRUCTOR

    [SerializeField] public float regionalCivilianPercentage;
    
    //random for now?
    [SerializeField] public int buildingPopulation = Random.Range(90, 171);
    [SerializeField] public int buildingCivilianPopulation;
    //policePopulation is equal to the percentage of policePopulation in the region
    //region is equal to 10 buildings
    //for every police station around, multiply police population 
    [SerializeField] public int buildingPolicePopulation;
    [SerializeField] public int buildingMilitaryPopulation;

    [SerializeField] public Region thisBuildingRegion;
    [SerializeField] public GameObject building;

    [SerializeField] public GameObject player;

    public enum BuildingState
    {
        
        OCCUPIED,
        
        CONQUERED,
        
        ARMY_OCCUPIED,
        
        INFESTED,
        
        INFESTING,
        
        
    }

    public BuildingState buildingState;

    private void Start()
    {
        
        Region buildingRegion = building.GetComponent<Region>();
        float regionalPolicePercentage = buildingRegion.regionalPolicePercentage;
        PlayerController playerController = player.GetComponent<PlayerController>();
        //loads in the selected player difficulty level into the class
        int difficultyLevel = playerController.difficultyLevel;
        
        //debug.Log if it reaches under 0
        regionalCivilianPercentage = 100 - (regionalPolicePercentage * difficultyLevel);

        buildingCivilianPopulation = Mathf.RoundToInt(buildingPopulation * regionalCivilianPercentage);

        buildingPolicePopulation = Mathf.RoundToInt(buildingPopulation * regionalPolicePercentage);

    }

    void Update()
    {

        //probably should have an "empty" building state because that might happen somehow
        //find a way to insert zombie army leftover zombie population and then make it && zombiePopulationPercentage > 0
        if(buildingPopulation == 0)
        {

            buildingState = BuildingState.CONQUERED;

        }

        else
        {

            buildingState = BuildingState.OCCUPIED;

        }

        if (regionalCivilianPercentage < 0)
        {
            
            Debug.Log("The regionalCivilianPercentage was under 0: " + regionalCivilianPercentage);

        }

    }

}