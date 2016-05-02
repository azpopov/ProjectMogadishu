using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Embassy : Building
{
	Faction f;
	public GameObject embassyManagementPrefab, factionSelectWindowPrefab, factionElementPrefab;
	GameObject embassyUI, instaceFactionSelectWindow;
	Image embassyInsig;
	Text influencePointsText;

	Button changeFactionBtn, receiveEnvoyBtn;

	float influenceBonus_;
	public float influenceBonus{
		get{
			return influenceBonus_;
		}
		set{
			influenceBonus_ = value;
			influencePointsText.text = influenceBonus_.ToString(); 
		}
	}
	void Awake()
	{
		embassyUI = Instantiate(embassyManagementPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
		embassyUI.transform.SetParent (GameObject.Find ("UI").transform, false);
		embassyUI.SetActive (false);
		embassyInsig = embassyUI.transform.FindChild("Insignia").GetComponent<Image>();
		influencePointsText = embassyUI.transform.FindChild ("Influence Points").GetComponent<Text> ();
		changeFactionBtn = embassyUI.transform.FindChild ("ChangeFactionButton").GetComponent<Button> ();
		receiveEnvoyBtn = embassyUI.transform.FindChild ("ReceiveEnvoyButton").GetComponent<Button> ();
		embassyUI.transform.FindChild ("CloseButton").GetComponent<Button>().onClick.AddListener(() => embassyUI.SetActive(false));
		changeFactionBtn.onClick.AddListener (() => SelectFaction ());
	}


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void SelectFaction(){
		instaceFactionSelectWindow = Instantiate (factionSelectWindowPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
		instaceFactionSelectWindow.transform.SetParent (GameObject.Find ("UI").transform, false);
		foreach (Faction _f in Factions.current.factionList) {
			GameObject instance = Instantiate (factionElementPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent (instaceFactionSelectWindow.transform, false);
			instance.transform.FindChild("FactionName").GetComponent<Text>().text = _f.name.ToString();
			Button instanceButton = instance.transform.FindChild("SetupButton").GetComponent<Button> ();
			instanceButton.onClick.AddListener (() => SetFaction(_f));
			instanceButton.onClick.AddListener (() => Destroy(instaceFactionSelectWindow, 0.2f));
			instanceButton.onClick.AddListener (() => SetInterals());
		}
		instaceFactionSelectWindow.SetActive (true);
	}
	void SetFaction(Faction _f)
	{
		f = _f;
	}
			                                   
	void SetInterals()
	{
		embassyInsig.sprite = f.insignia;
	}
	

//	public void CreateShipUI (TradeMission mission)
//	{
//		
//		GameObject shipyardUI;
//		shipyardUI = Game.current.uiElements ["ShipyardWindow"];
//		foreach (Ship _ship in shipsInShipyard) {
//			GameObject instance = Instantiate (shipUIPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
//			instance.transform.SetParent (shipyardUI.transform, false);
//			instance.GetComponentInChildren<Text> ().text = _ship.name;
//			Button instanceButton = instance.GetComponentInChildren<Button> ();
//			if (_ship.theMission != null) {
//				instanceButton.interactable = false;
//			} else {
//				instanceButton.onClick.AddListener (() => mission.StartSailing (_ship));
//				instanceButton.onClick.AddListener (() => SetMission (_ship, mission));
//				instanceButton.onClick.AddListener (() => Game.current.DestroyShipUIInstances ());
//			}
//		}
//		
//		
//	}

	protected override void CheckProduction ()
	{
		throw new System.NotImplementedException ();
	}

	protected override int GlowSprite {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	protected override void OnDisable ()
	{

	}

	protected override void OnEnable ()
	{
		if (Game.current.embassyTut) {
			EventSystem.OccurEvent ("FirstEmbassyEvent");
			Game.current.embassyTut = false;
		}
	}

	protected override void OnMouseDown ()
	{
		embassyUI.SetActive (true);
	}

	public override void ProductionTick ()
	{
		throw new System.NotImplementedException ();
	}
}
