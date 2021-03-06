using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour
{
	//Singleton
	static public Game current;

    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this);
    }

	public GameModel model;
	public GameView view;
	public GameController controller;

    public void Notify(string p_event_path, System.Object p_target, params object[] p_data)
    {
        List<GameElement> controller_list = new List<GameElement>();
        controller_list = GetAllControllers();
        foreach (GameElement c in controller_list)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }

    }

    // Fetches all scene Controllers.
    public List<GameElement> GetAllControllers() { return controller.GetAll() ; }
	
}
