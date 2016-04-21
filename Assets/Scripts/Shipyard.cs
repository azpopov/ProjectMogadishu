using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Shipyard : Building {

	public struct Ship{
		public Ship(string _name){
			name = _name;
			onMission = false;
			theMission = null;
		}
		public string name;
		public bool onMission;
		public TradeMission theMission;
	}
	[SerializeField]
	public List<Ship> shipsInShipyard;


	public GameObject shipUIPrefab;

	public float lived;

	Button newShipPopUpButton;
	// Use this for initialization
	void Start () {
		shipsInShipyard = new List<Ship> ();
		lived = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		lived += Time.deltaTime;
	}
	protected override void CheckProduction ()
	{
		foreach (Ship _ship in shipsInShipyard) {
			if (_ship.onMission && Random.Range(0,10) < 3){
				GenerateEvent();
			}
		}
	}


	public override void ProductionTick()
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
		Game.current.buildingList.Add (this);
		Game.current.maxShips += 1;
        EventSystem.OccurEvent("NewShipPopUp");

	
}

	void GenerateEvent()
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
	public void CreateShip(string _name){
		Shipyard.Ship _ship = new Ship (_name);
		this.shipsInShipyard.Add (_ship);
	}
	public void CreateShipUI(TradeMission mission)
	{

		GameObject shipyardUI;
		shipyardUI = Game.current.uiElements["ShipyardWindow"];
		foreach (Ship _ship in shipsInShipyard) {
			GameObject instance = Instantiate(shipUIPrefab,new Vector3 (0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent (shipyardUI.transform, false);
			instance.GetComponentInChildren<Text>().text = _ship.name;
			Button instanceButton = instance.GetComponentInChildren<Button>();
			if(_ship.onMission)
			{
				instanceButton.interactable = false;
			}
			else
			{
				instanceButton.onClick.AddListener(() => mission.StartSailing(_ship));
				instanceButton.onClick.AddListener(() => Game.current.DestroyShipUIInstances());
			}
	}
}
}
