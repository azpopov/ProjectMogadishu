using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TradeCompleteScript : CustomEvent {

    ShipyardModel.Ship ship;

    void Start()
    {
        ship = (ShipyardModel.Ship)data[0];
        CompleteJourney();
    }
	public void CompleteJourney()
	{
        string customText;
        
        int rndChance = UnityEngine.Random.Range (0, 101);
        if(EmbassyModel.influenceBonuses.Count != 0)
             rndChance += (int)EmbassyModel.influenceBonuses[ship.theMission.f.name];
		float amountReceived = 0;
        string result;
		if (rndChance <= 20) {
			result = "Disastrous Trading";
            amountReceived = (float)SuccessfulJourney() * 0.6f;
		} else if (rndChance <= 80) {
            result = "Favorable Trading";
            amountReceived = (float)SuccessfulJourney();
		} else {
            result = "Incredible Trading";
            amountReceived = (float)SuccessfulJourney() * 1.5f;
		}
        foreach (Transform child in transform)
        {
            if (child.name.Equals("TitleText"))
                child.GetComponent<Text>().text = child.GetComponent<Text>().text.Replace("[shipname]", ship.name);
            if (child.name == "ContentText")
            {
                customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[faction]", ship.theMission.f.name);
                customText = customText.Replace("[shipname]", ship.name);
                customText = customText.Replace("[outcome]", result);
                customText = customText.Replace("[amountrequest]", ship.theMission.requestResource.ReturnMax().ToString());
                customText = customText.Replace("[amountreceive]", amountReceived.ToString());
                customText = customText.Replace("[requesttype]", ship.theMission.requestResource.ReturnStringofMax().ToString());
                customText = customText.Replace("[targettype]", ship.theMission.targetResource.ReturnStringofMax().ToString());
                child.GetComponent<Text>().text = customText;
            }
        }
        app.Notify(GameNotification.EmbassyFactionInfluence, app.controller.manager, ship.theMission.f);
        app.model.manager.addBundle (new ResourceBundle(ship.theMission.targetResource.ReturnTypeofMax(), (int)amountReceived));

	}
	int SuccessfulJourney()
	{
        float amountReturned = (float)ship.theMission.targetResource.ReturnMax();
		amountReturned *= ship.theMission.f.tradeBias;
		
		if (ship.theMission.requestResource.ReturnTypeofMax() == 0
            && ship.theMission.targetResource.ReturnTypeofMax() == 1) {
			amountReturned /= 2f;
		} else if (ship.theMission.requestResource.ReturnTypeofMax() == 0 
            && ship.theMission.targetResource.ReturnTypeofMax() == 2) {
			amountReturned /= 6f;
		} else if (ship.theMission.requestResource.ReturnTypeofMax() == 1
            && ship.theMission.targetResource.ReturnTypeofMax() == 2)
        {
			amountReturned *= 0.6f;
		} else if (ship.theMission.requestResource.ReturnTypeofMax() == 1
            && ship.theMission.targetResource.ReturnTypeofMax() == 0)
        {
			amountReturned *= 2.2f;
        }
        else if (ship.theMission.requestResource.ReturnTypeofMax() == 
            ship.theMission.targetResource.ReturnTypeofMax())
			amountReturned *= 1.7f;
		
		return (int)amountReturned;
	}
	public override void OnDisable ()
	{
		Factions.current.RemoveTradeMission (this.ship.theMission);
        Destroy(ship.theMission.gameObject);
		base.OnDisable ();

	}
}
