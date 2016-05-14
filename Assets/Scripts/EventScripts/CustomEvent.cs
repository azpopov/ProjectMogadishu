using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomEvent : GameElement
{
	protected Button endTurnButton, disableButton;
    protected ResourceBundle finalBundle, changeBundle;
    public object[] data;
	public virtual void Awake()
	{
		endTurnButton = GameObject.Find ("EndTurnButton").GetComponentInChildren<Button> ();
		foreach (Transform _child in transform) {
			if(_child.name.Equals("CloseButton"))
			   disableButton = _child.GetComponent<Button>(); 
		}
	}



	public virtual void OnEnable ()
	{
		gameObject.transform.SetParent (GameObject.Find("UI").transform, false);
		endTurnButton.interactable = false;
		if (disableButton != null) {
			disableButton.onClick.AddListener(() => gameObject.SetActive(false));
		}
	}

	public virtual void OnDisable(){
		endTurnButton.interactable = true;
		EventSystem.eventPresent = null;
		Destroy (gameObject, 5.0f);
	}
	

}

