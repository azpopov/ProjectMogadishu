using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShipyardModel : BuildingModel {
    public string shipList;
    public ShipyardModel.Ship eventShip;
	public class Ship
	{
		public Ship (string _name)
		{
			name = _name;
			theMission = null;
		}
        public Ship(string _name, TradeMission p_mission)
        {
            name = _name;
            theMission = p_mission;
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
        eventShip = null;
	}

    void Update()
    {
    }

	public void SetMission (Ship _ship, TradeMission _mission){
       
        _ship.theMission = _mission;
        
    }


    public override ResourceBundle GetBuildCost() { return BuildingCosts.shipyard; }
    
}
