using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class NewShipNaming : CustomEvent
{
	Shipyard senderShipyard;
	InputField inputField;
	public override void Awake()
	{
		base.Awake ();
		foreach (Transform _child in transform) {
			if(_child.name.Equals("InputField"))
				inputField = _child.GetComponent<InputField>();
		}
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void OnEnable()
	{
		base.OnEnable();
		senderShipyard = Game.current.buildingList[Game.current.buildingList.Count-1] as Shipyard;
		base.disableButton.onClick.AddListener (() => AddToShipyard ());
	}

	void AddToShipyard()
	{
		string newShip = inputField.text;
		senderShipyard.CreateShip (newShip);
	}
}

