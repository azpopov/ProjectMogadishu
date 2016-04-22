using UnityEngine;
using System.Collections;

public class ShipNamePopUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable()
	{
		Destroy (this.gameObject, 4.0f);
	}
}
