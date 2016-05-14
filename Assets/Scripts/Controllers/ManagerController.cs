using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ManagerController : GameElement
{

    Hashtable buildingCostsReferences;
	void IncrementProductionTicks ()
	{
		foreach (BuildingController building in app.controller.buildings) {
			building.ProductionTick ();
		}
	}

	void DecrementSailingShips ()
	{
		foreach (TradeMission missionScript in Factions.current.GetTradeMissionList()) {
			missionScript.SailTick (1);
		}
	}

	bool NextTurnCheck ()
	{
		foreach (BuildingController building in app.controller.buildings) {
			if (building.GetType () == Type.GetType ("ResourceBuildingController")) {
                if (building.GetComponent<ResourceBuildingModel>().resourcesMaxed)
                {
                    Debug.Log("ResourceCheck Failed");
                    EventSystem.OccurEvent("CapacityErrorPanel");
                    return false;
                }
			}
		}
		//Add building costs
		if (ShipAvailable()) {
			EventSystem.OccurEvent ("ShipErrorPanel");
			return false;
		}
		return true;
	}

	public void NextTurn ()
	{
		if (!NextTurnCheck ())
			return;
		IncrementProductionTicks ();
		DecrementSailingShips ();
	}
	
	public void NextTurnForce ()
	{
		IncrementProductionTicks ();
		DecrementSailingShips ();
	}
	public void StartBuilding (string _building)
	{
		app.model.manager.CancelBuild ();
        ResourceBundle buildCost = app.model.manager.buildingCostsReferences[_building] as ResourceBundle;
		if (!ManagerModel.resourcesMain.CompareBundle(buildCost)) return;// check to make sure build costs satisfy
		Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentMousePosition.z = 0;
		GameObject instance = (GameObject)Instantiate (ManagerModel.buildingHashtable [_building] as UnityEngine.Object, currentMousePosition, Quaternion.identity);
		app.model.manager.SetCurrentlyBuilding (instance);
	}
	public void toggleInteractableButton (Button b)
	{
		b.interactable = !b.interactable;
	}
	
	public void DestroyGameObject (UnityEngine.Object o)
	{
		Destroy (o);
	}
	
	public void DestroyGameObject (UnityEngine.Object o, float time)
	{
		Destroy (o, time);
	}

	public void PassAllResourceChecks ()
	{
		foreach (ResourceBuildingController building in app.controller.buildings)
			building.GetComponent<ResourceBuildingModel>().resourcesMaxed = false;
		NextTurn ();
	}

    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        if (p_target != this) return;
        switch (p_event_path)
        {
            case GameNotification.ResultResourceChange:
                EventSystem.OccurEvent("ResultPrefab", p_data);
                return;
            case GameNotification.AddResources:
                app.model.manager.addBundle((ResourceBundle)p_data[0]);
                return;
        }
    }

	public bool ShipAvailable ()
	{
		if (app.model.manager.currentShips == app.model.manager.maxShips) {return false;}
		return true;
	}
}

