using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextOnSpotScript : MonoBehaviour {

	public string DisplayText;
	public Text TextPrefab;
	public float Speed;
	public float DestroyAfter;
	private float Timer;

	// Use this for initialization
	void Start () {
		Timer = DestroyAfter;
		TextPrefab = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
		if(Timer < 0) {
			Destroy(gameObject);
		}

		if(DisplayText != null) {
			TextPrefab.text = DisplayText;
		}

		if(Speed > 0) {
			transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
		}
	}
}
