using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TooltipManager : MonoBehaviour {

	static TooltipManager current;

	Text title;
	Text content;
	Tooltip currentlyDisplaying = null;

	Canvas canvasGame;
	void Awake()
	{
		title = GameObject.Find("TooltipTitle").GetComponent<Text> ();
		content = GameObject.Find ("TooltipContent").GetComponent<Text> ();
		canvasGame = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		current.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		if (current == null)
			current = this;
		else
			Destroy (this);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(currentlyDisplaying == null)
		{
			gameObject.SetActive(false);
		}
		Vector2 posTooltip;
		RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvasGame.transform, Input.mousePosition, canvasGame.worldCamera, out posTooltip);
		transform.position = canvasGame.transform.TransformPoint (posTooltip);
	}
}
