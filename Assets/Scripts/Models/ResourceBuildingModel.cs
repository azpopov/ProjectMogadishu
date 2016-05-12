using UnityEngine;
using System.Collections;

public class ResourceBuildingModel : BuildingModel {

    //Stores resources to be collected later up to a Maximum
    public int storedResources = 0, maxResources = 500;

    public bool resourcesMaxed = false;
    public int type;

    // Use this for initialization
    void Start()
    {
        timeSinceTick = 0;
    }


    public void addResource()
    {
        if (type == 0)
        {
            app.model.manager.addToResource(0, storedResources);

        }
        else if (type == 1)
        {
            app.model.manager.addToResource(1, storedResources);

        }
        storedResources = 0;
        resourcesMaxed = false;
    }

    public override ResourceBundle GetBuildCost()
    {
        if (type == 0)
            return BuildingCosts.farm;
        else if (type == 1)
            return BuildingCosts.huntersLodge;
        return null;
    }

}
