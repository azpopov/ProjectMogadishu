using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmbassyController : BuildingController {
    Button changeFactionBtn;

    void Start()
    {
        changeFactionBtn = gameObject.GetComponent<EmbassyView>().embassyUI.transform.FindChild("ChangeFactionButton").GetComponent<Button>();
        changeFactionBtn.onClick.AddListener(() => gameObject.GetComponent<EmbassyView>().SelectFaction());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(GetComponent<BuildingController>().IgnoreMouseDownSec());
        if (app.model.manager.embassyTut)
        {
            EventSystem.OccurEvent("TutorialEmbassy");
            app.model.manager.embassyTut = false;
        }

    }
    protected override void OnMouseDown()
    {
        if (!gameObject.activeSelf && EventSystem.eventPresent != null)
            return;
        app.Notify(GameNotification.EmbassyShow, gameObject.GetComponent<EmbassyController>(), this);
      

    }



    public override void ProductionTick()
    {
        if (GetComponent<EmbassyModel>().timeSinceTick == 3)
        {
            if (GetComponent<EmbassyModel>().f.minDistance != 0)
                (EmbassyModel.influenceBonuses[GetComponent<EmbassyModel>().f.name]) = (EmbassyModel.influenceBonuses[GetComponent<EmbassyModel>().f.name]) + 1;
            GetComponent<EmbassyModel>().timeSinceTick = 0;
            return;
        }
        GetComponent<EmbassyModel>().timeSinceTick++;
    }

    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        if (p_target != this) return;
        switch (p_event_path)
        {
            case GameNotification.EmbassyShow:
                gameObject.GetComponent<EmbassyView>().embassyUI.SetActive(!gameObject.GetComponent<EmbassyView>().embassyUI.activeSelf);
                gameObject.GetComponent<EmbassyView>().cnvGroup.blocksRaycasts 
                    = gameObject.GetComponent<EmbassyView>().embassyUI.activeSelf;
                gameObject.GetComponent<EmbassyView>().cnvGroup.interactable 
                    = gameObject.GetComponent<EmbassyView>().embassyUI.activeSelf;
                return;
            case GameNotification.EmbassyFactionChange:
                Faction newFaction = (Faction)p_data[1];
                EmbassyView theView = (EmbassyView)p_data[0];
                EmbassyModel theModel = GetComponent<EmbassyModel>();
                theModel.SetFaction(newFaction);
                theView.UpdateUI();
                return;
              
        }
    }
}
