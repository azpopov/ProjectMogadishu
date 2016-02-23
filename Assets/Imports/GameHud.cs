using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHud : MonoBehaviour {

	public static GameHud Instance { get; set; }

	public Text Score;

	public Text CenterText;
	private bool CenterTextON = false;
	private float CenterTextTime = 2f;


	private int TotalScore;

	void Awake() {
		Instance = this;
		CenterTextON = false;
		CenterText.gameObject.SetActive(false);
		Score.text = "Score: 000";
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(CenterTextON == true) {
			CenterTextTime -= Time.deltaTime;

			if(CenterTextTime <= 0) {
				HideCenterText();
			}

		}
	
	}

	public void TextOnSpot(string name, string text, float speed, Transform obj) {

		GameObject PointsText = Instantiate(Resources.Load("Prefabs/" + name)) as GameObject;
		
		if(PointsText.GetComponent<TextOnSpotScript>() != null) {
			
			var givePointsText = PointsText.GetComponent<TextOnSpotScript>();
			givePointsText.DisplayText = text;
			givePointsText.Speed = speed;
			
		}
		PointsText.transform.position = obj.transform.position;
	}

	public void AddPoints(int points) {
		TotalScore += points;

		if(TotalScore < 100 && TotalScore > 9) {
			Score.text = "Score: 0" + TotalScore;
		} else if(TotalScore < 10 && TotalScore > 0) {
			Score.text = "Score: 00" + TotalScore;
		} else if(TotalScore <= 0) {
			Score.text = "Score: 000" + TotalScore;
		}
	}

	public void ShowCenterText(string text, float time) {
		CenterText.text = text;
		CenterTextTime = time;
		CenterTextON = true;
		CenterText.gameObject.SetActive(true);
	}

	private void HideCenterText() {
		CenterText.gameObject.SetActive(false);
		CenterText.text = "";
		CenterTextTime = 0;
		CenterTextON = false;
	}
}
