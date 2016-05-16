using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ManagerController : GameElement
{

    Dictionary<string, ResourceBundle> buildingCostsReferences;
	void IncrementProductionTicks ()
	{
		foreach (BuildingController building in app.controller.buildings) {
			building.ProductionTick ();
		}
	}

	bool NextTurnCheck ()
	{
		foreach (BuildingController building in app.controller.buildings) {
			if (building.GetType () == Type.GetType ("ResourceBuildingController")) {
                if (building.GetComponent<ResourceBuildingModel>().resourcesMaxed)
                {
                    app.Notify(GameNotification.ErrorBuildingCapacityMax, app.controller.manager, building.GetType());
                    return false;
                }
			}
		}
		//Add building costs
		if (ShipAvailable()) {
            app.Notify(GameNotification.ErrorShipAvailable, app.controller.manager);
			return false;
		}
		return true;
	}

	public void NextTurn ()
	{
		if (!NextTurnCheck ())
			return;
        NextTurnForce();
	}
	
	public void NextTurnForce ()
	{
		IncrementProductionTicks ();
        Factions.timeToNewMission--;
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
            case GameNotification.EmbassyFactionInfluence:
                Faction f = ((Faction)p_data[0]);
                
                foreach (BuildingController building in app.controller.buildings)
                    if (building is EmbassyController)
                        if (building.GetComponent<EmbassyModel>().f.name.Equals(f.name))
                            (EmbassyModel.influenceBonuses[f.name]) = ((int)EmbassyModel.influenceBonuses[f.name]) + 1;
                return;
            case GameNotification.ErrorBuildingCapacityMax:
                EventSystem.OccurEvent("ErrorCapacityBuilding", p_data);
                return;
            case GameNotification.ErrorNoShipAvailable:
                EventSystem.OccurEvent("ErrorNoAvailableShip");
                return;

            case GameNotification.ErrorShipAvailable:
                EventSystem.OccurEvent("ErrorShipAvailable");
                return;

        }
    }

	public bool ShipAvailable ()
	{
		if (app.model.manager.currentShips == app.model.manager.maxShips) {return false;}
		return true;
	}
}

