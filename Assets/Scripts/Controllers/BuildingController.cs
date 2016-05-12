using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class BuildingController : GameElement
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
        ManagerModel.resourcesMain -= GetComponent<BuildingModel>().GetBuildCost();
    }

    protected virtual void OnDestroy()
    {
        try
        {
            if (app.controller.buildings.Contains(this))
                app.controller.buildings.Remove(this);
        }
        catch { }
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
