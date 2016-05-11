using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckErrorScript : CustomEvent {

	Button forceTurn;

	public override void Awake(){
		base.Awake ();
		foreach (Transform _child in transform) {
			if(_child.name.Equals("ForceTurnButton"))
			{
				forceTurn = _child.GetComponent<Button>();
				forceTurn.onClick.AddListener(()=> Game.current.controller.manager.NextTurnForce());
				forceTurn.onClick.AddListener(() => gameObject.SetActive(false));
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
