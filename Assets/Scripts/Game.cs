using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour
{
	//Singleton
	static public Game current;

	public GameModel model;
	public GameView view;
	public GameController controller;
	void Awake()
	{
		if (current == null)
			current = this;
	}
	
}
