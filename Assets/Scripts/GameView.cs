using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameView : GameElement
{
	Text commoditiesText, luxuriesText, wealthText, shipText;
	public Sprite[] resourceSprites;
	void Awake()
	{
		commoditiesText = GameObject.Find("Commodities").GetComponentInChildren<Text>();
		luxuriesText = GameObject.Find("Luxuries").GetComponentInChildren<Text>();
		wealthText = GameObject.Find("Wealth").GetComponentInChildren<Text>();
		shipText = GameObject.Find("MaxShips").GetComponentInChildren<Text>();

	}
	// Use this for initialization
	void Start ()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			app.model.CancelBuild();
		}
	}
	void FixedUpdate()
	{
		UpdateResourceText ();
	}
	void UpdateShipCapacityText()
	{
		shipText.text = app.model.currentShips.ToString () + " / " + app.model.maxShips.ToString ();
	}

	public void DestroyShipUIInstances ()
	{
		GameObject shipyardUI;
		shipyardUI = GameObject.Find("ShipyardWindow");
		foreach (Transform child in shipyardUI.transform) {
			if (child.name.Equals ("Ship(Clone)"))
				Destroy (child.gameObject);
		}
	}

	public void UpdateResourceText()
	{
		commoditiesText.text = ":" + GameModel.resourcesMain.commodity.ToString();
		luxuriesText.text = ":" + GameModel.resourcesMain.luxury.ToString();
		wealthText.text = ":" + GameModel.resourcesMain.wealth.ToString();
	}

	public void ShipyardWindowPopulate (TradeMission currentTradeMission)
	{
		
		foreach (Building _shipyard in app.model.buildingList) {
			if (_shipyard.GetType ().Equals (Type.GetType ("Shipyard")))
				((Shipyard)_shipyard).CreateShipUI (currentTradeMission);
		}
	}
}

