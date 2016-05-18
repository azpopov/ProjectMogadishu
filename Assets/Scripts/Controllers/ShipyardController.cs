using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipyardController : BuildingController {

	Button newShipPopUpButton;
    public GameObject newShipPopUpPrefab;

    protected override void CheckProduction()
    {
        foreach (ShipyardModel.Ship _ship in gameObject.GetComponent<ShipyardModel>().shipsInShipyard)
        {
            if (_ship.theMission != null)
            {
                _ship.theMission.SailTick(1);
                if (_ship.theMission.timeToDest <= 0f)
                {
                    EventSystem.OccurEvent("TradeComplete", _ship);
                    _ship.theMission.CancelSailing();
                    return;
                }
                if (Random.Range(0, 10) < 3 && GetComponent<ShipyardModel>().eventShip == null)
                {
                    GetComponent<ShipyardModel>().eventShip = _ship;
                    GetComponent<ShipyardView>().SetGlowSprite();
                }
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
        StartCoroutine(GetComponent<BuildingController>().IgnoreMouseDownSec());
        if (app.model.manager.shipyardTut)
        {
            EventSystem.OccurEvent("TutorialShipyard");
            app.model.manager.shipyardTut = false;
        }
        app.model.manager.maxShips += 1;
        EventSystem.OccurEvent("NewShipPopUp");
       
    }

    public void CreateShip(string _name)
    {
        ShipyardModel.Ship _ship = new ShipyardModel.Ship(_name);
        GetComponent<ShipyardModel>().shipsInShipyard.Add(_ship);
    }

    protected override void OnMouseDown()
    {
        if (GetComponent<ShipyardModel>().eventShip != null)
        {
            ShipyardModel.Ship newShip = GetComponent<ShipyardModel>().eventShip;
            app.Notify(GameNotification.ShipTravelEvent, this, newShip);
            GetComponent<ShipyardView>().SetDefaultSprite();
            GetComponent<ShipyardModel>().eventShip = null;
        }
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
            case GameNotification.ShipTravelEvent:
                string eventName = EventSystem.RandomTravelEvent("TravelEvent");
                if (eventName.Equals("none loaded")) return;
                EventSystem.OccurEvent(eventName, p_data);
                return;
            case GameNotification.ShipOnMission:
                int index = GetComponent<ShipyardModel>().shipsInShipyard.IndexOf((ShipyardModel.Ship)p_data[1]);
                ShipyardModel.Ship newShip = new ShipyardModel.Ship(((ShipyardModel.Ship)p_data[1]).name, p_data[0] as TradeMission);
                GetComponent<ShipyardModel>().shipsInShipyard[index] = newShip;
                return;

        }
    }
   
}
