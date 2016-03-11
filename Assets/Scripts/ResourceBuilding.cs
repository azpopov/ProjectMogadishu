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

	public enum resourceType
	{
		commodity,
		luxury
	}
	public resourceType type;

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
		if (timeSinceTick > timeToTick) {
			timeSinceTick = 0.0f;
			if (storedResources < maxResources) {
				spriteRnd.sprite = glowSprite;
				produce();
			}
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
			addResource();
		}
	}
	void addResource()
	{
		if (type == resourceType.commodity) {
			ResourceManager.current.commodities += storedResources;
		
		} else if (type == resourceType.luxury) {
			ResourceManager.current.luxuries += storedResources;

		}
		storedResources = 0;
	}

	void produce()
	{
		int random = Random.Range (0, 10);
		if (type == resourceType.commodity) {
			storedResources += Random.Range(50,200);
			
		} else if (type == resourceType.luxury) {
			storedResources += Random.Range(12,50);
			
		}
	}

}
