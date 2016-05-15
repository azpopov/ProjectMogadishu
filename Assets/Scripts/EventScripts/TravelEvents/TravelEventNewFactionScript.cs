using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TravelEventNewFactionScript : CustomEvent {
    ShipyardModel.Ship ship;
    string customText;
	// Use this for initialization
	void Start () {
	
          Faction newFaction = Factions.current.DiscoverNewEmpire();
          if (newFaction.minDistance == 0) gameObject.SetActive(false);
          foreach (Transform child in transform)
              if (child.name == "ContentText")
              {
                  customText = child.GetComponent<Text>().text;
                  customText = customText.Replace("[faction]", newFaction.name);
                  child.GetComponent<Text>().text = customText;
              }
          return;
	}
	

}
