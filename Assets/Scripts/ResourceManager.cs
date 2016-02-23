using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager current;
	Text commoditiesText, luxuriesText, wealthText;
	int _commodities = 0;
	int _luxuries = 0;
	int _wealth = 0;
	public int commodities {
		get {return _commodities;}
		set{ _commodities = value ;
			commoditiesText.text = _commodities.ToString();
			Debug.Log(value);}
	}
	public int luxuries
	{
		get {return _luxuries;}
		set{ _luxuries = value;
			luxuriesText.text = _luxuries.ToString();}
	}
	public int wealth
	{
		get {return _wealth;}
		set{ _wealth = value ;
			wealthText.text = _wealth.ToString();}
	}



	// Use this for initialization
	void Start () {
		if (current == null)
			current = this;
		else
			Destroy (this);
		commoditiesText = GameObject.Find ("Commodities").GetComponentInChildren<Text> ();
		luxuriesText = GameObject.Find ("Luxuries").GetComponentInChildren<Text> ();
		wealthText = GameObject.Find ("Wealth").GetComponentInChildren<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addToCommodities(int additive)
	{
		this.commodities += additive;
	}
}
