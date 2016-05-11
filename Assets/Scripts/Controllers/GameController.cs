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
	void Update () {
	
	}

    public virtual void OnNotification(string p_event_path, System.Object p_target, params object[] p_data)
    {
        if (p_target != this) return;
    }

    public GameController[] GetAll()
    {
        return new GameController[]{
            manager,
        };

    }
}
