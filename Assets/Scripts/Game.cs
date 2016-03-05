using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	//Singleton
	static public Game current;
	public GameObject currentlyBuilding;


	[SerializeField]
	public GameObject[]
		buildingPrefabs;
	// Use this for initialization
	void Start ()
	{
		if (current == null) {
			current = this;
		} else {
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			CancelBuild ();	
	}

	void CancelBuild ()
	{
		if (currentlyBuilding != null) {
			Destroy (currentlyBuilding);
			SetCurrentlyBuilding (null);
		}
	}

	public void SetCurrentlyBuilding (GameObject _currentlyBuilding)
	{

		currentlyBuilding = _currentlyBuilding;
	}

	public void toggleToolBar (string toolbarPosition)
	{
		GameObject toolbar = GameObject.Find (toolbarPosition + "Toolbar");
		if (toolbar != null) {
			CanvasGroup canvasGroup = toolbar.GetComponent<CanvasGroup> ();
			canvasGroup.alpha = (canvasGroup.alpha + 1) % 2; 
			canvasGroup.interactable = !canvasGroup.interactable;
			if (!canvasGroup.interactable)
				GameObject.Find (toolbarPosition + "Button").GetComponentInChildren<Text> ().text = "Show";
			else 
				GameObject.Find (toolbarPosition + "Button").GetComponentInChildren<Text> ().text = "Hide";
		}

	}

	public void StartBuilding (int _buildIndex)
	{
		CancelBuild ();
		Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currentMousePosition.z = 0;
		GameObject instance = (GameObject)Instantiate (buildingPrefabs [_buildIndex], currentMousePosition, Quaternion.identity);
		SetCurrentlyBuilding (instance);

	}

	public void toggleCanvasGroup (CanvasGroup _canvasgroup)
	{
		if (_canvasgroup.alpha == 0) {
			_canvasgroup.alpha = 1;
			_canvasgroup.interactable = true;
		} else {
			_canvasgroup.alpha = 0;
			_canvasgroup.interactable = false;
		}



	}

	public void DestroyGameObject(Object o)
	{
		Destroy (o);
	}



}
