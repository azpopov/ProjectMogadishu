using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceBuilding : MonoBehaviour {

	float timeSinceTick = 0.0f;

	int storedResources = 0;

	SpriteRenderer spriteRnd;
	Sprite defaultSprite;
	public Sprite glowSprite;
	TextMesh floatText;
	float floatTextLive = 0.0f; 
	// Use this for initialization
	void Start () {
		spriteRnd = GetComponent<SpriteRenderer> ();
		floatText = GetComponentInChildren<TextMesh> ();
		if (spriteRnd != null)
			defaultSprite = spriteRnd.sprite;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float timeSinceLastUpdate = Time.deltaTime;
		timeSinceTick += timeSinceLastUpdate;
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
		if (floatTextLive > float.Epsilon) 
		{
			floatTextLive -= timeSinceLastUpdate;
			Vector3 newPos = new Vector3(0f, 0.00125f * timeSinceLastUpdate);
			floatText.gameObject.transform.Translate(newPos);
			floatText.color -= new Color(floatText.color.r, floatText.color.g, floatText.color.b, floatText.color.a - 127*timeSinceLastUpdate);
		}
	}

	void OnMouseDown()
	{
		if (floatText != null) 
		{
			floatText.text = storedResources.ToString();
			floatText.color = new Color(floatText.color.r, floatText.color.g, floatText.color.b, 255);
			floatText.transform.Translate(new Vector3(this.gameObject.transform.position.x, 0.5f));
			floatTextLive = 2.0f;

		}
		if(spriteRnd != null)
			spriteRnd.sprite = defaultSprite;
		ResourceManager.current.commodities += storedResources;
		storedResources = 0;
	}


}
