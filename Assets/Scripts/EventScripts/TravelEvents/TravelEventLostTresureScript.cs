using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TravelEventLostTresureScript : CustomEvent {
    ShipyardModel.Ship ship;
    int rndType;
	// Use this for initialization
	void Start () {
        rndType = Random.Range(0, 3);
        string customText;
        ship = (ShipyardModel.Ship)data[0];
        foreach (Transform child in transform)
            if (child.name == "ContentText")
            {
                customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[faction]", ship.theMission.f.name);
                customText = customText.Replace("[type]", ResourceBundle.TypePluralToString(rndType));
                child.GetComponent<Text>().text = customText;
            }
        disableButton.onClick.AddListener(() => AddTresure());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void AddTresure()
    {
        int rndAmount = Random.Range(0, 500);
        changeBundle = new ResourceBundle(rndType, rndAmount);
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, changeBundle, true);
        app.model.manager.addToResource(rndType, rndAmount);
    }
}
