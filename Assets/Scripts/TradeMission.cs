using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TradeMission : MonoBehaviour
{
	
	GameObject uiRepresentation;
	public float timeToDest, originalTime;

	public int type;
	public Sprite insignia;
	Image insigiaComp;
	public int requestedResources;


	public int expectedReturn;

	Text destText;

	bool sailing = false;
	void Start ()
	{
		uiRepresentation = gameObject;
//		foreach (Transform child in transform) {
//			if(String.Equals(child.transform.name,"FactionInsignia") )
//				insigiaComp = child.GetComponent<Image> ();
//		}
		insigiaComp = transform.Find ("FactionInsignia").GetComponent<Image> ();
		insigiaComp.sprite = insignia;
		destText = transform.Find ("TripLength").GetComponent<Text> ();
		destText.text = "Trip Length: " + Math.Round (timeToDest).ToString () + "s";
		transform.Find ("ResourcesRequested").Find("ResourceText").GetComponent<Text> ().text = requestedResources.ToString ();
		transform.Find ("ResourcesRequested").Find ("ResourceImage").GetComponent<Image> ().sprite = ResourceManager.current.resourceSprites [type];
		timeToDest = originalTime;
	}

	void Update ()
	{
		if (sailing) {
			timeToDest -= Time.deltaTime;
			destText.text = "Trip Length: " + Math.Round (timeToDest).ToString () + "s";
		}
		}




	
}
