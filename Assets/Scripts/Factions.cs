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
	Dictionary<faction, float> factionBiases = new Dictionary<faction, float> ();

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

	public void CompleteJourney(faction _f, int _type, int _targetType, int _amountSent, int iniResult)
	{
		iniResult = iniResult + UnityEngine.Random.Range (0, 100);

		if (iniResult <= 20)
			DisasterJourney ( factionBiases[_f],  _type, _targetType,  _amountSent);
		else if (iniResult <= 80) 
			ResourceManager.current.addToResource(_targetType, SuccessfulJourney (factionBiases[_f],  _type, _targetType, _amountSent));
		else
			AmazingJourney (factionBiases[_f], _type, _targetType, _amountSent);

	}
	int SuccessfulJourney(float _factionBias, int _type, int _targetType, int _amountSent)
	{
		float amountReturned = (float)_amountSent;
		amountReturned *= _factionBias;

		if (_type == 0 && _targetType == 1) {
			amountReturned /= 2f;
		} else if (_type == 0 && _targetType == 2) {
			amountReturned /= 6f;
		} else if (_type == 1 && _targetType == 2) {
			amountReturned *= 0.6f;
		} else if (_type == 1 && _targetType == 0) {
			amountReturned *= 2.2f;
		} else if (_type == _targetType)
			amountReturned *= 1.7f;

		return (int)amountReturned;
	}
	int DisasterJourney(float _factionBias, int _type, int _targetType, int _amountSent)
	{
		Debug.Log ("disaster not implemented");
		return 0;
	}
	int AmazingJourney(float _factionBias, int _type,  int _targetType,int _amountSent)
	{
		Debug.Log ("Amazing not implemented");
		return 0;
	}

}
	
