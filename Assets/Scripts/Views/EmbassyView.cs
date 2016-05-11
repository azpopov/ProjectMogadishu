using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmbassyView : BuildingView {

   
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
        influencePointsText.text = gameObject.GetComponent<EmbassyModel>().influenceBonus_.ToString();
    }
        protected override int GlowSprite{
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }
        public void SelectFaction()
        {
            instaceFactionSelectWindow = Instantiate(factionSelectWindowPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
            instaceFactionSelectWindow.transform.SetParent(GameObject.Find("UI").transform, false);
            foreach (Faction _f in Factions.current.factionList)
            {
                GameObject instance = Instantiate(factionElementPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
                instance.transform.SetParent(instaceFactionSelectWindow.transform, false);
                instance.transform.FindChild("FactionName").GetComponent<Text>().text = _f.name.ToString();
                Button instanceButton = instance.transform.FindChild("SetupButton").GetComponent<Button>();
                instanceButton.onClick.AddListener(() => SetFaction(_f));
                instanceButton.onClick.AddListener(() => Destroy(instaceFactionSelectWindow, 0.2f));
                instanceButton.onClick.AddListener(() => SetInterals());
            }
            instaceFactionSelectWindow.SetActive(true);
        }
}
