//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//private class Embassy : Building
//{
//    //Faction f;
//    //public GameObject embassyManagementPrefab, factionSelectWindowPrefab, factionElementPrefab;
//    //GameObject embassyUI, instaceFactionSelectWindow;
//    //Image embassyInsig;
//    //Text influencePointsText;
//    //CanvasGroup cnvGroup;
//    //Button changeFactionBtn, receiveEnvoyBtn;

//    //float influenceBonus_;
//    //static System.Random rndGen = new System.Random();
//    //public float influenceBonus{
//    //    get{
//    //        return influenceBonus_;
//    //    }
//    //    set{
//    //        influenceBonus_ = value;
//    //        influencePointsText.text = influenceBonus_.ToString(); 
//    //    }
//    //}
//    //void Awake()
//    //{
//    //    embassyUI = Instantiate(embassyManagementPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
//    //    embassyUI.transform.SetParent (GameObject.Find ("UI").transform, false);
//    //    cnvGroup = embassyUI.GetComponent<CanvasGroup> ();
//    //    cnvGroup.blocksRaycasts = false;
//    //    embassyUI.SetActive (false);
//    //    embassyInsig = embassyUI.transform.FindChild("Insignia").GetComponent<Image>();
//    //    influencePointsText = embassyUI.transform.FindChild ("Influence Points").GetComponent<Text> ();
//    //    changeFactionBtn = embassyUI.transform.FindChild ("ChangeFactionButton").GetComponent<Button> ();
//    //    receiveEnvoyBtn = embassyUI.transform.FindChild ("ReceiveEnvoyButton").GetComponent<Button> ();
//    //    embassyUI.transform.FindChild ("CloseButton").GetComponent<Button>().onClick.AddListener(() => embassyUI.SetActive(false));
//    //    changeFactionBtn.onClick.AddListener (() => SelectFaction ());

//    //    timeToTick = 2;
//    //}


//    //// Use this for initialization
//    //void Start ()
//    //{
//    //    if (spriteRnd == null)
//    //        spriteRnd = GetComponent<SpriteRenderer> ();
//    //}

//    //// Update is called once per frame
//    //void FixedUpdate ()
//    //{
//    //    CheckProduction ();
//    //}

//    //void SelectFaction(){
//    //    instaceFactionSelectWindow = Instantiate (factionSelectWindowPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
//    //    instaceFactionSelectWindow.transform.SetParent (GameObject.Find ("UI").transform, false);
//    //    foreach (Faction _f in Factions.current.factionList) {
//    //        GameObject instance = Instantiate (factionElementPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
//    //        instance.transform.SetParent (instaceFactionSelectWindow.transform, false);
//    //        instance.transform.FindChild("FactionName").GetComponent<Text>().text = _f.name.ToString();
//    //        Button instanceButton = instance.transform.FindChild("SetupButton").GetComponent<Button> ();
//    //        instanceButton.onClick.AddListener (() => SetFaction(_f));
//    //        instanceButton.onClick.AddListener (() => Destroy(instaceFactionSelectWindow, 0.2f));
//    //        instanceButton.onClick.AddListener (() => SetInterals());
//    //    }
//    //    instaceFactionSelectWindow.SetActive (true);
//    //}
//    //void SetFaction(Faction _f)
//    //{
//    //    f = _f;
//    //}                                  
//    //void SetInterals()
//    //{
//    //    embassyInsig.sprite = f.insignia;
//    //}


//    //protected override void CheckProduction ()
//    //{
//    //    if (timeSinceTick >= timeToTick) 
//    //    {

//    //        if(rndGen.Next(5) == 4)
//    //            spriteRnd.sprite = glowSprite[0];
//    //    }
//    //}

//    //protected override int GlowSprite {
//    //    get {
//    //        throw new System.NotImplementedException ();
//    //    }
//    //    set {
//    //        throw new System.NotImplementedException ();
//    //    }
//    //}

//    //protected override void OnDisable ()
//    //{

//    //}

//    //protected override void OnEnable ()
//    //{
//    //    base.OnEnable ();
//    //    StartCoroutine (GetComponent<Building>().IgnoreMouseDownSec());
//    //    if (Game.current.model.manager.embassyTut) {
//    //        EventSystem.OccurEvent ("FirstEmbassyEvent");
//    //        Game.current.model.manager.embassyTut = false;
//    //    }

//    //}

	

//    //protected override void OnMouseDown ()
//    //{
//    //    if (!gameObject.activeSelf  && EventSystem.eventPresent != null)
//    //        return;
//    //    embassyUI.SetActive (!embassyUI.activeSelf);
//    //    cnvGroup.blocksRaycasts = embassyUI.activeSelf;
//    //    cnvGroup.interactable = embassyUI.activeSelf;

//    //}

//    //public override void ProductionTick ()
//    //{
//    //    timeSinceTick++;
//    //}

//    //public override ResourceBundle GetBuildCost()
//    //{
//    //    return new ResourceBundle(300, 200, 200);
//    //}
//}
