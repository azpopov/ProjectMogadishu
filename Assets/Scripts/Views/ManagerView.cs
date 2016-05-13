using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ManagerView : GameElement
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
			app.model.manager.CancelBuild();
		}
	}
	void FixedUpdate()
	{
		UpdateResourceText ();
        UpdateShipCapacityText();
	}
	void UpdateShipCapacityText()
	{
		shipText.text = app.model.manager.currentShips.ToString () + " / " + app.model.manager.maxShips.ToString ();
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
		commoditiesText.text = ":" + ManagerModel.resourcesMain.commodity.ToString();
		luxuriesText.text = ":" + ManagerModel.resourcesMain.luxury.ToString();
		wealthText.text = ":" + ManagerModel.resourcesMain.wealth.ToString();
	}

	public void ShipyardWindowPopulate (TradeMission currentTradeMission)
	{
		foreach (BuildingController _shipyard in app.controller.buildings) {
            if (_shipyard is ShipyardController)
                app.Notify(GameNotification.ShipyardCreateShipUI, _shipyard, currentTradeMission);
		}
	}
}

