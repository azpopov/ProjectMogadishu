using UnityEngine;
using System.Collections;
using System;
public class BuildingPlacer : GameElement {

	bool validPlace = false;
	Collider2D col;
    Color green = new Color(0, 240, 0);
    Color red = new Color(220, 0, 0);
    SpriteRenderer rndSprite;
	// Use this for initialization
	void Start () {
		//set UI aplha to visible
		GameObject.Find ("Esc Text").GetComponent<CanvasGroup> ().alpha = 1;
		//Get the Collider
		col = GetComponent<Collider2D> ();
        rndSprite = GetComponent<SpriteRenderer>();
	}

    void OnEnable()
    {
        GameObject.Find("background").gameObject.layer = LayerMask.NameToLayer("Buildable");
        GameObject.Find("Water").gameObject.layer = LayerMask.NameToLayer("Water");
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
		if(!col.IsTouchingLayers(LayerMask.GetMask("Building")) && !col.IsTouchingLayers(LayerMask.GetMask("NonBuildable"))){
		if (transform.name.Equals ("shipyard(Clone)")) {
          
			if( col.IsTouchingLayers(LayerMask.GetMask("Water")) && col.IsTouchingLayers(LayerMask.GetMask("Buildable"))){
				validPlace = true;
			}
			}
		//As long as it's not touching Water
		else if(!col.IsTouchingLayers(LayerMask.GetMask("Water")))
		   {
			validPlace = true;

		}
		}
        if (validPlace)
            rndSprite.color = green;
        else
            rndSprite.color = red;
		//If able to place & valid location
		if (validPlace && Input.GetMouseButtonDown(0)) {
			//Destroy components
			app.model.manager.SetCurrentlyBuilding(null);
			gameObject.layer = LayerMask.NameToLayer("Building");
            if (GetComponent<ResourceBuildingController>() != null)
                GetComponent<ResourceBuildingController>().enabled = true;
			else if(GetComponent<ShipyardController>() != null)
			{
                GetComponent<ShipyardController>().enabled = true;

			}
			else if(GetComponent<BuildingController>()  != null)
			{
                GetComponent<BuildingController>().enabled = true;
			}
            rndSprite.color = new Color(255, 255, 255, 255);
			Destroy(GetComponent<Rigidbody2D>());

			Destroy(this);
			
		}
	}

	void OnDestroy()
	{
		GameObject.Find ("Esc Text").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("background").gameObject.layer = 2;
        GameObject.Find("Water").gameObject.layer = 2;

	}

	void LateUpdate()
	{
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y);
	}



}
