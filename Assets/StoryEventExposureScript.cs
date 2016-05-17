using UnityEngine;
using System.Collections;

public class StoryEventExposureScript : CustomEvent {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnDisable()
    {
        base.OnDisable();
        app.Notify(GameNotification.StoryEventDebt, app.controller.story, "StoryEventIntroDebt");
    }
}
