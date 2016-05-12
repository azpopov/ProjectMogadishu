using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Factions : GameElement
{

	public static Factions current = null;
	public List<TradeMission> tradeMissions = new List<TradeMission> ();
	GameObject tradeWindow;
	public GameObject tradePrefab;
	static System.Random rndGen = new System.Random();
	public List<Faction> factionList = new List<Faction>();
	float refreshTimer = 5f;
    void Awake() {
        var _object = GameObject.Find("UI").GetComponentsInChildren(typeof(Transform), true);
        foreach (Component lookUp in _object)
        {
            if (lookUp.gameObject.name.Equals("TradeWindow"))
            {
                tradeWindow = lookUp.gameObject;
                var shipView = tradeWindow.transform.parent.gameObject;
                GameObject.Find("MaxShips").GetComponent<Button>().onClick.AddListener(() => shipView.SetActive(!shipView.activeSelf));
                return;
            }

        }
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
        tradeMissionScript.requestResource = GetRandomTrueResourceRequest(factionList[rndFaction]);
        tradeMissionScript.targetResource = GetRandomTrueResourceRequest(factionList[rndFaction]);
		tradeMissionScript.f = factionList[rndFaction]; //!!!
		tradeMissions.Add (tradeMissionScript);
		return instance;
	}
	ResourceBundle GetRandomTrueResourceRequest(Faction _f)
	{
        int rndType;
        while (true)
        {
            rndType = rndGen.Next(_f.tradeResourceTypes.Length);
            if (_f.tradeResourceTypes[rndType])
                break;
        }
        int baseValue = 0;

        switch (rndType)
        { 
            case 0:
                baseValue = UnityEngine.Random.Range(300, 600);
                baseValue = (int)((float)baseValue * _f.tradeBias);
                return new ResourceBundle("Commodity", baseValue);
            case 1:
                baseValue = UnityEngine.Random.Range(100, 300);
                baseValue = (int)((float)baseValue * _f.tradeBias);
                return new ResourceBundle("Luxury", baseValue);
            case 2:
                baseValue = UnityEngine.Random.Range(100, 200);
                baseValue = (int)((float)baseValue * _f.tradeBias);
                return new ResourceBundle("Wealth", baseValue);
            default:
                return null;
        }
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
		app.model.manager.currentShips--;
	}

	public TradeMission[] GetTradeMissionList()
	{
		return tradeMissions.ToArray ();
	}



}
	
