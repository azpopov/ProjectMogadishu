using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverEventScript : CustomEvent {
    Text contentText, titleText;
    string customText;
    string reasonForLoss;
    public override void Awake()
    {
        base.Awake();
       
    }
    void Start()
    {
        string customText;
        foreach (Transform child in transform)
            if (child.name == "ContentText")
            {
                contentText = child.GetComponent<Text>();
            }
            else if (child.name == "TitleText")
                titleText = child.GetComponent<Text>();
        reasonForLoss = (string)data[0];
        customText = titleText.text;
        switch (reasonForLoss)
        {
            case "debt":
                customText = customText.Replace("[reason]", "Game Over - Debt");
                titleText.text = customText;
                customText = contentText.text;
                customText = "As our debt has reached critically high levels, the moneylenders have returned to seize the city port. \nNothing could be done as they come with an army..";
                return;
            case "monetary":
                customText = customText.Replace("[reason]", "Victory - Monetary");
                titleText.text = customText;
                customText = "Victory! We have established a highly respected city and our trade relations will ensure our safety and our continuous growth and prosperity. \nHuzzah!";
                customText = contentText.text;
                return;
            case "vascoAnger":
                customText = customText.Replace("[reason]", "Game Over - Hostile Takeover");
                titleText.text = customText;
                customText = "PLACEHOLDER";
                customText = contentText.text;
                return;
        }
        contentText.text = customText;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        Application.LoadLevel("Main Menu");
    }

}
