using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour {

	bool validPlace = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		validPlace = false;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		this.transform.position = mousePos;

		Collider2D collider = GetComponent<Collider2D> ();

		if( !collider.IsTouchingLayers(LayerMask.GetMask("Water")))
		   {
			validPlace = true;

		}

		if (Input.GetMouseButton(0) && validPlace)
			Debug.Log (validPlace);
	}
}
