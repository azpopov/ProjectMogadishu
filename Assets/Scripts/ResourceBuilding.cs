using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ResourceBuilding : Building
{
	//Stores resources to be collected later up to a Maximum
	public int storedResources = 0, maxResources = 500;

	//Prefab to display Gained resources upon click
	public GameObject floatingTextPrefab;
	FloatText floatText;

	public bool resourcesMaxed = false;
	public int type;


	// Use this for initialization
	void Start ()
	{
		timeSinceTick = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckProduction ();
	}
	protected override void OnEnable()
	{
		Game.current.buildingList.Add (this);
        Game.resourcesMain -= GetBuildCost();
	
    }

	protected override void OnDisable()
	{
		Game.current.buildingList.Remove (this);
	}
	
	protected override void OnMouseDown ()
	{
        Debug.Log("called");
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

			//Reset checking boolean to false
			resourcesMaxed = false;

		}
	}
	void addResource()
	{
		if (type == 0) {
			Game.current.addToResource(0,storedResources);
		
		} else if (type == 1) {
            Game.current.addToResource(1, storedResources);

		}
		storedResources = 0;
	}



	public override void ProductionTick()
	{
		timeSinceTick++;
	}

	protected override void CheckProduction()
	{
		if (timeSinceTick < timeToTick) 
			return;
		timeSinceTick = 0;
		Produce ();
	}


	void Produce()
	{
		if (storedResources > maxResources) {
			resourcesMaxed = true;
			return;
			
		}
		if (type == 0) {
			storedResources += Random.Range(50,200);
			
		} else if (type == 1) {
			storedResources += Random.Range(12,50);
			
		}
		spriteRnd.sprite = glowSprite [0];

	}


	protected override int GlowSprite
	{
		set{
			spriteRnd.sprite = glowSprite[value];
			return;
		}
		get{
			int _index = 0;
			foreach(Sprite spr in glowSprite){
				if(spr == spriteRnd.sprite)
				{
					return _index;
				}
				_index++;
			}
			return 0;
		}
	}


    public override ResourceBundle GetBuildCost()
    {
        return new ResourceBundle(400, 0, 0); //Farm
       // buildCosts.Add(new ResourceBundle(1000, 100, 0)); //Hunters Lodge
    }


}
