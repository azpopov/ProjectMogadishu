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
		Unassigned,
		Celestial,
		Omani
	}
	;
	public static Dictionary<faction, float> factionBiases = new Dictionary<faction, float> ();

	public Sprite[] insignias;
	float refreshTimer = 5f;
    void Awake() { 
    
    }


	// Use this for initialization
	void Start ()
	{
		if (current == null)
			current = this;
		else
			Destroy (this);
		
		factionBiases.Add (faction.Celestial, 1.5f);
		factionBiases.Add (faction.Omani, 0.8f);
        GameObject shipViewUI;
		shipViewUI = Game.current.uiElements["ShipView"];
        shipViewUI.SetActive(true);
        foreach(Transform child in shipViewUI.transform){
            if (String.Equals(child.name, "TradeWindow"))
                tradeWindow = child.gameObject;
        }
        shipViewUI.SetActive(false);
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
			refreshTimer = 20f + UnityEngine.Random.Range (-10f, +10f);
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
			tradeMissionScript.originalTime = UnityEngine.Random.Range (2, 6);
			resourceType = UnityEngine.Random.Range (1, 3);
			targetType = UnityEngine.Random.Range (2,3);
			break;
		case faction.Omani:
			tradeMissionScript.insignia = insignias [1];
			tradeMissionScript.originalTime = UnityEngine.Random.Range (5, 8);
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
		Game.current.currentShips--;
	}



//	public Image GetFactionInsignia(faction _f)
//	{
//
//	}

	public TradeMission[] GetTradeMissionList()
	{
		return tradeMissions.ToArray ();
	}



}
	
