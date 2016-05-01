using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Embassy : Building
{

	public GameObject embassyManagementPrefab;

	GameObject embassyUI = null;
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

	}

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
