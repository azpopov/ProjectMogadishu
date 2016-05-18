using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VascoShowsUpScipt : CustomEvent {

    Button choiceWealthButton, choiceCommoditiesButton;
	// Use this for initialization
	void Start () {
        foreach (Transform _child in transform)
        {
            if (_child.name.Equals("choiceWealth"))
                choiceWealthButton = _child.GetComponent<Button>();
            else if(_child.name.Equals("choiceCommodities"))
                choiceCommoditiesButton = _child.GetComponent<Button>();
        }
        choiceWealthButton.onClick.AddListener(() => ChoiceWealth());
        choiceCommoditiesButton.onClick.AddListener(() => ChoiceCommodities());
        disableButton.onClick.AddListener(() => VascoAnger(true));
	}

    void ChoiceWealth()
    {
        app.model.manager.addBundle(new ResourceBundle(2, -200));
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle(2, -200), false);
        VascoAnger(false);
        gameObject.SetActive(false);
    }

    void ChoiceCommodities()
    {
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle(0, ManagerModel.resourcesMain.commodity), false);
        app.model.manager.addBundle(new ResourceBundle(0, -ManagerModel.resourcesMain.commodity));
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
