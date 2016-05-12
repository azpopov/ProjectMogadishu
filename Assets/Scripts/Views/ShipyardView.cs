using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipyardView : BuildingView {

	
	public GameObject shipUIPrefab;


	public void CreateShipUI (TradeMission mission)
	{

		GameObject shipyardUI;
        shipyardUI = GameObject.Find("ShipyardWindow");
		foreach (ShipyardModel.Ship _ship in gameObject.GetComponent<ShipyardModel>().shipsInShipyard) {
			GameObject instance = Instantiate (shipUIPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent (shipyardUI.transform, false);
			instance.GetComponentInChildren<Text> ().text = _ship.name;
			Button instanceButton = instance.GetComponentInChildren<Button> ();
			if (_ship.theMission != null) {
				instanceButton.interactable = false;
			} else {
				instanceButton.onClick.AddListener (() => mission.StartSailing(_ship));
				instanceButton.onClick.AddListener (() => gameObject.GetComponent<ShipyardModel>().SetMission (_ship, mission));
				instanceButton.onClick.AddListener (() => app.view.manager.DestroyShipUIInstances ());
                instanceButton.onClick.AddListener(() => GameObject.Find("ShipyardWindow").gameObject.SetActive(!GameObject.Find("ShipyardWindow").gameObject.activeSelf));
			}
		}

        
	}

    public void NewShip()
    { 
        
    }
    
}
