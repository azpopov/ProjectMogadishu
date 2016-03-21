using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;

public class TradeMission : MonoBehaviour
{

	public float timeToDest, originalTime;
	public int type, targetType, requestedResources;
	public Sprite insignia;
	Image insigiaComp;
	Transform button;
	Text destText;
	bool sailing = false;
	private UnityAction action;
	public Factions.faction f;
	public int resultBias = 0;


	//
	void Start ()
	{
		action = new UnityAction (StartSailing);
		button = transform.Find ("SendTradeButton");
		button.GetComponent<Button> ().onClick.AddListener (action);
		
		insigiaComp = transform.Find ("FactionInsignia").GetComponent<Image> ();

		insigiaComp.sprite = insignia;
		destText = transform.Find ("TripLength").GetComponent<Text> ();

		transform.Find ("ResourcesRequested").Find ("ResourceText").GetComponent<Text> ().text = requestedResources.ToString ();
		transform.Find ("ResourcesRequested").Find ("ResourceImage").GetComponent<Image> ().sprite = ResourceManager.current.resourceSprites [type];
		transform.Find ("ResourcesRequested").Find ("TargetResourceImage").GetComponent<Image> ().sprite = ResourceManager.current.resourceSprites [targetType];
		timeToDest = originalTime;

		destText.text = "Est Trip Length: " + Math.Round (timeToDest).ToString () + "s";
	}

	void Update ()
	{
		if (sailing) {
			timeToDest -= Time.deltaTime;
			destText.text = "Arriving Back: " + Math.Round (timeToDest).ToString () + "s";
		}
		if (timeToDest < float.Epsilon) {
			Factions.current.CompleteJourney(this);
			CancelSailing ();
		}
	}

	void StartSailing ()
	{
		if (type == 0)
		if (ResourceManager.current.commodities < requestedResources)
			return;
		else
			ResourceManager.current.commodities -= requestedResources;
		if(type == 1)
			if(ResourceManager.current.luxuries < requestedResources)
				return;
		else
			ResourceManager.current.luxuries -= requestedResources;
		if(type == 2)
			if(ResourceManager.current.wealth < requestedResources)
				return;
		else
			ResourceManager.current.wealth -= requestedResources;
		if (!ResourceManager.current.ShipAvailable ())
			return;
		ResourceManager.current.SendTradeship ();
		sailing = true;
		button.GetComponent<Image> ().color = Color.red;
		button.transform.Find ("Text").GetComponent<Text> ().text = "Cancel Ship";
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		action -= StartSailing;
		action += CancelSailing;
		button.GetComponent<Button> ().onClick.AddListener (action);

		//((Button)button).onClick.RemoveListener =>
	}

	void CancelSailing ()
	{
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		sailing = false;
		action -= CancelSailing;
		Factions.current.RemoveTradeMission (this);
		Destroy (gameObject);

	}
	
}
