using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryManager : GameElement {
    public GameObject storyPanelPrefab;
    GameObject storyPanelInstance;
    protected Button endTurnButton, disableButton, debtButton;
   public int remainingDebt, maxDebt;
    int _interestCounter, vascoStoryCounter;
    public bool debtStory, vascoStory;
    public int interestCounter
    {
        set
        {
            _interestCounter = value;
            if (_interestCounter <= 0)
            {
                remainingDebt = (int)((float)remainingDebt * 2f);
                app.Notify(GameNotification.StoryEventInterest, this, this);
                if (remainingDebt >= maxDebt) app.Notify(GameNotification.GameOver, app.controller.manager, "debt" ,remainingDebt);
                _interestCounter = 6;
            }
        }
        get {
           return _interestCounter;
        }
    }

    int _vascoCounter;
    public int vascoCounter
    {
        set
        {
            if (!vascoStory) return;
            _vascoCounter = value;
            if (_vascoCounter <= 0)
            {
                app.Notify(GameNotification.VascoStory, this);
                _vascoCounter = 5;
                if (vascoStoryCounter == 4)
                    _vascoCounter = 7;
            }
        }
        get
        {
            return _vascoCounter;
        }
    }
    int _vascoAnger;
    public int vascoAnger {
        set {
            if (_vascoAnger <= value)
                _vascoAnger += 20;
            else { 
                if(_vascoAnger != 0)
                    _vascoAnger -= 10;
            }
               
            if (_vascoAnger >= 100)
            {
                app.Notify(GameNotification.GameOver, app.controller.manager, "vascoAnger");
            }
            int rndChance = UnityEngine.Random.Range(0,101);
            if (_vascoAnger > rndChance)
            {
                app.Notify(GameNotification.VascoAnger, this);
            }
        }
        get {
            return _vascoAnger;
        }
    }
    Text debtText, interestTurnText, vascoText, vascoCounterText, moneyValue;
    List<string> storyDept;
	// Use this for initialization
	public void Awake () {
        vascoStoryCounter = 10;
        interestCounter = 10;
        debtStory = true;
        vascoStory = false;
        storyPanelInstance = Instantiate(storyPanelPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
        storyPanelInstance.transform.SetParent(GameObject.Find("UI").transform, false);
        foreach (Transform _child in storyPanelInstance.transform)
        {
            if (_child.name.Equals("CloseButton"))
                _child.GetComponent<Button>().onClick.AddListener(() => storyPanelInstance.SetActive(false));
            if (_child.name.Equals("DebtPayButton"))
                debtButton = _child.GetComponent<Button>();
        }
        debtButton.onClick.AddListener(() => PayDebt());
        debtText = storyPanelInstance.transform.Find("DebtText").GetComponent<Text>();
        interestTurnText = storyPanelInstance.transform.Find("InterestText").GetComponent<Text>();
        vascoText = storyPanelInstance.transform.Find("VascoText").GetComponent<Text>();
        vascoCounterText = storyPanelInstance.transform.Find("ReturnText").GetComponent<Text>();
        remainingDebt = (ManagerModel.resourcesMain * 3).ReturnMax();
        maxDebt = remainingDebt * 2;
        GetComponent<Button>().onClick.AddListener(() => storyPanelInstance.SetActive(!storyPanelInstance.activeSelf));
        moneyValue = storyPanelInstance.transform.Find("NetWorthText").GetComponent<Text>();
        storyPanelInstance.SetActive(false);
	}

    void Start()
    {
        app.Notify(GameNotification.StoryEventDebt, this, "StoryEventExposure");
    }

	// Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (debtStory)
            {
                debtText.text = remainingDebt.ToString();
                interestTurnText.text = interestCounter.ToString();
            }
            if (true)
            {
                vascoText.text = vascoAnger.ToString();
                vascoCounterText.text = vascoCounter.ToString();
               
            }
            float currentWorth = (float)app.controller.manager.GetNetWorth();
            float neededWorth = (float)app.model.manager.winningWorth;
            moneyValue.text = ((currentWorth / neededWorth) * 100).ToString() + "%";
        }
    }

    void PayDebt()
    {
        if (!ManagerModel.resourcesMain.CompareBundle(new ResourceBundle(0, 100, 0))) return;
        app.model.manager.addBundle(new ResourceBundle(1, -100));
          remainingDebt -= 100;
          if (remainingDebt <= 0)
          {
              remainingDebt = 0;
              debtButton.onClick.RemoveAllListeners();
          }
    }

    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        if (p_target != this) return;
        switch (p_event_path)
        {
            case GameNotification.StoryEventDebt:
                EventSystem.OccurEvent((string)p_data[0], this, p_data);
                return;
            case GameNotification.StoryEventInterest:
                EventSystem.OccurEvent("StoryEventInterest", p_data);
                return;
            case GameNotification.VascoStory:
                switch (vascoStoryCounter)
                {
                    case 0:
                        EventSystem.OccurEvent("VascoSpookEvent", p_data);
                        break;
                    case 1:
                        EventSystem.OccurEvent("VascoShowsUpEvent", p_data);
                        break;
                    case 2:
                        EventSystem.OccurEvent("VascoShipNameEvent", p_data);
                        break;
                    case 3:
                        EventSystem.OccurEvent("VascoStealsTradeEvent", p_data);
                        break;
                    case 4:
                        EventSystem.OccurEvent("VascoExtortEvent", p_data);
                        break;
                }
                if(vascoStoryCounter != 4)
                    vascoStoryCounter++;
                return;
            case GameNotification.VascoAnger:
                string vascoEvent = EventSystem.RandomTravelEvent("VascoAnger");
                EventSystem.OccurEvent(vascoEvent, p_data);
                return;
                
        }
    }




}
