﻿using UnityEngine;
using System.Collections;

public class FloatText : MonoBehaviour {
	private Vector3 start;         //the starting point for lerping movement
	private Vector3 end;           //ending point
	
	public Vector3 offset = new Vector3(0f, 1f, 0f); //added relative distance to move
	public float randomX = 0.5f;   //randomize movement left/right +/- this value
	public float time = 2.0f;      //time to move. this with distance will determine speed
	
	public AnimationCurve acMove;  //animation curve to move transform
	public AnimationCurve acAlpha; //animation curve to move alpha
	public TextMesh textMesh;

	public string textForTextMesh;
	void Start () {
		transform.position = transform.parent.position;
		transform.Translate (0f, 0.5f, -1f);
		//get start position
		start = transform.position;
		
		//get offset
		end = start + offset;
		
		//move the x randomly left/right a little
		//end.x += Random.Range(-randomX, randomX);
		textMesh = GetComponent<TextMesh>();

		textMesh.text = textForTextMesh;
		//start the animation
		StartCoroutine(Animate(start, end));
	}
	
	IEnumerator Animate(Vector3 pos1, Vector3 pos2) {
		//current time
		float timer = 0.0f;

		//while we still have animation time
		while (timer <= time) {

			//lerp our position
			//transform.position = Vector3.Lerp (pos1, pos2, evalMove);
			transform.Translate(offset*Time.deltaTime, Space.World);
			//get the current color
			textMesh.color = Color.Lerp(textMesh.color, Color.clear, timer/time);

			//add time passed so far
			timer += Time.deltaTime;
			//wait till next frame
			yield return null;
		}
		
		//animation done, remove game object
		Destroy(gameObject);
	}

	public void setFloatText(string _text)
	{
		textForTextMesh = _text;
	}
}