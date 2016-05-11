using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour
{
	//Singleton
	static public Game current;
	public GameObject currentlyBuilding;
	public GameObject[]
		buildingPrefabs;
	public Sprite[] resourceSprites;

	//Checks for Tutorial Info
	public bool embassyTut;

	static Hashtable buildingHashtable;

	//ShipManagement
	int _maxShips = 0;
	int _currentShips = 0;
	public int currentShips {
		get {
			return _currentShips;
		}
		set {
			_currentShips = value;
			ShipCheck ();
			shipText.text = currentShips.ToString () + " / " + _maxShips.ToString ();
		}
	}
	//List of current Buildings
	public List<Building> buildingList;

	public ConcurrentDictionary<string, GameObject> uiElements;

	void Awake ()
	{
		if (current == null) {
			current = this;
		} else {
			Destroy (this);
		}
		embassyTut = false;
		InitializeBuildingHashtable ();
        commoditiesText = GameObject.Find("Commodities").GetComponentInChildren<Text>();
        luxuriesText = GameObject.Find("Luxuries").GetComponentInChildren<Text>();
        wealthText = GameObject.Find("Wealth").GetComponentInChildren<Text>();
        shipText = GameObject.Find("MaxShips").GetComponentInChildren<Text>();

		buildingList = new List<Building> ();
		
	}

	void InitializeBuildingHashtable ()
	{
		buildingHashtable = new Hashtable ();
		foreach (GameObject _object in buildingPrefabs) {
			buildingHashtable [_object.name] = _object;
		}
	}
   

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
    void Update()
    {
        //If presses Escape while holding bUilding, cancel it.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuild();
        }
    }

    void FixedUpdate()
    {
        UpdateResourceText();
    }
	void CancelBuild ()
	{
		//if holding building, destroy it and set the variable to null
		if (currentlyBuilding != null) {
			Destroy (currentlyBuilding);
			SetCurrentlyBuilding (null);
		}
	}
	//For every building in the resourceBuildingList advance their production by 1
	void IncrementProductionTicks ()
	{
		foreach (Building building in buildingList) {
			building.ProductionTick ();
		}
	}
	
	void DecrementSailingShips ()
	{
		foreach (TradeMission missionScript in Factions.current.GetTradeMissionList()) {
			missionScript.SailTick (1);
		}
	}

	bool shipCheck = true;
	//Check all booleans here. Returning true means go ahead with NextTurn
	bool NextTurnCheck ()
	{
		foreach (Building building in buildingList) {
			if (building.GetType () == Type.GetType ("ResourceBuilding") && ((ResourceBuilding)building).resourcesMaxed) {
				Debug.Log ("ResourceCheck Failed");
				EventSystem.OccurEvent ("CapacityErrorPanel");
				return false;
			}
		}
		//Add building costs
		if (!shipCheck) {
			Debug.Log ("ShipCheckFailed");
			EventSystem.OccurEvent ("ShipErrorPanel");
			return false;
		}
		return true;
	}
	//Perform all between methods here
	public void NextTurn ()
	{
		if (!NextTurnCheck ())
			return;
		IncrementProductionTicks ();
		DecrementSailingShips ();

		ShipCheck ();


	}

	public void ShipCheck ()
	{
		shipCheck = !ShipAvailable ();
	}

	public void NextTurnForce ()
	{
		IncrementProductionTicks ();
		DecrementSailingShips ();
	}

	public void SetShipCheck (bool b)
	{
		shipCheck = b;
	}
	

	//BUILDER METHODS
	//Set method for currentlyBuilding
	public void SetCurrentlyBuilding (GameObject _currentlyBuilding)
	{

		currentlyBuilding = _currentlyBuilding;
	}

	public void StartBuilding (string _building)
	{
		CancelBuild ();
        ResourceBundle buildCost = ((GameObject)buildingHashtable[_building]).GetComponent<Building>().GetBuildCost();
        if (!resourcesMain.CompareBundle(buildCost)) return;// check to make sure build costs satisfy
        //resourcesMain -= ((Building)buildingHashtable[_building]).GetBuildCost();
		Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentMousePosition.z = 0;
		GameObject instance = (GameObject)Instantiate (buildingHashtable [_building] as UnityEngine.Object, currentMousePosition, Quaternion.identity);
		SetCurrentlyBuilding (instance);
	}

	//OTHER METHODS

	public void toggleInteractableButton (Button b)
	{
		b.interactable = !b.interactable;
	}

	public void DestroyGameObject (UnityEngine.Object o)
	{
		Destroy (o);
	}

	public void DestroyGameObject (UnityEngine.Object o, float time)
	{
		Destroy (o, time);
	}

    Text commoditiesText, luxuriesText, wealthText, shipText;
    public static ResourceBundle resourcesMain = new ResourceBundle(1000, 1000, 1000);
	public void addToResource (int _type, int _amount)
	{
		if (_type == 0) {
			resourcesMain.StringAdd("Commodity", _amount);
		} else if (_type == 1) {
            resourcesMain.StringAdd("Luxury", _amount);
		} else
            resourcesMain.StringAdd("Wealth", _amount);
        UpdateResourceText();
	}

    public void UpdateResourceText()
    {
        commoditiesText.text = ":" + resourcesMain.commodity.ToString();
        luxuriesText.text = ":" + resourcesMain.luxury.ToString();
        wealthText.text = ":" + resourcesMain.wealth.ToString();
    }

    public static bool EnoughResourceCheck(int _type, int _amount)
    {
        if (_type == 0 &&
            resourcesMain.commodity >= _amount)
            return true;
        else if (_type == 1 &&
            resourcesMain.luxury >= _amount)
            return true;
        else if (_type == 2 &&
            resourcesMain.wealth >= _amount)
            return true;
        return false;
    }

	public void PassAllResourceChecks ()
	{
		foreach (ResourceBuilding building in buildingList)
			building.resourcesMaxed = false;
		NextTurn ();
	}

	//SHIP MANAGEMENT METHODS

	public int maxShips {
		get {
			return _maxShips;
		}
		set {
			_maxShips = value;
			ShipCheck ();
			shipText.text = currentShips.ToString () + " / " + _maxShips.ToString ();
		}
	}

	public bool ShipAvailable ()
	{
		if (currentShips >= maxShips) {
			return false;
		}
		return true;
	}

	public void ShipyardWindowPopulate (TradeMission currentTradeMission)
	{

		foreach (Building _shipyard in buildingList) {
			if (_shipyard.GetType ().Equals (Type.GetType ("Shipyard")))
				((Shipyard)_shipyard).CreateShipUI (currentTradeMission);
		}
	}

	public void DestroyShipUIInstances ()
	{
		GameObject shipyardUI;
		shipyardUI = GameObject.Find("ShipyardWindow");
		foreach (Transform child in shipyardUI.transform) {
			if (child.name.Equals ("Ship(Clone)"))
				Destroy (child.gameObject);
		}
	}
	
}
