using UnityEngine;
using System.Collections;

public class BuildingModel : GameElement {
    public float ignoreTime = 0.5f;
    //Counts up to timeToTick then adds resources to Building
    public int timeSinceTick = 0;
    public int timeToTick = 1;

    public virtual ResourceBundle GetBuildCost()
    {
        return null;
    }
    
}
