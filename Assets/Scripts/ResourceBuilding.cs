using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ResourceBuilding : MonoBehaviour
{
	//Counts up to timeToTick then adds resources to Building
	int timeSinceTick = 0;
	public int timeToTick = 1;

	//Stores resources to be collected later up to a Maximum
	int storedResources = 0, maxResources = 500;

	//Variables for switching the Sprite between empty -> produced
	SpriteRenderer spriteRnd;
	Sprite defaultSprite;
	public Sprite glowSprite;

	//Prefab to display Gained resources upon click
	public GameObject floatingTextPrefab;
	FloatText floatText;

	public bool resourcesMaxed = false;
	public enum resourceType
	{
		commodity,
		luxury
	}
	public resourceType type;

	void Awake()
	{
		spriteRnd = GetComponent<SpriteRenderer> ();
		
		if (spriteRnd != null)
			defaultSprite = spriteRnd.sprite;
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		checkProduction ();
	}
	void OnEnable()
	{
		Game.current.resourceBuildingList.Add (this);
	}

	void OnDisable()
	{
		Game.current.resourceBuildingList.Remove (this);
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

			resourcesMaxed = false;
		}
	}
	void addResource()
	{
		if (type == resourceType.commodity) {
			Game.current.commodities += storedResources;
		
		} else if (type == resourceType.luxury) {
			Game.current.luxuries += storedResources;

		}
		storedResources = 0;
	}



	public void productionTick(int n)
	{
		timeSinceTick += n;
	}

	void checkProduction()
	{
		if (timeSinceTick != timeToTick) 
			return;
		Produce ();
		timeSinceTick = 0;
	}


	void Produce()
	{
		if (storedResources > maxResources) {
			resourcesMaxed = true;
			return;
			
		}
		if (type == resourceType.commodity) {
			storedResources += Random.Range(50,200);
			
		} else if (type == resourceType.luxury) {
			storedResources += Random.Range(12,50);
			
		}
		spriteRnd.sprite = glowSprite;

	}

}
