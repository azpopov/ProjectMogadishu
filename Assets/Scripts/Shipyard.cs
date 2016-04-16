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


	public void ProductionTick()
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
		Game.current.shipyardList.Add (this);
		Game.current.maxShips += 1;
		CanvasGroup newShipWindow = GameObject.Find ("NewShipPopUp").GetComponent<CanvasGroup>();
		if (newShipWindow == null)
			return;
		newShipWindow.alpha = 1;
		newShipWindow.interactable = true;
		newShipWindow.blocksRaycasts = true;
		GameObject.Find ("NewShipPopUpButton").GetComponent<Button> ().onClick.AddListener(() => this.CreateShip());
	}

	void GenerateEvent()
	{
		spriteRnd.sprite = glowSprite [0];

	}

	protected override void OnDisable ()
	{
		Game.current.shipyardList.Remove (this);
	}

	protected override void OnMouseDown ()
	{

	}

	public void CreateShip()
	{
		GameObject.Find ("NewShipPopUpButton").GetComponent<Button> ().onClick.RemoveAllListeners();
		Ship _ship = new Ship (GameObject.Find("InputField").GetComponent<InputField>().text);

		this.shipsInShipyard.Add (_ship);

	}

	public void CreateShipUI(TradeMission mission)
	{

		GameObject shipyardUI = GameObject.Find ("ShipyardWindow");
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
