using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TravelEventDiseaseScript : CustomEvent
{

    ShipyardModel.Ship ship;
    string customText;

    Button choiceGuarantee, choiceRisk;
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals("ChoiceGuarantee"))
                choiceGuarantee = child.GetComponent<Button>();
            if (child.name.Equals("ChoiceRisk"))
                choiceRisk = child.GetComponent<Button>();

        }
        choiceGuarantee.onClick.AddListener(() => gameObject.SetActive(false));
        choiceRisk.onClick.AddListener(() => gameObject.SetActive(false));
        choiceGuarantee.onClick.AddListener(() => ChoiceGuarantee());
        choiceRisk.onClick.AddListener(() => ChoiceRisk());
        ship = (ShipyardModel.Ship)data[0];
        foreach (Transform child in transform)
            if (child.name == "ContentText")
            {
                customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[randomfaction]", Factions.current.factionList[Random.Range(0, Factions.current.factionList.Count)].name);
                customText = customText.Replace("[shipname]", ship.name);
                child.GetComponent<Text>().text = customText;
            }
    }

    void ChoiceGuarantee()
    {
        app.model.manager.addToResource(0, -200);
        app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle("Commodity", -200), false);
    }

    void ChoiceRisk()
    {
        int riskRandom = Random.Range(0, 4);
        if (riskRandom == 3)
        {
            app.model.manager.addToResource(0, -1000);
            app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle("Commodity", -1000), false);
        }
        else
            app.Notify(GameNotification.ResultResourceChange, app.controller.manager, new ResourceBundle(0,0,0), true);
    }

}