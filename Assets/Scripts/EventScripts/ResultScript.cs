using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScript : CustomEvent {
    ResourceBundle result;
	// Use this for initialization
	void Start () {
        result = (ResourceBundle)data[0];
        bool state = (bool)data[1];
        string newContent;
        foreach(Transform child in transform)
        {
            if(child.name.Equals("ResultText")){
                newContent = child.GetComponent<Text>().text;
                if(state)
                    newContent = newContent.Replace("[result]", "Favorable outcome!");
                else
                    newContent = newContent.Replace("[result]", "Unfavorable outcome..");
                child.GetComponent<Text>().text = newContent;
            }
            if (child.name.Equals("ContentText"))
            {
                string finalString = "";
                string holderString = child.GetComponent<Text>().text;
                if (state)
                    holderString = holderString.Replace("[impact]", "gained");
                else
                    holderString = holderString.Replace("[impact]", "lost");
                if (result.commodity != 0)
                {
                    newContent = holderString;
                    newContent = newContent.Replace("[amount]", Mathf.Abs(result.commodity).ToString());
                    newContent = newContent.Replace("[type]", "commodities");
                    finalString += newContent + "\n";
                }
                if (result.luxury != 0)
                {
                    newContent = holderString;
                    newContent = newContent.Replace("[amount]", Mathf.Abs(result.luxury).ToString());
                    newContent = newContent.Replace("[type]", "luxuries");
                    finalString += newContent + "\n";
                }
                if (result.wealth != 0)
                {
                    newContent = holderString;
                    newContent = newContent.Replace("[amount]", Mathf.Abs(result.wealth).ToString());
                    newContent = newContent.Replace("[type]", "wealth");
                    finalString += newContent + "\n";
                }
                if (finalString.Equals("") && state) finalString = "Sometimes, no change is good.";
                if (finalString.Equals("") && !state) finalString = "It's a shame";
               
                child.GetComponent<Text>().text = finalString;

            }

        }

        
	}
	


	// Update is called once per frame
	void Update () {
	
	}
}
