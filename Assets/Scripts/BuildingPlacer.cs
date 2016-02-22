using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour {

	bool validPlace = false;

	// Use this for initialization
	void Start () {
		GameObject.Find ("Esc Text").GetComponent<CanvasGroup> ().alpha = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//Set to false
		validPlace = false;
		//
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		this.transform.position = mousePos;

		Collider2D collider = GetComponent<Collider2D> ();

		if( !collider.IsTouchingLayers(LayerMask.GetMask("Water")))
		   {
			validPlace = true;

		}

		if (validPlace && Input.GetMouseButtonDown(0)) {
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(this);

		}
	}

	void OnDestroy()
	{
		GameObject.Find ("Esc Text").GetComponent<CanvasGroup> ().alpha = 0;

	}
}
