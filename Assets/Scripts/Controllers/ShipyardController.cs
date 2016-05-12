using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipyardController : BuildingController {

	Button newShipPopUpButton;
    public GameObject newShipPopUpPrefab;
	protected override void CheckProduction ()
	{
		foreach (ShipyardModel.Ship _ship in gameObject.GetComponent<ShipyardModel>().shipsInShipyard) {
			if (_ship.theMission != null && Random.Range (0, 10) < 3) {
				GenerateEvent ();
			}
		}
	}

	public override void ProductionTick ()
	{
		CheckProduction ();
	}

    protected override void OnEnable()
    {
        base.OnEnable();
        app.model.manager.maxShips += 1;
        EventSystem.OccurEvent("NewShipPopUp");
    }

    public void CreateShip(string _name)
    {
        ShipyardModel.Ship _ship = new ShipyardModel.Ship(_name);
        GetComponent<ShipyardModel>().shipsInShipyard.Add(_ship);
    }


	void GenerateEvent ()
	{
        gameObject.GetComponent<ShipyardView>().spriteRnd.sprite = gameObject.GetComponent<ShipyardView>().glowSprite;

	}
    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        if (this != p_target) return;
        switch (p_event_path)
        {
            case GameNotification.ShipyardCreateShipUI:
                GetComponent<ShipyardView>().CreateShipUI(p_data[0] as TradeMission);
                return;
        }
    }
   
	protected override void OnMouseDown ()
	{

	}
}
