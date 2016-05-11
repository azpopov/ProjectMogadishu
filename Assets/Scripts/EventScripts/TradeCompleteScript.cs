using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TradeCompleteScript : CustomEvent {

	TradeMission tradeComplete;
	
	public override void OnEnable ()
	{
		base.OnEnable ();
		CompleteJourney ();
	}

	public void CompleteJourney()
	{
		tradeComplete = EventSystem.pendingMissions [0];

		tradeComplete.resultBias += UnityEngine.Random.Range (0, 100);
		int amountReceived = 0;
		string resultText = "The "+ tradeComplete.shipName  + " has returned!\nThe Captain would like to report:\n ";
		
		if (tradeComplete.resultBias <= 20) {
			resultText += "DISASTROUS!\n";
			amountReceived = DisasterJourney (tradeComplete);
			resultText += "You haven't received anything sadly";
		} else if (tradeComplete.resultBias <= 80) {
			resultText += "SUCCESSFUL!\n ";
			amountReceived = SuccessfulJourney (tradeComplete);
			
		} else {
			resultText += "AMAZINGLY SUCCESSFUL!\n";
			amountReceived = AmazingJourney (tradeComplete);
			
		}
		resultText += "We Received "+ amountReceived.ToString ();
		
		transform.FindChild ("TradeCompleteText").GetComponent<Text> ().text = resultText;
		Game.current.addToResource (tradeComplete.targetResource.ReturnTypeofMax(), amountReceived);


	}
	int SuccessfulJourney(TradeMission tradeComplete)
	{
        float amountReturned = (float)tradeComplete.requestResource.ReturnMax();
		amountReturned *= tradeComplete.f.tradeBias;
		
		if (tradeComplete.requestResource.ReturnTypeofMax() == 0
            && tradeComplete.targetResource.ReturnTypeofMax() == 1) {
			amountReturned /= 2f;
		} else if (tradeComplete.requestResource.ReturnTypeofMax() == 0 
            && tradeComplete.targetResource.ReturnTypeofMax() == 2) {
			amountReturned /= 6f;
		} else if (tradeComplete.requestResource.ReturnTypeofMax() == 1
            && tradeComplete.targetResource.ReturnTypeofMax() == 2)
        {
			amountReturned *= 0.6f;
		} else if (tradeComplete.requestResource.ReturnTypeofMax() == 1
            && tradeComplete.targetResource.ReturnTypeofMax() == 0)
        {
			amountReturned *= 2.2f;
        }
        else if (tradeComplete.requestResource.ReturnTypeofMax() == 
            tradeComplete.targetResource.ReturnTypeofMax())
			amountReturned *= 1.7f;
		
		return (int)amountReturned;
	}

	int DisasterJourney(TradeMission script)
	{
		Debug.Log ("disaster not implemented");
		return 0;
	}
	int AmazingJourney(TradeMission script)
	{
		Debug.Log ("Amazing not implemented");
		return 0;	
	}
	public override void OnDisable ()
	{
		EventSystem.pendingMissions.RemoveAt(0);
		Factions.current.RemoveTradeMission (this.tradeComplete);
        Destroy(tradeComplete.gameObject);
		base.OnDisable ();

	}
}
