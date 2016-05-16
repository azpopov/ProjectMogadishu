using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TravelEventMerchantScript : CustomEvent {

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

        float rndModifier = Random.Range(0.2f, 3f);
        int rndType = Random.Range(0, 3);

        changeBundle = new ResourceBundle(rndType, (int)(ship.theMission.targetResource * rndModifier));


        foreach (Transform child in transform)
            if (child.name == "ContentText")
            {
                customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[faction]", ship.theMission.f.name);
                customText = customText.Replace("[shipname]", ship.name);
                customText = customText.Replace("[amount]", changeBundle.ReturnMax().ToString());
                customText = customText.Replace("[type]", changeBundle.ReturnStringofMax());
                customText = customText.Replace("[amountcargo]", ship.theMission.requestResource.ReturnMax().ToString());
                customText = customText.Replace("[typecargo]", ship.theMission.requestResource.ReturnStringofMax());
                child.GetComponent<Text>().text = customText;
            }
    }

    void ChoiceGuarantee()
    {
        
    }

    void ChoiceRisk()
    {
        app.Notify(GameNotification.AddResources, app.controller.manager, changeBundle, true);
        ship.theMission.requestResource *= 0.5f;
    }
}
