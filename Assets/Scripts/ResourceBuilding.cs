using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceBuilding : MonoBehaviour {

	float timeSinceTick = 0.0f;
	int storedResources = 0;
	SpriteRenderer spriteRnd;
	Sprite defaultSprite;
	public Sprite glowSprite;

	public GameObject floatingTextPrefab;
	FloatText floatText;
	// Use this for initialization
	void Start () {
		spriteRnd = GetComponent<SpriteRenderer> ();

		if (spriteRnd != null)
			defaultSprite = spriteRnd.sprite;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeSinceTick += Time.deltaTime;
		if (timeSinceTick > 3) 
		{
			spriteRnd.sprite = glowSprite;
			if(Random.Range(0,10) == 9)
			{
				storedResources += Random.Range(150,250);
			}
			else
				storedResources += Random.Range(50,150);
			timeSinceTick = 0.0f;
		}

	}

	void OnMouseDown()
	{
		if (storedResources != 0) {
			GameObject instanceText = Instantiate(floatingTextPrefab);
			instanceText.transform.parent = transform;
			floatText = GetComponentInChildren<FloatText> ();
			floatText.setFloatText(storedResources.ToString());
			floatText.enabled = true;
			if (spriteRnd != null)
				spriteRnd.sprite = defaultSprite;
			ResourceManager.current.commodities += storedResources;
			storedResources = 0;
		}
	}


}
