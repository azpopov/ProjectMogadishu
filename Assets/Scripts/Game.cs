using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	//Singleton
	static public Game current;
	public GameObject currentlyBuilding;


	[SerializeField]
	public GameObject[]
		buildingPrefabs;
	public Sprite[] resourceSprites;

	//ShipManagement
	int _maxShips = 0;
	int _currentShips = 0;
	public int currentShips
	{
		get{
			return _currentShips;
		}
		set
		{
			_currentShips = value;
			ShipCheck();
			shipText.text = currentShips.ToString() + " / "+_maxShips.ToString();
		}
	}
	public List<string> ownedShips = new List<string>();



	//List of current Buildings
	public List<ResourceBuilding> resourceBuildingList;
	
	Text commoditiesText, luxuriesText, wealthText, shipText;
	void Awake(){
		if (current == null) {
			current = this;
		} else {
			Destroy (this);
		}
		resourceBuildingList = new List<ResourceBuilding> ();
		commoditiesText = GameObject.Find ("Commodities").GetComponentInChildren<Text> ();
		luxuriesText = GameObject.Find ("Luxuries").GetComponentInChildren<Text> ();
		wealthText = GameObject.Find ("Wealth").GetComponentInChildren<Text> ();
		shipText = GameObject.Find ("MaxShips").GetComponentInChildren<Text> ();
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		//If presses Escape while holding bUilding, cancel it.
		if (Input.GetKeyDown (KeyCode.Escape))
			CancelBuild ();	
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
	void IncrementProductionTicks()
	{
		foreach (ResourceBuilding building in resourceBuildingList) {
			building.productionTick(1);
		}
	}

	void DecrementSailingShips ()
	{
		foreach (TradeMission missionScript in Factions.current.tradeMissions) {
			missionScript.SailTick(1);
		}
	}
	bool shipCheck = true;
	//Check all booleans here. Returning true means go ahead with NextTurn
	bool NextTurnCheck()
	{
		foreach (ResourceBuilding building in resourceBuildingList) {
			if(building.resourcesMaxed)
			{
				Debug.Log("ResourceCheck Failed");
				toggleCanvasGroup(GameObject.Find("CapacityErrorPanel").GetComponent<CanvasGroup>());
				toggleInteractableButton(GameObject.Find("EndTurnButton").GetComponent<Button>());
				return false;
			}
		}
		//Add building costs
		if (!shipCheck) {
			Debug.Log("ShipCheckFailed");
			toggleCanvasGroup(GameObject.Find("ShipErrorPanel").GetComponent<CanvasGroup>());
			toggleInteractableButton(GameObject.Find("EndTurnButton").GetComponent<Button>());
		}
		return true;
	}
	//Perform all between methods here
	public void NextTurn()
	{
		if (!NextTurnCheck ())
			return;
		IncrementProductionTicks ();
		DecrementSailingShips ();

		ShipCheck ();

	}
	public void ShipCheck()
	{
		shipCheck = !ShipAvailable();
	}
	public void NextTurnForce()
	{
		IncrementProductionTicks();
		DecrementSailingShips();
	}

	public void SetShipCheck(bool b)
	{
		shipCheck = b;
	}

	//BUILDER METHODS
	//Set method for currentlyBuilding
	public void SetCurrentlyBuilding (GameObject _currentlyBuilding)
	{

		currentlyBuilding = _currentlyBuilding;
	}
	

	public void StartBuilding (int _buildIndex)
	{
		CancelBuild ();
		Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentMousePosition.z = 0;
		GameObject instance = (GameObject)Instantiate (buildingPrefabs [_buildIndex], currentMousePosition, Quaternion.identity);
		SetCurrentlyBuilding (instance);

	}


	//OTHER METHODS

	public void toggleCanvasGroup (CanvasGroup _canvasgroup)
	{

		_canvasgroup.alpha = (_canvasgroup.alpha + 1) % 2; 
		_canvasgroup.interactable = !_canvasgroup.interactable;
		_canvasgroup.blocksRaycasts = !_canvasgroup.blocksRaycasts;

	}

	public void toggleInteractableButton(Button b)
	{
		b.interactable = !b.interactable;
	}


	public void DestroyGameObject(Object o)
	{
		Destroy (o);
	}

	//RESOURCE MANAGEMENT METHODS

	int _commodities = 500;
	int _luxuries = 500;
	int _wealth = 500;

	public int commodities {
		get {return _commodities;}
		set{
			_commodities = value ;
			commoditiesText.text =  ":"+_commodities.ToString();
		}
	}
	public int luxuries
	{
		get {return _luxuries;}
		set{ _luxuries = value;
			luxuriesText.text =  ":"+_luxuries.ToString();}
	}
	public int wealth
	{
		get {return _wealth;}
		set{ _wealth = value ;
			wealthText.text = ":"+_wealth.ToString();}
	}
	


	public void addToResource(int _type, int _amount)
	{
		if (_type == 0) {
			commodities += _amount;
		} else if (_type == 1) {
			luxuries += _amount;
		} else
			wealth += _amount;
	}


	public void PassAllResourceChecks()
	{
		foreach(ResourceBuilding building in resourceBuildingList)
			building.resourcesMaxed = false;
		NextTurn();
	}

	//SHIP MANAGEMENT METHODS

	public int maxShips
	{
		get
		{
			return _maxShips;
		}
		set
		{
			_maxShips = value;
			ShipCheck();
			shipText.text = currentShips.ToString() + " / "+_maxShips.ToString();
		}
	}

	public bool ShipAvailable()
	{
		if (currentShips >= maxShips) {
			return false;
		}
		return true;
	}

	public void AddNewShipName()
	{
		
		
		ownedShips.Add(GameObject.Find("InputField").GetComponent<InputField>().text);
		Debug.Log(GameObject.Find("InputField").GetComponent<InputField>().text);
		
	}
}
