using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ManagerModel : GameElement
{

	public GameObject currentlyBuilding;
	public GameObject[] buildingPrefabs;
    public int winningWorth;
	//Checks for Tutorial Info
    public bool embassyTut, shipyardTut, productionTut;
	
	public static Dictionary<string, GameObject> buildingHashtable;
	
	//List of current Buildings
	public static ResourceBundle resourcesMain = new ResourceBundle(3500, 350, 150);
    public int currentShips;
    public int maxShips;
    public Dictionary<string, ResourceBundle> buildingCostsReferences;

    public static int currentTurn = 0;

	void Awake()
	{
        
        buildingCostsReferences = new Dictionary<string, ResourceBundle>();
        buildingCostsReferences.Add("embassy", BuildingCosts.embassy);
        buildingCostsReferences.Add("shipyard", BuildingCosts.shipyard);
        buildingCostsReferences.Add("huntersLodge", BuildingCosts.huntersLodge);
        buildingCostsReferences.Add("farm", BuildingCosts.farm);
        buildingCostsReferences.Add("goldsmith", BuildingCosts.goldsmith);
		buildingHashtable = new Dictionary<string, GameObject> ();
		foreach (GameObject _object in buildingPrefabs) {
			buildingHashtable [_object.name] = _object;
		}
        
	}
    void Start()
    {
        winningWorth = app.controller.story.maxDebt * 3;
        Debug.Log(winningWorth.ToString());
    }
	
	//BUILDER METHODS
	//Set method for currentlyBuilding
	public void SetCurrentlyBuilding (GameObject _currentlyBuilding)
	{
		
		currentlyBuilding = _currentlyBuilding;
	}
	public void CancelBuild ()
	{
		//if holding building, destroy it and set the variable to null
		if (currentlyBuilding != null) {
			Destroy (currentlyBuilding);
			currentlyBuilding = null;
		}
	}

	public void addToResource (int _type, int _amount)
	{
		if (_type == 0) {
			resourcesMain.StringAdd("Commodity", _amount);
		} else if (_type == 1) {
			resourcesMain.StringAdd("Luxury", _amount);
		} else
			resourcesMain.StringAdd("Wealth", _amount);
		//UpdateResourceText();
	}

    public void addBundle(ResourceBundle _change)
    {
        resourcesMain += _change;
    }

}

