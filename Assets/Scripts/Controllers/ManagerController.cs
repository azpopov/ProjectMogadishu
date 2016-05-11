using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ManagerController : GameController
{
	void IncrementProductionTicks ()
	{
		foreach (Building building in app.model.manager.buildingList) {
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
		foreach (Building building in app.model.manager.buildingList) {
			if (building.GetType () == Type.GetType ("ResourceBuilding") && ((ResourceBuilding)building).resourcesMaxed) {
				Debug.Log ("ResourceCheck Failed");
				EventSystem.OccurEvent ("CapacityErrorPanel");
				return false;
			}
		}
		//Add building costs
		if (!ShipAvailable()) {
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
		GameObject theBuilding = ManagerModel.buildingHashtable [_building] as GameObject;
		ResourceBundle buildCost = theBuilding.gameObject.GetComponent<Building>().GetBuildCost();
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
		foreach (ResourceBuilding building in app.model.manager.buildingList)
			building.resourcesMaxed = false;
		NextTurn ();
	}

	public bool ShipAvailable ()
	{
		if (app.model.manager.currentShips < app.model.manager.maxShips) {return false;}
		return true;
	}
}

