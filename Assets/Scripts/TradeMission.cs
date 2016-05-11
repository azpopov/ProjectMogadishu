using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;

public class TradeMission : MonoBehaviour
{

	public float timeToDest, originalTime;
    public ResourceBundle requestResource, targetResource;
	public Sprite insignia;
	Image insigiaComp;
	Transform button;
	Text destText;
	bool sailing = false;
	private UnityAction action;
	public Faction f;
	public int resultBias = 0;
	public string shipName;


    public GameObject shipView, shipyardWindow;

    void Awake()
    {
        var _object = GameObject.Find("UI").GetComponentsInChildren(typeof(Transform), true);
        foreach(Component lookUp in _object){
            if (lookUp.gameObject.name.Equals("ShipView"))
                shipView = lookUp.gameObject;
            if (lookUp.gameObject.name.Equals("ShipyardWindow"))
                shipyardWindow = lookUp.gameObject;
        }
        
    }

	void Start ()
	{
		action = new UnityAction (CheckValidityOfSailing);
		action += Game.current.ShipCheck;
        
		button = transform.Find ("SendTradeButton");
		button.GetComponent<Button> ().onClick.AddListener (action);
		
		insigiaComp = transform.Find ("FactionInsignia").GetComponent<Image> ();

		insigiaComp.sprite = insignia;
		destText = transform.Find ("TripLength").GetComponent<Text> ();
        transform.Find("ResourcesRequested").Find("ResourceText").GetComponent<Text>().text = requestResource.ReturnMax().ToString();
		transform.Find ("ResourcesRequested").Find ("ResourceImage").GetComponent<Image> ().sprite = Game.current.resourceSprites [requestResource.ReturnTypeofMax()];
		transform.Find ("ResourcesRequested").Find ("TargetResourceImage").GetComponent<Image> ().sprite = Game.current.resourceSprites [targetResource.ReturnTypeofMax()];
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
				EventSystem.pendingMissions.Add(this);
				EventSystem.OccurEvent ("TradeComplete");
				CancelSailing ();
			}
		}
	}


	public void StartSailing (Shipyard.Ship _ship)
	{

        switch (requestResource.ReturnTypeofMax())
		{
		case 0:
                Game.current.addToResource(0, -requestResource.ReturnMax());
			break;
		case 1:
            Game.current.addToResource(1, -requestResource.ReturnMax());
			break;
		case 2:
            Game.current.addToResource(2, -requestResource.ReturnMax());
			break;
		}
        shipName = _ship.name;
		Game.current.currentShips++;
		sailing = true;
		button.GetComponent<Image> ().color = Color.red;
		button.transform.Find ("Text").GetComponent<Text> ().text = "Cancel Ship";
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		action -= CheckValidityOfSailing;
		action -= Game.current.ShipCheck;
		action += CancelSailing;
		button.GetComponent<Button> ().onClick.AddListener (action);
	}

    void CheckValidityOfSailing()
    {

        if (!Game.current.ShipAvailable())
            return;
        Debug.Log("ReachedCheckValiditySailing");
        if (!Game.resourcesMain.CompareBundle(requestResource)) return;
        shipView.SetActive(true);
        shipyardWindow.SetActive(true);
        Game.current.ShipyardWindowPopulate(this);
    }

	void CancelSailing ()
	{
		button.GetComponent<Button> ().onClick.RemoveListener (action);
		sailing = false;
		action -= CancelSailing;
	}
	
}
