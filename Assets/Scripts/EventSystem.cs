using UnityEngine;
using System.Collections;

public class EventSystem : MonoBehaviour {

    int head = 0;
    int tail = 0;

    static int MAX_PENDING = 10;
    static int numPending;
    string prefabName;

    void Awake()
    {
        numPending = 0;
    }   
    
    // Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   static void ShowEvent(GameObject _uiGameObject) {

    }
}
