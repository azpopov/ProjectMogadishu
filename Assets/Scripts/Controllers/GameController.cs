using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : GameElement {

	public ManagerController manager;
    public List<BuildingController> buildings;
    void Awake()
    {
        buildings = new List<BuildingController>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    

    public List<GameElement> GetAll()
    {
        List<GameElement> controllers = new List<GameElement>();
        controllers.AddRange(buildings.ToArray());
        controllers.Add(manager);
        return controllers;

    }
}
