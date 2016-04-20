using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventSystem : MonoBehaviour {

    int head = 0;

    static int MAX_PENDING = 10;
    public static int numPending;



    static int[] pending = new int[MAX_PENDING];

	public static bool eventPresent;

    static Dictionary<string, int> eventDic;

    public GameObject[] events; 
    void Awake()
    {
        eventPresent = false;
        numPending = 0;
        head = 0;
        eventDic = new Dictionary<string, int>();
		int i = 0;
		foreach (GameObject _event in events) {
			eventDic.Add(_event.name, i);
			i++;
		}
    }   
    
    // Use this for initialization
	void Start () {
	   
	}
	// Update is called once per frame
	void Update () {
        if (!eventPresent && numPending > 0)
        {
			CreateEvent(pending[head]);
            numPending--;
            eventPresent = true;
			head = (head + 1) % MAX_PENDING;
        }
    
    }

    GameObject CreateEvent(int eventID)
    {
        GameObject uiEvent = Instantiate(events[eventID], new Vector3(0, 0), Quaternion.identity) as GameObject;
        uiEvent.transform.SetParent(GameObject.Find("UI").transform, false);
        uiEvent.SetActive(true);
        return uiEvent;
    }
    public static void OccurEvent(string eventName)
    {
        int eventID = eventDic[eventName];
        OccurEvent(eventID);
    }
   public static void OccurEvent(int eventID) {
       if (numPending >= MAX_PENDING) return;
       pending[numPending] = eventID;
       numPending++;
    }
}
