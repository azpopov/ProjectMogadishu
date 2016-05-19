using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomEvent : GameElement
{
	protected Button endTurnButton, disableButton;
    protected ResourceBundle finalBundle, changeBundle;
    public object[] data;
    public AudioClip openSFX, closeSFX;
	public virtual void Awake()
	{
        openSFX = AudioManager.Instance.openParch;
        closeSFX = AudioManager.Instance.closeParch;
		endTurnButton = GameObject.Find ("EndTurnButton").GetComponentInChildren<Button> ();
		foreach (Transform _child in transform) {
			if(_child.name.Equals("CloseButton"))
			   disableButton = _child.GetComponent<Button>(); 
		}
	}



	public virtual void OnEnable ()
	{
        AudioManager.Instance.RandomizeSfx(openSFX);
		gameObject.transform.SetParent (GameObject.Find("UI").transform, false);
		endTurnButton.interactable = false;
		if (disableButton != null) {
			disableButton.onClick.AddListener(() => gameObject.SetActive(false));
		}
	}

	public virtual void OnDisable(){
        AudioManager.Instance.RandomizeSfx(closeSFX);
		endTurnButton.interactable = true;
		EventSystem.eventPresent = null;
		Destroy (gameObject, 5.0f);
	}
    
	

}

