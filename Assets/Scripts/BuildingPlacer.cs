using UnityEngine;
using System.Collections;
using System;
public class BuildingPlacer : MonoBehaviour {

	bool validPlace = false;
	Collider2D col;
	CanvasGroup newShipWindow;
	// Use this for initialization
	void Start () {
		//set UI aplha to visible
		GameObject.Find ("Esc Text").GetComponent<CanvasGroup> ().alpha = 1;
		newShipWindow = GameObject.Find ("NewShipPopUp").GetComponent<CanvasGroup> ();
		//Get the Collider
		col = GetComponent<Collider2D> ();
	}



	// Update is called once per frame
	void Update () {
		//Set to false
		validPlace = false;
		//Get current mouse coord
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Put the Z axis to 0 so not behind camera
		mousePos.z = 0;
		//Whatever this script is attached to, follow mouse.
		this.transform.position = mousePos;
		if (transform.name.Equals ("ShipYard(Clone)")) 
		{
			if( col.IsTouchingLayers(LayerMask.GetMask("Water")) && !col.IsTouchingLayers(LayerMask.GetMask("Building")) && col.IsTouchingLayers(LayerMask.GetMask("Buildable")))
				validPlace = true;

		}
		//As long as it's not touching Water
		else if( !col.IsTouchingLayers(LayerMask.GetMask("Water")) && !col.IsTouchingLayers(LayerMask.GetMask("Building")))
		   {
			validPlace = true;

		}
		//If able to place & valid location
		if (validPlace && Input.GetMouseButtonDown(0)) {
			//Destroy components
			Game.current.SetCurrentlyBuilding(null);
			if(GetComponent<ResourceBuilding>() != null)
				GetComponent<ResourceBuilding>().enabled = true;
			else 
			{
				ResourceManager.current.maxShips += 1;
			//	GameObject instance = Instantiate(newShipWindow, new Vector3 (0, 0), Quaternion.identity) as GameObject;
				//instance.transform.SetParent (GameObject.Find("UI").transform, false);
				newShipWindow.alpha = 1;
				newShipWindow.interactable = true;
				newShipWindow.blocksRaycasts = true;
			}
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(this);
			
		}
	}

	void OnDestroy()
	{
		GameObject.Find ("Esc Text").GetComponent<CanvasGroup> ().alpha = 0;

	}
}
