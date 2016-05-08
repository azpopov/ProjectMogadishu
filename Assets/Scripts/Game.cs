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
	public static GameObject uiMain;

	//Checks for Tutorial Info
	public bool embassyTut;
	
	public enum ResourceType
	{
		Commodity,
		Luxury,
		Wealth
	}

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
	Text commoditiesText, luxuriesText, wealthText, shipText;
	public ConcurrentDictionary<string, GameObject> uiElements;

	void Awake ()
	{
		if (current == null) {
			current = this;
		} else {
			Destroy (this);
		}
		embassyTut = false;
		uiMain = GameObject.Find ("UI");
		InitializeDictionary ();
		InitializeBuildingHashtable ();

		buildingList = new List<Building> ();
		
	}

	void InitializeDictionary ()
	{

		foreach (Transform child in uiMain.transform) {
			child.gameObject.SetActive (true);
		}
		uiElements = new ConcurrentDictionary<string, GameObject> ();
		uiElements.Add ("BottomToolbar", GameObject.Find ("BottomToolbar"));
		uiElements.Add ("TopToolbar", GameObject.Find ("TopToolbar"));
		uiElements.Add ("ShipView", GameObject.Find ("ShipView"));
		uiElements.Add ("ShipyardWindow", GameObject.Find ("ShipyardWindow"));
		commoditiesText = GameObject.Find ("Commodities").GetComponentInChildren<Text> ();
		luxuriesText = GameObject.Find ("Luxuries").GetComponentInChildren<Text> ();
		wealthText = GameObject.Find ("Wealth").GetComponentInChildren<Text> ();
		shipText = GameObject.Find ("MaxShips").GetComponentInChildren<Text> ();
		foreach (GameObject _target in uiElements.GetValuesArray()) {
			_target.SetActive (false);
		}
		GameObject _object;
		_object = uiElements ["TopToolbar"];
		_object.gameObject.SetActive (true);
		_object = uiElements ["BottomToolbar"];
		_object.gameObject.SetActive (true);
        
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
		Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentMousePosition.z = 0;
		GameObject instance = (GameObject)Instantiate (buildingHashtable [_building] as UnityEngine.Object, currentMousePosition, Quaternion.identity);
		SetCurrentlyBuilding (instance);
	}


	//OTHER METHODS

	public void enableUI (string _uiName)
	{
		GameObject _uiGameObject;
		try {
			_uiGameObject = uiElements [_uiName.Trim ()];
		} catch (NullReferenceException e) {
			_uiGameObject = GameObject.Find (_uiName);
		}
		_uiGameObject.SetActive (!_uiGameObject.activeSelf);
	}

	public void enableUI (string _uiName, bool desireBool)
	{
		GameObject _uiGameObject;
		try {
			_uiGameObject = uiElements [_uiName.Trim ()];
		} catch (NullReferenceException e) {
			_uiGameObject = GameObject.Find (_uiName);
			Debug.Log (e.Message);
		}
		_uiGameObject.SetActive (desireBool);
	}

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

	//RESOURCE MANAGEMENT METHODS

	int _commodities = 500;
	int _luxuries = 500;
	int _wealth = 500;

	public int commodities {
		get { return _commodities;}
		set {
			_commodities = value;
			commoditiesText.text = ":" + _commodities.ToString ();
		}
	}

	public int luxuries {
		get { return _luxuries;}
		set {
			_luxuries = value;
			luxuriesText.text = ":" + _luxuries.ToString ();
		}
	}

	public int wealth {
		get { return _wealth;}
		set {
			_wealth = value;
			wealthText.text = ":" + _wealth.ToString ();
		}
	}

	public void addToResource (int _type, int _amount)
	{
		if (_type == 0) {
			commodities += _amount;
		} else if (_type == 1) {
			luxuries += _amount;
		} else
			wealth += _amount;
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
		shipyardUI = Game.current.uiElements ["ShipyardWindow"];
		foreach (Transform child in shipyardUI.transform) {
			if (child.name.Equals ("Ship(Clone)"))
				Destroy (child.gameObject);
		}
	}
	
}
