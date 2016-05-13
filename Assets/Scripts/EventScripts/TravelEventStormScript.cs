using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TravelEventStormScript : CustomEvent {

    ShipyardModel.Ship ship;
    string customText;
	// Use this for initialization
	void Start () {
        int rndRandom = Random.Range(2, 7);
        ship = (ShipyardModel.Ship)data[0];
        foreach(Transform child in transform)
            if (child.name == "ContentText")
            {
                customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[faction]", ship.theMission.f.name);
                customText = customText.Replace("[shipname]", ship.name);
                customText = customText.Replace("[percent]", (rndRandom * 10).ToString());
                child.GetComponent<Text>().text = customText;
            }
        ship.theMission.targetResource *= (1 - (((float)rndRandom) / 10));
	}

}
