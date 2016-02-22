using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {
	//Singleton
	static public Game current;

	public GameObject currentlyBuilding;
	// Use this for initialization
	void Start () {
		if (current == null) {
			current = this;
		}
		else{
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			CancelBuild ();	
	}


	void CancelBuild()
	{
		if (currentlyBuilding != null) {
			Destroy(currentlyBuilding);
			setCurrentlyBuilding(null);
		}
	}

	public void setCurrentlyBuilding(GameObject _currentlyBuilding)
	{

		currentlyBuilding = _currentlyBuilding;
	}

	public void toggleToolBar(string toolbarPosition)
	{
		GameObject toolbar = GameObject.Find (toolbarPosition +"Toolbar");
		if (toolbar != null)
		{
			CanvasGroup canvasGroup = toolbar.GetComponent<CanvasGroup> ();
			if (canvasGroup.alpha == 1) 
			{
				canvasGroup.alpha = 0;
				canvasGroup.interactable = false;
				GameObject.Find(toolbarPosition+"Button").GetComponentInChildren<Text>().text = "Show";

			} else {
				canvasGroup.alpha = 1;
				canvasGroup.interactable = true;
				GameObject.Find(toolbarPosition+"Button").GetComponentInChildren<Text>().text = "Hide";
			}
		}
	}
}
