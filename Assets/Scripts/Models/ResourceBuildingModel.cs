using UnityEngine;
using System.Collections;

public class ResourceBuildingModel : BuildingModel {

    //Stores resources to be collected later up to a Maximum
    //public int storedResources = 0, maxResources = 500;

    public ResourceBundle storedResources;
    public static ResourceBundle maxResources;

    public bool resourcesMaxed = false;
    public int type;

    // Use this for initialization
    void Start()
    {
        storedResources = new ResourceBundle(0, 0, 0);
        maxResources = new ResourceBundle(300, 200, 100);
        timeSinceTick = 0;
    }


    public void addResource()
    {
        app.model.manager.addBundle(storedResources);
        storedResources = new ResourceBundle(0, 0, 0);
        resourcesMaxed = false;
    }

    public override ResourceBundle GetBuildCost()
    {
        if (type == 0)
            return BuildingCosts.farm;
        else if (type == 1)
            return BuildingCosts.huntersLodge;
        else if (type == 2)
            return BuildingCosts.goldsmith;
        return null;
    }

}
