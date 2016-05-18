using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VascoAngerFireScript : CustomEvent
{
    Text contentText;

    // Use this for initialization
    void Start()
    {
        BuildingController building = new BuildingController();
        int rndNum = 0;
        while (true)
        {
            rndNum = Random.Range(0, app.controller.buildings.Count);
            if (app.controller.buildings[rndNum] is ResourceBuildingController)
                building = app.controller.buildings[rndNum];
            break;
        }
        string type ="";
        switch(building.GetComponent<ResourceBuildingModel>().type)
        {
            case 0:
                type = " Farm ";
                break;
            case 1:
                type = "Hunter's Lodge";
                break;
            case 2:
                type = "Goldsmith";
                break;

        }
        foreach (Transform _child in transform)
        {
            if (_child.name.Equals("ContentText"))
                _child.GetComponent<Text>().text = _child.GetComponent<Text>().text.Replace("[building]", type);
        }
        app.controller.buildings.Remove(building);
        Destroy(building);

    }
}
