using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VascoAngerRepelledScript : CustomEvent
{

    // Use this for initialization
    void Start()
    {
        disableButton.onClick.AddListener(() => GainedResources());
    }

    void GainedResources()
    {
        ResourceBundle gainedBundle = new ResourceBundle(0, 0, 50);
        app.model.manager.addBundle(gainedBundle);
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle(2, gainedBundle.ReturnMax()), true);
        gameObject.SetActive(false);
    }

}
