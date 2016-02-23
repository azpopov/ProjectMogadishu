using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int Health = 100;
	public int Damage = 20;
	public int PointsToGive = 5;

	public void OnMouseDown ()
	{

		Health -= Damage;

		//Ref TextOnSpot(string name, string text, float speed, Transform obj) 
		GameHud.Instance.TextOnSpot("TextOnSpot","-"+Damage+"!", 1f, gameObject.transform);

		if(Health <= 0) {
			GameHud.Instance.AddPoints(PointsToGive);

			//Ref howCenterText("Any Text", time);
			GameHud.Instance.ShowCenterText(gameObject.name + " was destroyed", 2f);
			Destroy (gameObject);
		}
	}
}
