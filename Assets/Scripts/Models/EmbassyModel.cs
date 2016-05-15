using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EmbassyModel : BuildingModel {

    public Faction f = new Faction();
    public GameObject embassyManagementPrefab, factionSelectWindowPrefab, factionElementPrefab;
    public static Dictionary<string, int> influenceBonuses = new Dictionary<string, int>();
    // influencePointsText.text = influenceBonus_.ToString()
    void Awake()
    {
        if (influenceBonuses.Count != Factions.current.factionList.Count)
        {
            influenceBonuses.Clear();
            foreach (Faction _f in Factions.current.factionList)
                influenceBonuses.Add(_f.name, 0);
        }
        timeToTick = 2;
    }

    public void SetFaction(Faction _f)
    {
        f = _f;

    }
   

    public override ResourceBundle GetBuildCost()
    {
        return BuildingCosts.embassy; 
    }
}
