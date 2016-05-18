using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryEventInterestScript : CustomEvent {


    public override void Awake()
    {
        base.Awake();
        foreach(Transform child in transform)
            if (child.name.Equals("ContentText"))
            {
                string customText = child.GetComponent<Text>().text;
                customText = customText.Replace("[amount]", (app.controller.story.remainingDebt - (app.controller.story.remainingDebt / 1.1f)).ToString());
                customText = customText.Replace("[maxamount]", (app.controller.story.maxDebt.ToString()));
                child.GetComponent<Text>().text = customText;
            }

    }
}
