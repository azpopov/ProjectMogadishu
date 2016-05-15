using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EmbassyView : BuildingView {

    [NonSerialized]
    public GameObject embassyUI, instaceFactionSelectWindow;
    Image embassyInsig;
    Text influencePointsText;
    internal CanvasGroup cnvGroup;

    void Awake()
    {
        embassyUI = Instantiate(gameObject.GetComponent<EmbassyModel>().embassyManagementPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
        embassyUI.transform.SetParent(GameObject.Find("UI").transform, false);
        cnvGroup = embassyUI.GetComponent<CanvasGroup>();
        cnvGroup.blocksRaycasts = false;
        embassyUI.SetActive(false);
        embassyInsig = embassyUI.transform.FindChild("Insignia").GetComponent<Image>();
        influencePointsText = embassyUI.transform.FindChild("Influence Points").GetComponent<Text>();
        embassyUI.transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(() => embassyUI.SetActive(false));

    }

    // Use this for initialization
    void Start()
    {
        if (spriteRnd == null)
            spriteRnd = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if(GetComponent<EmbassyModel>().f.minDistance != 0){
           influencePointsText.text = EmbassyModel.influenceBonuses[GetComponent<EmbassyModel>().f.name].ToString() + "influence points";
           
        }
    }

    public void SelectFaction()
    {
        instaceFactionSelectWindow = Instantiate(gameObject.GetComponent<EmbassyModel>().factionSelectWindowPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
        instaceFactionSelectWindow.transform.SetParent(GameObject.Find("UI").transform, false);
        instaceFactionSelectWindow.GetComponentInChildren<Button>().onClick.AddListener(() => Destroy(instaceFactionSelectWindow, 0.2f));
        foreach (Faction _f in Factions.current.factionList)
        {
            GameObject instance = Instantiate(gameObject.GetComponent<EmbassyModel>().factionElementPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
            instance.transform.SetParent(instaceFactionSelectWindow.transform, false);
            instance.transform.FindChild("FactionName").GetComponent<Text>().text = _f.name.ToString();
            Button instanceButton = instance.transform.FindChild("SetupButton").GetComponent<Button>();
            instanceButton.onClick.AddListener(() => Destroy(instaceFactionSelectWindow, 0.2f));
            Faction newFaction = _f;
            instanceButton.onClick.AddListener(() => app.Notify(GameNotification.EmbassyFactionChange, GetComponent<EmbassyController>(), this, newFaction));
        }
        instaceFactionSelectWindow.SetActive(true);
    }

    public void UpdateUI()
    {   
        embassyInsig.sprite = GetComponent<EmbassyModel>().f.insignia;
        embassyInsig.color = new Color(255,255,255, 1);
    }
}
