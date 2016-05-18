using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VascoAngerStealScript : CustomEvent
{

    Button choiceAgreeButton;
    // Use this for initialization
    void Start()
    {
        disableButton.onClick.AddListener(() => LostResources());
    }

    void LostResources()
    {
        int type = UnityEngine.Random.Range(0,2);
        ResourceBundle lostBundle = new ResourceBundle(0,0,0);
        switch (type)
        {
            case 0:
                lostBundle = new ResourceBundle(type, (int)((float)ManagerModel.resourcesMain.commodity * 0.3));
                break;
            case 1:
                lostBundle = new ResourceBundle(type, (int)((float)ManagerModel.resourcesMain.luxury * 0.3));
                break;
        }
        app.model.manager.addToResource(type, -lostBundle.ReturnMax());
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle(type, -lostBundle.ReturnMax()), false);
        gameObject.SetActive(false);
    }

}
