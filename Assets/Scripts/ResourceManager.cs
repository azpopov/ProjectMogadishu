using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager current;

	public Sprite[] resourceSprites;


	Text commoditiesText, luxuriesText, wealthText, maxShipText;


	int _maxShips = 2;
	int currentShips = 0;

	int _commodities = 500;
	int _luxuries = 500;
	int _wealth = 500;

	public List<string> ownedShips = new List<string>();
	public int commodities {
		get {return _commodities;}
		set{
			_commodities = value ;
			commoditiesText.text =  ":"+_commodities.ToString();
			}
	}
	public int luxuries
	{
		get {return _luxuries;}
		set{ _luxuries = value;
			luxuriesText.text =  ":"+_luxuries.ToString();}
	}
	public int wealth
	{
		get {return _wealth;}
		set{ _wealth = value ;
			wealthText.text = ":"+_wealth.ToString();}
	}

	public int maxShips
	{
		get
		{
			return _maxShips;
		}
		set
		{
			_maxShips = value;
			maxShipText.text = currentShips.ToString() + " / "+_maxShips.ToString();
		}
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
		maxShipText = GameObject.Find ("MaxShips").GetComponentInChildren<Text> ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if (maxShipText != null)
			maxShipText.text = currentShips.ToString () + " / " + _maxShips.ToString ();
	}

	public void addToResource(int _type, int _amount)
	{
		if (_type == 0) {
			commodities += _amount;
		} else if (_type == 1) {
			luxuries += _amount;
		} else
			wealth += _amount;
	}

	public bool ShipAvailable()
	{
		if (currentShips >= maxShips) {
			return false;
		}
		return true;
	}

	public void TradeshipReturn()
	{
		currentShips--;
	}

	public void SendTradeship()
	{
		currentShips++;
	}

	public void AddNewShipName()
	{


			ownedShips.Add(GameObject.Find("InputField").GetComponent<InputField>().text);
			Debug.Log(GameObject.Find("InputField").GetComponent<InputField>().text);

	}

}
