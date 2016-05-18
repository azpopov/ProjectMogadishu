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
			if (building is ResourceBuildingController) {
                if (building.GetComponent<ResourceBuildingModel>().resourcesMaxed)
                {
                    app.Notify(GameNotification.ErrorBuildingCapacityMax, app.controller.manager, building.GetType());
                    return false;
                }
			}
            else if (building is ShipyardController)
            {
                if (building.GetComponent<ShipyardModel>().eventShip != null)
                {
                    app.Notify(GameNotification.ErrorShipEventAvailable, app.controller.manager);
                    return false;
                }
            }
		}
		//Add building costs
		if (ShipAvailable()) {
            if(!ManagerModel.resourcesMain.CompareBundle(new ResourceBundle(400,300,100)))
                  return true;
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
        if (GetNetWorth() > app.model.manager.winningWorth)
            app.Notify(GameNotification.GameOver, this, "monetary");
        if (GetNetWorth() >= (app.model.manager.winningWorth / 2))
            app.controller.story.vascoStory = true;
	}
	
	public void NextTurnForce ()
	{
        ManagerModel.currentTurn++;
        app.controller.story.interestCounter--;
        app.controller.story.vascoCounter--;
		IncrementProductionTicks ();
        Factions.timeToNewMission--;
	}
	public void StartBuilding (string _building)
	{
		app.model.manager.CancelBuild ();
        ResourceBundle buildCost = app.model.manager.buildingCostsReferences[_building] as ResourceBundle;
        if (!ManagerModel.resourcesMain.CompareBundle(buildCost)) return;
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
                        if (building.GetComponent<EmbassyModel>().f.minDistance != 0 &&
                            building.GetComponent<EmbassyModel>().f.name.Equals(f.name))
                            (EmbassyModel.influenceBonuses[f.name]) = ((int)EmbassyModel.influenceBonuses[f.name]) + 1;
                return;
            case GameNotification.ErrorBuildingCapacityMax:
                EventSystem.OccurEvent("ErrorCapacityBuilding", p_data);
                return;
            case GameNotification.ErrorNoShipAvailable:
                EventSystem.OccurEvent("ErrorNoAvailableShip", p_data);
                return;

            case GameNotification.ErrorShipAvailable:
                EventSystem.OccurEvent("ErrorShipAvailable", p_data);
                return;
            case GameNotification.ErrorShipEventAvailable:
                EventSystem.OccurEvent("ErrorShipEventAvailable", p_data);
                return;
            case GameNotification.GameOver:
                EventSystem.OccurEvent("GameOverEvent", p_data);
                return;

        }
    }

	public bool ShipAvailable ()
	{
		if (app.model.manager.currentShips == app.model.manager.maxShips) {return false;}
		return true;
	}

    public int GetNetWorth()
    {
        ResourceBundle netWorth = new ResourceBundle(0,0,0);
        foreach (BuildingController building in app.controller.buildings)
        {
            if (building is ShipyardController)
                netWorth += BuildingCosts.shipyard;
            else if (building is ResourceBuildingController)
            {
                if (building.GetComponent<ResourceBuildingModel>().type == 0)
                    netWorth += BuildingCosts.farm;
                else if (building.GetComponent<ResourceBuildingModel>().type == 1)
                    netWorth += BuildingCosts.huntersLodge;
                else
                    netWorth += BuildingCosts.goldsmith;
            }
            else if (building is ShipyardController)
                netWorth += BuildingCosts.shipyard;
            else if (building is EmbassyController)
                netWorth += BuildingCosts.embassy;

        }
        netWorth += ManagerModel.resourcesMain;
        netWorth *= 1.2f;
        return ResourceBundle.ReturnWorth(netWorth);
    }
}

