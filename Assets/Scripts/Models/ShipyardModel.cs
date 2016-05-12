using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShipyardModel : BuildingModel {

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


	// Use this for initialization
	void Start ()
	{
		shipsInShipyard = new List<Ship> ();
	}

	
	public void SetMission (Ship _ship, TradeMission _mission){_ship.theMission = _mission;}


    public override ResourceBundle GetBuildCost() { return BuildingCosts.shipyard; }
    
}
