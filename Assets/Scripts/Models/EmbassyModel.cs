using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmbassyModel : BuildingModel {

    Faction f;
    public GameObject embassyManagementPrefab, factionSelectWindowPrefab, factionElementPrefab;
    public float influenceBonus_;
    public float influenceBonus{ get {return influenceBonus_;} set {influenceBonus_ = value;} }
    // influencePointsText.text = influenceBonus_.ToString()
    void Awake()
    {
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
