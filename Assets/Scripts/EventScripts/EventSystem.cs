using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventSystem : MonoBehaviour {

    static int head = 0;

    static int MAX_PENDING = 10;
    public static int tail;



    static int[] pending = new int[MAX_PENDING];

	public static bool eventPresent;

    static Dictionary<string, int> eventDic;
	public string[] eventsLoaded;
    public GameObject[] events; 
    void Awake()
    {
        eventPresent = false;
        tail = 0;
        eventDic = new Dictionary<string, int>();
		int i = 0;
		eventsLoaded = new string[events.Length];
		foreach (GameObject _event in events) {
			eventDic.Add(_event.name, i);
			eventsLoaded[i] = _event.name;
			i++;
		}
    }   
    
    // Use this for initialization
	void Start () {
	   
	}
	// Update is called once per frame
	void Update () {
        if (!eventPresent && head != tail)
        {
			CreateEvent(pending[head]);
            
            eventPresent = true;
			head = (head + 1) % MAX_PENDING;
        }
    }

    GameObject CreateEvent(int eventID)
    {
        GameObject uiEvent = Instantiate(events[eventID], new Vector3(0, 0), Quaternion.identity) as GameObject;
		uiEvent.transform.SetParent (GameObject.Find ("UI").transform, false);
        uiEvent.SetActive(true);
        return uiEvent;
    }
    public static void OccurEvent(string eventName)
    {
        int eventID = eventDic[eventName];
        OccurEvent(eventID);
    }

   public static void OccurEvent(int eventID) {
		for (int i = head; i != tail; i = (i + 1) % MAX_PENDING)
			if (pending[i] == eventID) return;
       if (tail >= MAX_PENDING) return;
       pending[tail] = eventID;
		tail = (tail + 1) % MAX_PENDING;
    }
}
