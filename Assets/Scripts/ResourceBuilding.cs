using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceBuilding : MonoBehaviour
{
	//Counts up to timeToTick then adds resources to Building
	float timeSinceTick = 0.0f, timeToTick = 3f;

	//Stores resources to be collected later up to a Maximum
	int storedResources = 0, maxResources = 500;

	//Variables for switching the Sprite between empty -> produced
	SpriteRenderer spriteRnd;
	Sprite defaultSprite;
	public Sprite glowSprite;

	//Prefab to display Gained resources upon click
	public GameObject floatingTextPrefab;
	FloatText floatText;

	// Use this for initialization
	void Start ()
	{
		spriteRnd = GetComponent<SpriteRenderer> ();

		if (spriteRnd != null)
			defaultSprite = spriteRnd.sprite;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeSinceTick += Time.deltaTime;
		//If reached the point for the tick and resources aren't full
		if (timeSinceTick > timeToTick && storedResources < maxResources) {
			spriteRnd.sprite = glowSprite;
			if (Random.Range (0, 10) == 9) {
				storedResources += Random.Range (150, 250);
			} else
				storedResources += Random.Range (50, 150);
			timeSinceTick = 0.0f;
		}

	}

	void OnMouseDown ()
	{
		//If there are any stored resources
		if (storedResources != 0) {
			///Instantiate the floating text
			GameObject instanceText = Instantiate (floatingTextPrefab);

			//Set parent of floating text to this
			instanceText.transform.parent = transform;

			//Set the string for the floating text
			floatText = GetComponentInChildren<FloatText> ();
			floatText.setFloatText (storedResources.ToString ());

			//Enable the script
			floatText.enabled = true;

			//Switch Sprite back to default
			if (spriteRnd != null)
				spriteRnd.sprite = defaultSprite;

			//Add stored resources to player Vault
			ResourceManager.current.commodities += storedResources;
			storedResources = 0;
		}
	}


}
