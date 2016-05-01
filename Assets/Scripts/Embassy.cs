using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Embassy : Building
{
	Factions.faction f;
	public GameObject embassyManagementPrefab, factionSelectWindowPrefab, factionElementPrefab;
	GameObject embassyUI, instaceFactionSelectWindow;
	Image embassyInsig;
	Text influencePointsText;

	Button changeFactionBtn, receiveEnvoyBtn;
	void Awake()
	{
		embassyUI = Instantiate(embassyManagementPrefab, new Vector3(0, 0), Quaternion.identity) as GameObject;
		embassyUI.transform.SetParent (GameObject.Find ("UI").transform, false);
		embassyInsig = transform.FindChild("Insignia").GetComponent<Image>();
		influencePointsText = transform.FindChild ("Influence Points").GetComponent<Text> ();
		changeFactionBtn = transform.FindChild ("ChangeFactionButton").GetComponent<Button> ();
		receiveEnvoyBtn = transform.FindChild ("ReceiveEnvoyButton").GetComponent<Button> ();
		transform.FindChild ("CloseButton").GetComponent<Button>().onClick.AddListener(() => gameObject.SetActive(false));
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
		instaceFactionSelectWindow.transform.SetParent (GameObject.Find ("UI").transform);
		foreach (Factions.faction _f in System.Enum.GetValues(typeof(Factions.faction))) {
			GameObject instance = Instantiate (factionElementPrefab, new Vector3 (0, 0), Quaternion.identity) as GameObject;
			instance.transform.SetParent (instaceFactionSelectWindow.transform, false);
			instance.GetComponentInChildren<Text> ().text = _f.ToString();
			Button instanceButton = instance.GetComponentInChildren<Button> ();
			instanceButton.onClick.AddListener (() => SetFaction(_f));
			instanceButton.onClick.AddListener (() => Destroy(instaceFactionSelectWindow, 0.2f));
		}
	}
	void SetFaction(Factions.faction _f)
	{
		f = _f;
	}
			                                   
	void SetInterals()
	{
		embassyInsig.sprite = Factions.current.insignias [0];
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
		throw new System.NotImplementedException ();
	}

	protected override void OnEnable ()
	{
		if(Game.current.embassyTut)
			EventSystem.OccurEvent ("FirstEmbassyEvent");
	}

	protected override void OnMouseDown ()
	{

	}

	public override void ProductionTick ()
	{
		throw new System.NotImplementedException ();
	}
}
