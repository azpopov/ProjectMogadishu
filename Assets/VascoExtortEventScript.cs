using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VascoExtortEventScript : CustomEvent
{

    Button choiceWealthButton, choiceCommoditiesButton;
	// Use this for initialization
	void Start () {
        foreach (Transform _child in transform)
        {
            if (_child.name.Equals("choiceAgree"))
                choiceWealthButton = _child.GetComponent<Button>();
        }
        choiceWealthButton.onClick.AddListener(() => ChoiceAgree());
        disableButton.onClick.AddListener(() => VascoAnger(true));
	}

    void ChoiceAgree()
    {
        if(!ManagerModel.resourcesMain.CompareBundle(new ResourceBundle(2,100))) return;
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle(2,-100), false);
        app.model.manager.addBundle(new ResourceBundle(2,-100));
        VascoAnger(false);
        gameObject.SetActive(false);
    }

    void VascoAnger(bool angered)
    {
        if (angered)
            app.controller.story.vascoAnger++;
        else
            app.controller.story.vascoAnger--;
    }

}
