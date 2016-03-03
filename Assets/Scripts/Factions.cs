using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Factions : MonoBehaviour
{

	public static Factions current = null;
	public List<TradeMission> tradeMissions = new List<TradeMission> ();
	GameObject tradeWindow;
	public GameObject tradePrefab;
	public enum faction
	{
		Celestial,
		Omani
	}
	;
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
		int resourceType = 0;



		switch (rndFaction) {
		case faction.Celestial:
			tradeMissionScript.insignia = insignias [0];
			tradeMissionScript.originalTime = UnityEngine.Random.Range (100f, 250f);
			resourceType = UnityEngine.Random.Range (1, 3);
			tradeMissionScript.f = faction.Celestial;
			tradeMissionScript.requestedResources = GetResourceAmount(1.5f, resourceType);
			break;
		case faction.Omani:
			tradeMissionScript.insignia = insignias [1];
			tradeMissionScript.originalTime = UnityEngine.Random.Range (30f, 100f);
			resourceType = UnityEngine.Random.Range (0, 3);
			tradeMissionScript.f = faction.Omani;
			tradeMissionScript.requestedResources = GetResourceAmount(0.8f, resourceType);
			break;
		default:
			break;
		}
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

	int GetResourceAmount (float factionBias, int type)
	{
		int baseValue = 0;
		if (type == 0) {
			baseValue = UnityEngine.Random.Range (300, 600);
		} else if (type == 1) {
			baseValue = UnityEngine.Random.Range (100, 200);
		} else {
			baseValue = UnityEngine.Random.Range (50, 150);
		}

		baseValue = (int)((float)baseValue * factionBias);
		return baseValue;
	}

	public void RemoveTradeMission(TradeMission _removedTrader)
	{
		tradeMissions.Remove (_removedTrader);
		ResourceManager.current.TradeshipReturn ();
	}

	public void CompleteJourney(faction _f, int type, int _amountSent, int iniResult)
	{
		iniResult = iniResult + UnityEngine.Random.Range (0, 100);

		if (iniResult <= 20)
			DisasterJourney ();
		else if (iniResult <= 70) 
			SuccessfulJourney ();
		else
			AmazingJourney ();

	}
	int SuccessfulJourney()
	{
		return 0;
	}
	int DisasterJourney()
	{
		return 0;
	}
	int AmazingJourney()
	{
		return 0;
	}

}
	
