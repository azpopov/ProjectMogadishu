using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class BuildingController : GameController
{
    public virtual void ProductionTick()
    {
    }

    protected virtual void OnMouseDown()
    {
    }

    protected virtual void CheckProduction()
    {
    }

    protected virtual void OnEnable()
    {
        app.controller.buildings.Add(this);
        ManagerModel.resourcesMain -= GetBuildCost();
    }

    protected virtual void OnDisable()
    {
    }
    public virtual ResourceBundle GetBuildCost()
    {
        return null;
    }
    internal System.Collections.IEnumerator IgnoreMouseDownSec()
    {

        float timer = 0.0f;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //while we still have animation time
        while (timer <= gameObject.GetComponent<BuildingModel>().ignoreTime)
        {
            //add time passed so far
            timer += Time.deltaTime;
            //wait till next frame
            yield return null;
        }
        gameObject.layer = LayerMask.NameToLayer("Building");
    }
}
