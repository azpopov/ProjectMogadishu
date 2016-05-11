using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Shipyard : Building
{

	public struct Ship
	{
		public Ship (string _name)
		{
			name = _name;
			theMission = null;
		}

		public string name;
		public TradeMission theMission;
	}
	[SerializeField]
	public List<Ship>
		shipsInShipyard;
	public GameObject shipUIPrefab;
	Button newShipPopUpButton;
	// Use this for initialization
	void Start ()
	{
		shipsInShipyard = new List<Ship> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
	}

	protected override void CheckProduction ()
	{
		foreach (Ship _ship in shipsInShipyard) {
			if (_ship.theMission != null && Random.Range (0, 10) < 3) {
				GenerateEvent ();
			}
		}
	}

	public override void ProductionTick ()
	{
		CheckProduction ();
	}

	protected override int GlowSprite {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	protected override void OnEnable ()
	{
        Game.resourcesMain -= GetBuildCost();
		Game.current.buildingList.Add (this);
		Game.current.maxShips += 1;
		EventSystem.OccurEvent ("NewShipPopUp");

	
	}

	void GenerateEvent ()
	{
		spriteRnd.sprite = glowSprite [0];

	}

	protected override void OnDisable ()
	{
		Game.current.buildingList.Remove (this);
	}

	protected override void OnMouseDown ()
	{

	}

	public void CreateShip (string _name)
	{
		Shipyard.Ship _ship = new Ship (_name);
		this.shipsInShipyard.Add (_ship);
	}

	public void CreateShipUI (TradeMission mission)
	{

		GameObject shipyardUI;
        shipyardUI = GameObject.Find("ShipyardWindow");
		foreach (Ship _ship in shipsInShipyard) {
			GameObject instance = Instantiate (shipUIPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent (shipyardUI.transform, false);
			instance.GetComponentInChildren<Text> ().text = _ship.name;
			Button instanceButton = instance.GetComponentInChildren<Button> ();
			if (_ship.theMission != null) {
				instanceButton.interactable = false;
			} else {
				instanceButton.onClick.AddListener (() => mission.StartSailing (_ship));
				instanceButton.onClick.AddListener (() => SetMission (_ship, mission));
				instanceButton.onClick.AddListener (() => Game.current.DestroyShipUIInstances ());
                instanceButton.onClick.AddListener(() => GameObject.Find("ShipyardWindow").gameObject.SetActive(!GameObject.Find("ShipyardWindow").gameObject.activeSelf));
			}
		}

        
	}

	public void SetMission (Ship _ship, TradeMission _mission)
	{
		_ship.theMission = _mission;
	}


    public override ResourceBundle GetBuildCost()
    {

        return new ResourceBundle(300, 200, 0);
    }
    
}
