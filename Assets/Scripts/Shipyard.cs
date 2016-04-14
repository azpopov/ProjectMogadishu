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
	List<Ship> shipsInShipyard;

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
		GameObject.Find ("NewShipPopUpButton").GetComponent<Button> ().onClick.AddListener(() => CreateShip());
	}

	void GenerateEvent()
	{
		throw new System.NotImplementedException ();
	}

	protected override void OnDisable ()
	{
		Game.current.shipyardList.Remove (this);
	}

	protected override void OnMouseDown ()
	{
		throw new System.NotImplementedException ();
	}

	public void CreateShip()
	{
		Ship _ship = new Ship (GameObject.Find("InputField").GetComponent<InputField>().text);

		shipsInShipyard.Add (_ship);
		Debug.Log (shipsInShipyard [0].name);
		GameObject.Find ("NewShipPopUpButton").GetComponent<Button> ().onClick.RemoveListener(() => CreateShip());
	}
}
