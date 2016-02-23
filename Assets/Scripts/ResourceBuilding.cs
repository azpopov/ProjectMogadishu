using UnityEngine;
using System.Collections;

public class ResourceBuilding : MonoBehaviour {

	float timeSinceTick = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceTick += Time.deltaTime;
		if (timeSinceTick > 3) 
		{
			if(Random.Range(0,10) == 9)
			{
				Debug.Log("Huge Success!!");
				ResourceManager.current.commodities += Random.Range(150,250);
			}
			else
				ResourceManager.current.commodities += Random.Range(50,150);
			timeSinceTick = 0.0f;
		}
	}
}
