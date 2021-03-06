﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;

public class TradeMission : GameElement
{

	public float timeToDest, originalTime;
    public ResourceBundle requestResource;
    public int targetResource;
	public Sprite insignia;
	Image insigiaComp;
	Transform button;
	Text destText;
	public bool sailing = false;
	private UnityAction action;
	public Faction f;


    public GameObject shipView, shipyardWindow;

    void Awake()
    {
        var _object = GameObject.Find("UI").GetComponentsInChildren(typeof(Transform), true);
        foreach(Component lookUp in _object){
            if (lookUp.gameObject.name.Equals("ShipView"))
                shipView = lookUp.gameObject;
            if (lookUp.gameObject.name.Equals("ShipyardWindow"))
                shipyardWindow = lookUp.gameObject;
        }
        
    }

	void Start ()
	{
		action = new UnityAction (CheckValidityOfSailing);
        
		button = transform.Find ("SendTradeButton");
		button.GetComponent<Button> ().onClick.AddListener (action);
        transform.Find("CancelMissionButton").GetComponent<Button>().onClick.AddListener(() => Factions.current.RemoveTradeMission(this));
		insigiaComp = transform.Find ("FactionInsignia").GetComponent<Image> ();

		insigiaComp.sprite = insignia;
		destText = transform.Find ("TripLength").GetComponent<Text> ();
        transform.Find("ResourcesRequested").Find("ResourceText").GetComponent<Text>().text = requestResource.ReturnMax().ToString();
		transform.Find ("ResourcesRequested").Find ("ResourceImage").GetComponent<Image> ().sprite = app.view.manager.resourceSprites [requestResource.ReturnTypeofMax()];
		transform.Find ("ResourcesRequested").Find ("TargetResourceImage").GetComponent<Image> ().sprite = app.view.manager.resourceSprites [targetResource];
		timeToDest = originalTime;

		destText.text = "Est Trip Length: " + Math.Round (timeToDest).ToString () + " Turns";
	}

	void FixedUpdate ()
	{
        destText.text = "Arriving Back in " + Math.Round(timeToDest).ToString() + " Turns";
	}

	public void SailTick(int n)
	{
		if (sailing) {
			timeToDest -= n;
		}
	}


	public void StartSailing (ShipyardModel.Ship _ship)
	{

        switch (requestResource.ReturnTypeofMax())
		{
		case 0:
                app.model.manager.addToResource(0, -requestResource.ReturnMax());
			break;
		case 1:
			app.model.manager.addToResource(1, -requestResource.ReturnMax());
			break;
		case 2:
			app.model.manager.addToResource(2, -requestResource.ReturnMax());
			break;
		}
		app.model.manager.currentShips++;
		sailing = true;
		button.GetComponent<Image> ().color = Color.red;
		button.transform.Find ("Text").GetComponent<Text> ().text = "Cancel Ship";
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		action -= CheckValidityOfSailing;
		action += CancelSailing;
		button.GetComponent<Button> ().onClick.AddListener (action);
	}

    void CheckValidityOfSailing()
    {

        if (!app.controller.manager.ShipAvailable())
        {
            app.Notify(GameNotification.ErrorNoShipAvailable, app.controller.manager);
            return;
        }
        if (!ManagerModel.resourcesMain.CompareBundle(requestResource)) {
            app.Notify(GameNotification.ErrorTradeMission, app.controller.manager);
            return; }
        shipView.SetActive(true);
        shipyardWindow.SetActive(true);
        app.view.manager.DestroyShipUIInstances();
        app.view.manager.ShipyardWindowPopulate(this);
    }

	public void CancelSailing ()
	{
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		sailing = false;
		action -= CancelSailing;
	}
	
}
