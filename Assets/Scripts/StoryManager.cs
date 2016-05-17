using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryManager : GameElement {
    public GameObject storyPanelPrefab;
    GameObject storyPanelInstance;
    protected Button endTurnButton, disableButton;
   public int remainingDebt, maxDebt;
    int _interestCounter;
    public int interestCounter
    {
        set
        {
            _interestCounter = value;
            if (_interestCounter <= 0)
            {
                remainingDebt = (int)((float)remainingDebt * 1.1f);
                app.Notify(GameNotification.StoryEventInterest, this, this);
                if (remainingDebt >= maxDebt) app.Notify(GameNotification.GameOver, app.controller.manager, "debt" ,remainingDebt);
                _interestCounter = 10;
            }
        }
        get {
           return _interestCounter;
        }
    }
    Text debtText, interestTurnText;
    List<string> storyDept;
	// Use this for initialization
	public void Awake () {
        interestCounter = 10;
        storyPanelInstance = Instantiate(storyPanelPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
        storyPanelInstance.transform.SetParent(GameObject.Find("UI").transform, false);
        foreach (Transform _child in storyPanelInstance.transform)
        {
            if (_child.name.Equals("CloseButton"))
                _child.GetComponent<Button>().onClick.AddListener(() => storyPanelInstance.SetActive(false));
            if (_child.name.Equals("DebtPayButton"))
                _child.GetComponent<Button>().onClick.AddListener(() => PayDebt());
        }

        debtText = storyPanelInstance.transform.Find("DebtText").GetComponent<Text>();
        interestTurnText = storyPanelInstance.transform.Find("InterestText").GetComponent<Text>();
        remainingDebt = (ManagerModel.resourcesMain * 5).ReturnMax();
        maxDebt = remainingDebt * 2;
        GetComponent<Button>().onClick.AddListener(() => storyPanelInstance.SetActive(!storyPanelInstance.activeSelf));
        
        storyPanelInstance.SetActive(false);
	}

    void Start()
    {
        app.Notify(GameNotification.StoryEventDebt, this, "StoryEventExposure");
    }

	// Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            debtText.text = remainingDebt.ToString();
            interestTurnText.text = interestCounter.ToString();

        }
    }

    void PayDebt()
    {
        if (!ManagerModel.resourcesMain.CompareBundle(new ResourceBundle(100, 0, 0))) return;
        app.model.manager.addBundle(new ResourceBundle(0, -100));
          remainingDebt -= 100;

    }

    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        if (p_target != this) return;
        switch (p_event_path)
        {
            case GameNotification.StoryEventDebt:
                EventSystem.OccurEvent((string)p_data[0], this, p_data);
                interestCounter = interestCounter + 3; 
                return;
            case GameNotification.StoryEventInterest:
                EventSystem.OccurEvent("StoryEventInterest", p_data);
                return;
        }
    }




}
