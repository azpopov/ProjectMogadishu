using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TradeMission : MonoBehaviour{
	
	GameObject uiRepresentation;
	public float timeToDest;
	public int type;

	public Sprite insignia;
	Image insigiaComp;

	void Start()
	{
		uiRepresentation = gameObject;
//		foreach (Transform child in transform) {
//			if(String.Equals(child.transform.name,"FactionInsignia") )
//				insigiaComp = child.GetComponent<Image> ();
//		}
		insigiaComp = transform.Find ("FactionInsignia").GetComponent<Image> ();
		insigiaComp.sprite = insignia;
		transform.Find ("TripLength").GetComponent<Text>().text = "Trip Length: "+Math.Round(timeToDest).ToString()+"s";

	}





	
}
