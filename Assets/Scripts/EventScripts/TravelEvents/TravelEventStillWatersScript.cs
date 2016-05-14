using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TravelEventStillWatersScript : CustomEvent
{

    ShipyardModel.Ship ship;
    string customText;
    // Use this for initialization
    void Start()
    {
        int rndRandom = Random.Range(1, 4);
        ship = (ShipyardModel.Ship)data[0];
        foreach (Transform child in transform)
            if (child.name == "ContentText")
            {
                customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[faction]", ship.theMission.f.name);
                customText = customText.Replace("[shipname]", ship.name);
                customText = customText.Replace("[turns]", (rndRandom).ToString());
                child.GetComponent<Text>().text = customText;
            }
        ship.theMission.timeToDest += rndRandom;
    }

}
