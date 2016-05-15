using UnityEngine;
using System.Collections;

public class ResourceBuildingController : BuildingController {

    

   
    // Update is called once per frame
    void Update()
    {
        CheckProduction();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(GetComponent<BuildingController>().IgnoreMouseDownSec());
        if (app.model.manager.productionTut)
        {
            EventSystem.OccurEvent("TutorialProduction");
            app.model.manager.productionTut = false;
        }

    }

    protected override void OnMouseDown()
    {
        //If there are any stored resources
        if (gameObject.GetComponent<ResourceBuildingModel>().storedResources != 0)
            app.Notify(GameNotification.ResourcePickup, this, this);
    }

    public override void ProductionTick()
    {
        gameObject.GetComponent<ResourceBuildingModel>().timeSinceTick++;
    }

    protected override void CheckProduction()
    {
        if (gameObject.GetComponent<ResourceBuildingModel>().timeSinceTick
            < gameObject.GetComponent<ResourceBuildingModel>().timeToTick)
            return;
        gameObject.GetComponent<ResourceBuildingModel>().timeSinceTick = 0;
        Produce();
    }

    public bool CheckMaxStorage()
    {
        if (GetComponent<ResourceBuildingModel>().resourcesMaxed)
            return true;
        return false;
    }


    void Produce()
    {
        if (gameObject.GetComponent<ResourceBuildingModel>().storedResources > gameObject.GetComponent<ResourceBuildingModel>().maxResources)
        {
            gameObject.GetComponent<ResourceBuildingModel>().resourcesMaxed = true;
            return;

        }
        if ( gameObject.GetComponent<ResourceBuildingModel>().type == 0)
        {
            gameObject.GetComponent<ResourceBuildingModel>().storedResources += Random.Range(50, 200);

        }
        else if ( gameObject.GetComponent<ResourceBuildingModel>().type == 1)
        {
            gameObject.GetComponent<ResourceBuildingModel>().storedResources += Random.Range(12, 50);

        }
        gameObject.GetComponent<ResourceBuildingView>().SetGlowSprite();

    }


    public override void OnNotification(string p_event_path, object p_target, params object[] p_data)
    {
        base.OnNotification(p_event_path, p_target, p_data);
        if (p_target != this) return;
        switch (p_event_path) { 
            case GameNotification.ResourcePickup:
                ///Instantiate the floating text
                GameObject floatText = Instantiate(GetComponent<ResourceBuildingView>().floatingTextPrefab);

                //Set parent of floating text to this
                floatText.transform.parent = transform;

                //Set the string for the floating text
                floatText.GetComponentInChildren<FloatText>().setFloatText(GetComponent<ResourceBuildingModel>().storedResources.ToString());

                //Enable the script
                floatText.GetComponent<FloatText>().enabled = true;

                GetComponent<ResourceBuildingView>().SetDefaultSprite();

                //Add stored resources to player Vault
                GetComponent<ResourceBuildingModel>().addResource();

                return;
        }
    }
}
