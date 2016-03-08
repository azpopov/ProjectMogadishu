using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Event : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewShip(Text newName)
	{
		ResourceManager.current.ownedShips.Add (newName.text);
	}

	public void toggleWindow(CanvasGroup windowCG)
	{
		windowCG.alpha = (windowCG.alpha +1f)%2;
		windowCG.interactable = !windowCG.interactable;
		windowCG.blocksRaycasts = !windowCG.blocksRaycasts;
	}
}
