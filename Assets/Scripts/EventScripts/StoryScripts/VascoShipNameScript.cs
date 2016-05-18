using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VascoShipNameScript : CustomEvent
{
    Text contentText;
    Button choiceAgreeButton, choiceCommoditiesButton;
    ShipyardModel.Ship _ship;
    // Use this for initialization
    void Start()
    {
        _ship = new ShipyardModel.Ship("HMS Victory");
         foreach (BuildingController building in app.controller.buildings)
        {
            if (building is ShipyardController)
            {
                _ship = building.GetComponent<ShipyardModel>().shipsInShipyard[0];
                
                break;
            }
        }
        foreach (Transform _child in transform)
        {
            if (_child.name.Equals("choiceAgree"))
            {
                choiceAgreeButton = _child.GetComponent<Button>();
            }
            if (_child.name.Equals("ContentText"))
                _child.GetComponent<Text>().text = _child.GetComponent<Text>().text.Replace("[shipname]", _ship.name);

        }
        choiceAgreeButton.onClick.AddListener(() => ChoiceAgree());
        disableButton.onClick.AddListener(() => VascoAnger(true));
        
       
    }

    void ChoiceAgree()
    {

        _ship.name = "Sao Gamma";
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
