﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmbassyController : BuildingController {


    float influenceBonus_;
    static System.Random rndGen = new System.Random();
    Button changeFactionBtn, receiveEnvoyBtn;
    public float influenceBonus
    {
        get
        {
            return influenceBonus_;
        }
        set
        {
            influenceBonus_ = value;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckProduction();
    }

    void Awake()
    {

        changeFactionBtn = gameObject.GetComponent<EmbassyView>().embassyUI.transform.FindChild("ChangeFactionButton").GetComponent<Button>();
        receiveEnvoyBtn = gameObject.GetComponent<EmbassyView>().embassyUI.transform.FindChild("ReceiveEnvoyButton").GetComponent<Button>();
        changeFactionBtn.onClick.AddListener(() => gameObject.GetComponent<EmbassyView>().SelectFaction());
    }

    protected override void CheckProduction()
    {
        if (gameObject.GetComponent<EmbassyModel>().timeSinceTick >= gameObject.GetComponent<EmbassyModel>().timeToTick)
        {
            //Generate FactionEvent HEre
            //if (rndGen.Next(5) == 4)
            //    spriteRnd.sprite = glowSprite[0];
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(GetComponent<BuildingController>().IgnoreMouseDownSec());
        if (Game.current.model.manager.embassyTut)
        {
            EventSystem.OccurEvent("FirstEmbassyEvent");
            Game.current.model.manager.embassyTut = false;
        }

    }
    protected override void OnMouseDown()
    {
        if (!gameObject.activeSelf && EventSystem.eventPresent != null)
            return;
        app.Notify(GameNotification.ShowEmbassy, gameObject.GetComponent<EmbassyController>(), this);
      

    }



    public override void ProductionTick()
    {
        gameObject.GetComponent<EmbassyModel>().timeSinceTick++;
    }

    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        switch (p_event_path)
        {
            case GameNotification.ShowEmbassy:
                gameObject.GetComponent<EmbassyView>().embassyUI.SetActive(!gameObject.GetComponent<EmbassyView>().embassyUI.activeSelf);
                gameObject.GetComponent<EmbassyView>().cnvGroup.blocksRaycasts 
                    = gameObject.GetComponent<EmbassyView>().embassyUI.activeSelf;
                gameObject.GetComponent<EmbassyView>().cnvGroup.interactable 
                    = gameObject.GetComponent<EmbassyView>().embassyUI.activeSelf;
                break;
        }
    }
}
