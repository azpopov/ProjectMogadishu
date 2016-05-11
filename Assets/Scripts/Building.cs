using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
	//Counts up to timeToTick then adds resources to Building
	protected int timeSinceTick = 0;
	public int timeToTick = 1;
	
	//Variables for switching the Sprite between empty -> produced
	internal SpriteRenderer spriteRnd;
	internal Sprite defaultSprite;
	public Sprite[] glowSprite;


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

	}
	float ignoreTime =0.5f;
	internal IEnumerator IgnoreMouseDownSec()
	{

		float timer = 0.0f;
		gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
		//while we still have animation time
		while (timer <= ignoreTime) {
			//add time passed so far
			timer += Time.deltaTime;
			//wait till next frame
			yield return null;
		}
		gameObject.layer = LayerMask.NameToLayer ("Building");
	}

	protected virtual int GlowSprite {
		set;
		get;
	}
    public virtual void ProductionTick(){
    }

    protected virtual void OnMouseDown()
    {
    }

    protected virtual void CheckProduction()
    {
    }

    protected virtual void OnEnable()
    {
		Game.current.model.buildingList.Add (this);
		GameModel.resourcesMain -= GetBuildCost();
    }

    protected virtual void OnDisable(){
    }
    public virtual ResourceBundle GetBuildCost()
    {
        return null;
    }

    


}

