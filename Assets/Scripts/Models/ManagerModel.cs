using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ManagerModel : GameElement
{

	public GameObject currentlyBuilding;
	public GameObject[] buildingPrefabs;

	//Checks for Tutorial Info
	public bool embassyTut = false;
	
	public static Hashtable buildingHashtable;
	
	//List of current Buildings
	public List<Building> buildingList;
	public static ResourceBundle resourcesMain = new ResourceBundle(1000, 1000, 1000);
    public int currentShips;
    public int maxShips;
    public Hashtable buildingCostsReferences;
	void Awake()
	{
        buildingCostsReferences = new Hashtable();
        buildingCostsReferences.Add("embassy", BuildingCosts.embassy);
        buildingCostsReferences.Add("shipyard", BuildingCosts.shipyard);
        buildingCostsReferences.Add("huntersLodge", BuildingCosts.huntersLodge);
        buildingCostsReferences.Add("farm", BuildingCosts.farm);
		embassyTut = false;
		buildingList = new List<Building> ();
		buildingHashtable = new Hashtable ();
		foreach (GameObject _object in buildingPrefabs) {
			buildingHashtable [_object.name] = _object;
		}
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

}

