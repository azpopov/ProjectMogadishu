using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VascoStealsTradeEventScript : CustomEvent
{
    Text contentText;
    Button choiceAgreeButton, choiceCommoditiesButton;
    ShipyardModel.Ship _ship;
    // Use this for initialization
    void Start()
    {
        foreach (Transform _child in transform)
        {
            if (_child.name.Equals("choiceAgree"))
            {
                choiceAgreeButton = _child.GetComponent<Button>();
            }

        }
        choiceAgreeButton.onClick.AddListener(() => ChoiceAgree());
        disableButton.onClick.AddListener(() => VascoAnger(true));


    }

    void ChoiceAgree()
    {
        List<TradeMission> newList = new List<TradeMission>();
        foreach (TradeMission _mission in Factions.current.tradeMissions)
        {
            if (!_mission.sailing)
            {
                newList.Add(_mission);
            }
        }
        foreach (TradeMission _mission in newList)
        {
            Factions.current.RemoveTradeMission(_mission);
        }
        
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
