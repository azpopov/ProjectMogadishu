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
	static System.Random rndGen = new System.Random();
	public List<Faction> factionList = new List<Faction>();
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
		factionList.Add (new Faction ("Celestial Empire", 1.5f, new bool[]{false, true, true}, 4, 7));
		factionList.Add (new Faction ("Oman", 0.6f, new bool[]{true, true, true}, 1, 5));
//		factionBiases.Add (faction.Celestial, 1.5f);
//		factionBiases.Add (faction.Omani, 0.8f);
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
		int rndFaction = rndGen.Next(factionList.Count);
		GameObject instance = (GameObject)Instantiate (tradePrefab, new Vector3 (0, 0), Quaternion.identity);
		instance.transform.SetParent (tradeWindow.transform, false);
		TradeMission tradeMissionScript = instance.GetComponent<TradeMission> ();
		tradeMissionScript.insignia = factionList[rndFaction].insignia;
		tradeMissionScript.originalTime = UnityEngine.Random.Range (factionList[rndFaction].minDistance, 
		                                                            factionList[rndFaction].maxDistance);
		tradeMissionScript.type = GetRandomTrueResourceType(factionList[rndFaction]);
		tradeMissionScript.targetType = GetRandomTrueResourceType(factionList[rndFaction]);
		tradeMissionScript.f = factionList[rndFaction]; //!!!
		tradeMissionScript.requestedResources = GetResourceAmount(factionList[rndFaction].tradeBias, tradeMissionScript.type);


		tradeMissions.Add (tradeMissionScript);
		return instance;
	}

	int GetRandomTrueResourceType(Faction _f)
	{
		int rndType;
		while(true)
		{
			rndType = rndGen.Next(_f.tradeResourceTypes.Length);
			if(_f.tradeResourceTypes[rndType])
				break;
		}
		return rndType;
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
	
