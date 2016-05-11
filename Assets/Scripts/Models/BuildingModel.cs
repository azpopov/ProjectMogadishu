using UnityEngine;
using System.Collections;

public class BuildingModel : GameModel {
    public float ignoreTime = 0.5f;
    //Counts up to timeToTick then adds resources to Building
    public int timeSinceTick = 0;
    public int timeToTick = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual ResourceBundle GetBuildCost()
    {
        return null;
    }
    
}
