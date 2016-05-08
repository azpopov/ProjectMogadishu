using UnityEngine;
using System.Collections;

public class unityColliderBug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Collider2D>().isTrigger = false;
    }
}
