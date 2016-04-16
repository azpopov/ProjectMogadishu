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
	public string shipName;

	//
	void Start ()
	{
		action = new UnityAction (CheckValidityOfSailing);
		action += Game.current.ShipCheck;
		button = transform.Find ("SendTradeButton");
		button.GetComponent<Button> ().onClick.AddListener (action);
		
		insigiaComp = transform.Find ("FactionInsignia").GetComponent<Image> ();

		insigiaComp.sprite = insignia;
		destText = transform.Find ("TripLength").GetComponent<Text> ();

		transform.Find ("ResourcesRequested").Find ("ResourceText").GetComponent<Text> ().text = requestedResources.ToString ();
		transform.Find ("ResourcesRequested").Find ("ResourceImage").GetComponent<Image> ().sprite = Game.current.resourceSprites [type];
		transform.Find ("ResourcesRequested").Find ("TargetResourceImage").GetComponent<Image> ().sprite = Game.current.resourceSprites [targetType];
		timeToDest = originalTime;

		destText.text = "Est Trip Length: " + Math.Round (timeToDest).ToString () + " Turns";
	}

	void Update ()
	{

	}

	public void SailTick(int n)
	{
		if (sailing) {
			timeToDest -= n;
			destText.text = "Arriving Back in " + Math.Round (timeToDest).ToString () + " Turns";
			if (timeToDest < float.Epsilon) {
				Factions.current.CompleteJourney (this);
				CancelSailing ();
			}
		}
	}


	public void StartSailing ()
	{
		switch (type)
		{
		case 0:
			Game.current.commodities -= requestedResources;
			break;
		case 1:
			Game.current.luxuries -= requestedResources;
			break;
		case 2:
			Game.current.wealth -= requestedResources;
			break;
		}

		Game.current.currentShips++;
		sailing = true;
		button.GetComponent<Image> ().color = Color.red;
		button.transform.Find ("Text").GetComponent<Text> ().text = "Cancel Ship";
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		action -= CheckValidityOfSailing;
		action -= Game.current.ShipCheck;
		action += CancelSailing;
		button.GetComponent<Button> ().onClick.AddListener (action);

		//((Button)button).onClick.RemoveListener =>
	} 

	void CheckValidityOfSailing()
	{
		if (!Game.current.ShipAvailable ())
			return;
		if (!(type == 0 && !(Game.current.commodities < requestedResources)) &&
		    !(type == 2 && !(Game.current.wealth < requestedResources)) &&
		    !(type == 1 && !(Game.current.luxuries < requestedResources)))
			return;
		Game.current.toggleCanvasGroup ("ShipView");
		Game.current.toggleCanvasGroup ("ShipyardWindow");
		Game.current.ShipyardWindowPopulate (this);
	

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
