using UnityEngine;
using System.Collections;

public class TravelEventStormScript : CustomEvent {

    ShipyardModel.Ship ship;
	// Use this for initialization
	void Start () {
	    if(data != null)
            ship = (ShipyardModel.Ship)data[0];
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("data psss succeesffulll");
	}

}
