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
    private List<Faction> factionListUndiscovered = new List<Faction>();

    static int defaultNewMissionTime = 5;
    public static int timeToNewMission;

    Text newMissionTime;
    void Awake() {
        var _object = GameObject.Find("UI").GetComponentsInChildren(typeof(Transform), true);
        foreach (Component lookUp in _object)
        {
            if (lookUp.gameObject.name.Equals("TradeWindow"))
            {
                tradeWindow = lookUp.gameObject;
                var shipView = tradeWindow.transform.parent.gameObject;
                GameObject.Find("MaxShips").GetComponent<Button>().onClick.AddListener(() => shipView.SetActive(!shipView.activeSelf));
                foreach (Transform child in lookUp.transform)
                    if (child.name.Equals("NewMissionTime"))
                        newMissionTime = child.GetComponent<Text>();
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
		factionList.Add (new Faction ("The Celestial Empire", 2f, new bool[]{true, true, true}, new bool[]{false, true, true}, 4, 10));
        factionList.Add(new Faction("Oman", 1f, new bool[] { true, false, false }, new bool[] { true, true, false }, 1, 5));
        factionList.Add(new Faction("Bengal", 1.2f, new bool[] { true, false, false }, new bool[] { true, true, true }, 3, 6));
        factionList.Add(new Faction("Ceylon", 1f, new bool[] { true, false, false }, new bool[] { true, true, false }, 2, 5));
        factionList.Add(new Faction("Chola", 1.1f, new bool[] { false, true, false }, new bool[] { true, true, false }, 3, 5));
        factionList.Add(new Faction("Khmer", 1.6f, new bool[] { true, false, false }, new bool[] { true, true, true }, 4, 8));
        factionList.Add(new Faction("Seljuk Empire", 1f, new bool[] { false, true, false }, new bool[] { false, true, false }, 2, 6));
        factionList.Add(new Faction("Srivijaya Empire", 1.5f, new bool[] { false, false, true }, new bool[] { true, true, true }, 4, 8));
        factionListUndiscovered.Add(new Faction("Champa", 1.6f, new bool[] { false, true, false }, new bool[] { true, true, true }, 4, 9));
        factionListUndiscovered.Add(new Faction("Gujarat", 1.3f, new bool[] { true, true, false }, new bool[] { true, true, true }, 2, 5));
		for (int i = 0; i < 5; i++) 
		{
			CreateTradeRoute ();
		}
        timeToNewMission = defaultNewMissionTime;

	}


	// Update is called once per frame
	void FixedUpdate ()
	{
        if (timeToNewMission <= 0)
            CreateTradeRoute();
        newMissionTime.text = timeToNewMission.ToString();

	}


	public GameObject CreateTradeRoute ()
	{
        if (tradeMissions.Count > 6)
            return null;
        
		int rndFaction = rndGen.Next(factionList.Count);
		GameObject instance = (GameObject)Instantiate (tradePrefab, new Vector3 (0, 0), Quaternion.identity);
		instance.transform.SetParent (tradeWindow.transform, false);
		TradeMission tradeMissionScript = instance.GetComponent<TradeMission> ();
		tradeMissionScript.insignia = factionList[rndFaction].insignia;
		tradeMissionScript.originalTime = UnityEngine.Random.Range (factionList[rndFaction].minDistance, 
		                                                            factionList[rndFaction].maxDistance);
        tradeMissionScript.requestResource = GetRandomTrueResourceRequest(factionList[rndFaction]);
        tradeMissionScript.targetResource = getRandomTrueResource(factionList[rndFaction]);
		tradeMissionScript.f = factionList[rndFaction]; 
		tradeMissions.Add (tradeMissionScript);
        timeToNewMission = defaultNewMissionTime;
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
                baseValue = UnityEngine.Random.Range(200, 800);
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
    int getRandomTrueResource(Faction _f)
    {
        int rndType;
        while (true)
        {
            rndType = rndGen.Next(_f.tradeForTypes.Length);
            if (_f.tradeForTypes[rndType])
                return rndType;
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
        Destroy(_removedTrader.gameObject);
		tradeMissions.Remove (_removedTrader);
	}

	public TradeMission[] GetTradeMissionList()
	{
		return tradeMissions.ToArray ();
	}
    public Faction DiscoverNewEmpire()
    {
        if (factionListUndiscovered.Count == 0) return new Faction();
        int rndFactionIndex = UnityEngine.Random.Range(0, factionListUndiscovered.Count);
        factionList.Add(factionListUndiscovered[rndFactionIndex]);
        factionListUndiscovered.RemoveAt(rndFactionIndex);
        return factionList[factionList.Count-1];
    }


}
	
