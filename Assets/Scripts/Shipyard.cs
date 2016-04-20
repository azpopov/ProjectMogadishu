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
	// Use this for initialization
	void Start () {
		shipsInShipyard = new List<Ship> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		timeSinceTick++;
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
       Game.current.toggleCanvasGroup("NewShipPopUp");

		GameObject.Find ("NewShipPopUpButton").GetComponent<Button> ().onClick.AddListener(() => this.CreateShip());
	
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

	public void CreateShip()
	{
		
        
	//	Game.current.toggleCanvasGroup ("NewShipPopUp", true);
		GameObject ui;
	//		ui =  = GameObject.Find("NewShipPopUp");
		ui = Game.current.uiElements["NewShipPopUp"];
        ui.GetComponentInChildren<Button> ().onClick.RemoveAllListeners();
		Ship _ship = new Ship (ui.GetComponentInChildren<InputField>().text);

		this.shipsInShipyard.Add (_ship);

	}

	public void CreateShipUI(TradeMission mission)
	{

		GameObject shipyardUI = new GameObject();
        Game.current.uiElements.TryGetValue("ShipyardWindow", out shipyardUI);
		foreach (Ship _ship in shipsInShipyard) {
			GameObject instance = Instantiate(shipUIPrefab,new Vector3 (0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent (shipyardUI.transform, false);
			instance.GetComponentInChildren<Text>().text = _ship.name + this.transform.localPosition;
			Button instanceButton = instance.GetComponentInChildren<Button>();
			if(_ship.onMission)
			{
				instanceButton.interactable = false;
			}
			else
			{
				instanceButton.onClick.AddListener(() => mission.StartSailing(_ship.name));
				instanceButton.onClick.AddListener(() => Game.current.DestroyShipUIInstances());
			}
	}
}
}
