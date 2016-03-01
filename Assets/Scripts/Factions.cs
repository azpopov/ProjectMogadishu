using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Factions : MonoBehaviour {

	public static Factions current = null;

	public List<TradeMission> tradeMissions = new List<TradeMission>();
	GameObject tradeWindow;

	public GameObject tradePrefab;
	public enum faction{
		Celestial,
		Omani
	};

	public Sprite[] insignias;

	float refreshTimer = 60f;

	// Use this for initialization
	void Start () {
		if (current == null)
			current = this;
		else
			Destroy (this);
		tradeWindow = GameObject.Find ("TradeWindow").gameObject;
		CreateTradeRoute ();
		int counter = 0;






	}


	// Update is called once per frame
	void Update () {
		if (refreshTimer < float.Epsilon) {
			refreshTimer = 60f + UnityEngine.Random.Range(-10f,+10f);

		}


	}
	GameObject CreateTradeRoute()
	{
		faction rndFaction = GetRandomEnum<faction> ();

		GameObject instance = (GameObject)Instantiate (tradePrefab, new Vector3(0,0), Quaternion.identity);
		instance.transform.SetParent (tradeWindow.transform, false);
		TradeMission tradeMissionScript = instance.GetComponent<TradeMission>();
		switch (rndFaction) {
		case faction.Celestial:
			tradeMissionScript.insignia = insignias[0];
			tradeMissionScript.timeToDest = UnityEngine.Random.Range(100f,250f);
			break;
		case faction.Omani:
			tradeMissionScript.insignia = insignias[1];
			break;
		default: break;
		}

		tradeMissions.Add(tradeMissionScript);
		return instance;
	}

	static T GetRandomEnum<T>()
	{
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
		return V;
	}
}
