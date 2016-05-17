using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverEventScript : CustomEvent {
    Text contentText, titleText;
    string customText;
    public override void Awake()
    {
        base.Awake();
        disableButton.onClick.AddListener(() => Application.LoadLevel("Main Menu"));
        string customText;
        foreach (Transform child in transform)
            if (child.name == "ContentText")
            {
                contentText = child.GetComponent<Text>();
            }
            else if (child.name == "TitleText")
                titleText = child.GetComponent<Text>();
        string reasonForLoss = (string)data[0];
        switch (reasonForLoss)
        {
            case "debt":
                customText = titleText.text;
                customText = customText.Replace("[reason]","");
                titleText.text = customText;
                customText = contentText.text;
                contentText.text = customText;
                return;
        }
    }

    
}
