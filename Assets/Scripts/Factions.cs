using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Factions : MonoBehaviour
{

	public static Factions current = null;
	public List<TradeMission> tradeMissions = new List<TradeMission> ();
	GameObject tradeWindow, completeTradeWindow;
	public GameObject tradePrefab, completeTradeWindowPrefab;
	public enum faction
	{
		Celestial,
		Omani
	}
	;
	Dictionary<faction, float> factionBiases = new Dictionary<faction, float> ();
	Dictionary<int, string> resourceTypes = new Dictionary<int,string>();
//	Dictionary<faction, > d = new Dictionary<string, int>()
//	{
//		{"cat", 2},
//		{"dog", 1},
//		{"llama", 0},
//		{"iguana", -1}
//	};
	public Sprite[] insignias;
	float refreshTimer = 5f;

	// Use this for initialization
	void Start ()
	{
		if (current == null)
			current = this;
		else
			Destroy (this);
		
		factionBiases.Add (faction.Celestial, 1.5f);
		factionBiases.Add (faction.Omani, 0.8f);
		resourceTypes.Add (0, "Commodities");
		resourceTypes.Add (1, "Luxuries");
		resourceTypes.Add (2, "Wealth");
		tradeWindow = GameObject.Find ("TradeWindow").gameObject;
		for (int i = 0; i < 5; i++) 
		{
			CreateTradeRoute ();
		}


	}


	// Update is called once per frame
	void Update ()
	{
		refreshTimer -= Time.deltaTime;
		if (refreshTimer < float.Epsilon) {
			refreshTimer = 60f + UnityEngine.Random.Range (-10f, +10f);
			for(int i = tradeMissions.Count; i < 5; i++)
			{
				CreateTradeRoute ();
			}
		}


	}

	GameObject CreateTradeRoute ()
	{
		faction rndFaction = GetRandomEnum<faction> ();

		GameObject instance = (GameObject)Instantiate (tradePrefab, new Vector3 (0, 0), Quaternion.identity);
		instance.transform.SetParent (tradeWindow.transform, false);
		TradeMission tradeMissionScript = instance.GetComponent<TradeMission> ();
		int resourceType = 0, targetType = 0;



		switch (rndFaction) {
		case faction.Celestial:
			tradeMissionScript.insignia = insignias [0];
			tradeMissionScript.originalTime = UnityEngine.Random.Range (100f, 250f);
			resourceType = UnityEngine.Random.Range (1, 3);
			targetType = UnityEngine.Random.Range (2,3);
			break;
		case faction.Omani:
			tradeMissionScript.insignia = insignias [1];
			tradeMissionScript.originalTime = UnityEngine.Random.Range (3f, 10f);
			resourceType = UnityEngine.Random.Range (0, 3);
			targetType = UnityEngine.Random.Range(0,3);

			break;
		default:
			break;
		}

		tradeMissionScript.f = rndFaction;
		tradeMissionScript.requestedResources = GetResourceAmount(factionBiases[rndFaction], resourceType);
		tradeMissionScript.targetType = targetType;
		tradeMissionScript.type = resourceType;
		tradeMissions.Add (tradeMissionScript);
		return instance;
	}

	static T GetRandomEnum<T> ()
	{
		System.Array A = System.Enum.GetValues (typeof(T));
		T V = (T)A.GetValue (UnityEngine.Random.Range (0, A.Length));
		return V;
	}

	int GetResourceAmount (float _factionBias, int type)
	{
		int baseValue = 0;
		if (type == 0) {
			baseValue = UnityEngine.Random.Range (300, 600);
		} else if (type == 1) {
			baseValue = UnityEngine.Random.Range (100, 200);
		} else {
			baseValue = UnityEngine.Random.Range (50, 150);
		}

		baseValue = (int)((float)baseValue * _factionBias);
		return baseValue;
	}

	public void RemoveTradeMission(TradeMission _removedTrader)
	{
		tradeMissions.Remove (_removedTrader);
		ResourceManager.current.TradeshipReturn ();
	}



	public void CompleteJourney(TradeMission script)
	{
		script.resultBias += UnityEngine.Random.Range (0, 100);

		completeTradeWindow = (GameObject)Instantiate (completeTradeWindowPrefab, new Vector3 (0, 0), Quaternion.identity);
		completeTradeWindow.transform.SetParent (GameObject.Find("UI").transform, false);

		int amountReceived = 0;
		string resultText = "The "+script.shipName + " has returned!\nThe Captain would like to report:\n ";



		if (script.resultBias <= 20) {
			resultText += "DISASTROUS!\n";
			amountReceived = DisasterJourney (script);
			resultText += "You haven't received anything sadly";
		} else if (script.resultBias <= 80) {
			resultText += "SUCCESSFUL!\n ";
			amountReceived = SuccessfulJourney (script);
			resultText += "Resources Sent: "+script.requestedResources.ToString() + resourceTypes[script.type]+"\n";
			resultText += "Resources Received: "+amountReceived.ToString() + resourceTypes[script.targetType]+"\n";
		
		} else {
			resultText += "AMAZINGLY SUCCESSFUL!\n";
			amountReceived = AmazingJourney (script);

		}


		completeTradeWindow.transform.FindChild ("TradeCompleteText").GetComponent<Text> ().text = resultText;
		ResourceManager.current.addToResource (script.targetType, amountReceived);

	}
	int SuccessfulJourney(TradeMission script)
	{
		float amountReturned = (float)script.requestedResources;
		amountReturned *= factionBiases[script.f];

		if (script.type == 0 && script.targetType == 1) {
			amountReturned /= 2f;
		} else if (script.type == 0 && script.targetType == 2) {
			amountReturned /= 6f;
		} else if (script.type == 1 && script.targetType == 2) {
			amountReturned *= 0.6f;
		} else if (script.type == 1 && script.targetType == 0) {
			amountReturned *= 2.2f;
		} else if (script.type == script.targetType)
			amountReturned *= 1.7f;

		return (int)amountReturned;
	}
	int DisasterJourney(TradeMission script)
	{
		Debug.Log ("disaster not implemented");
		return 0;
	}
	int AmazingJourney(TradeMission script)
	{
		Debug.Log ("Amazing not implemented");
		return 0;	
	}

}
	
