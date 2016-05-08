using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
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

	protected abstract int GlowSprite {
		set;
		get;
	}
	public abstract void ProductionTick();

	protected abstract void OnMouseDown();

	protected abstract void CheckProduction ();

	protected abstract void OnEnable();

	protected abstract void OnDisable();

	public abstract ResourceBundle GetBuildCost();
}

