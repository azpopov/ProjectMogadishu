using UnityEngine;
using System.Collections;

public class ShipYard : MonoBehaviour {

	//Variables for switching the Sprite between empty -> produced
	SpriteRenderer spriteRnd;
	Sprite defaultSprite;
	public Sprite glowSprite;

	// Use this for initialization
	void Start () {
		spriteRnd = GetComponent<SpriteRenderer> ();
		
		if (spriteRnd != null)
			defaultSprite = spriteRnd.sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
