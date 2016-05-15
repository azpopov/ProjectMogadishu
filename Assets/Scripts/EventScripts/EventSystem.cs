using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventSystem : MonoBehaviour {

    public static int head = 0;
    public static int headPriority = 0;
    static int MAX_PENDING = 10;
    static int MAX_PENDING_PRIORITY = 5;
    public static int tail;
    public static int tailPriority;



    static int[] pending = new int[MAX_PENDING];
    static object[][] pending_data = new object[MAX_PENDING][];
    static int[] pending_priority = new int[MAX_PENDING_PRIORITY];
    static object[][] pending_priority_data = new object[MAX_PENDING_PRIORITY][];

	public static GameObject eventPresent;

    static Dictionary<string, int> eventDic;
	static GameObject[] eventsLoaded;
    public GameObject[] events; 

    void Awake()
    {
        eventPresent = null;
        tail = 0;
        tailPriority = 0;
        eventDic = new Dictionary<string, int>();
		int i = 0;
		eventsLoaded = new GameObject[events.Length];
		foreach (GameObject _event in events) {
			eventDic.Add(_event.name, i);
			eventsLoaded[i] = _event;
			i++;
		}
    }   
    
    // Use this for initialization
	void Start () {
	   
	}
	// Update is called once per frame
	void Update () {
        if (eventPresent == null)
        {
            if (headPriority != tailPriority)
            {
                eventPresent = CreateEvent(pending_priority[headPriority], pending_priority_data[headPriority]);
                headPriority = (headPriority + 1) % MAX_PENDING_PRIORITY;
            }
            else if (head != tail)
            {
                eventPresent = CreateEvent(pending[head], pending_data[head]);
                head = (head + 1) % MAX_PENDING;
            }
        }
    }

    GameObject CreateEvent(int eventID, params object[] p_data)
    {
        GameObject uiEvent = Instantiate(events[eventID], new Vector3(0, 0), Quaternion.identity) as GameObject;
		uiEvent.transform.SetParent (GameObject.Find ("UI").transform, false);
        if (p_data != null) uiEvent.GetComponent<CustomEvent>().data = p_data;
        uiEvent.SetActive(true);
        return uiEvent;
    }
    public static void OccurEvent(string eventName, params object[] p_data)
    {
        int eventID = eventDic[eventName];
        if (eventName.Equals("ResultPrefab"))
        {
            OccurEvent(eventID,true, p_data );
            return;
        }
        OccurEvent(eventID,false, p_data);
    }

    static void OccurEvent(int eventID,  bool priority, params object[] p_data)
    {
        if (priority)
        {
            if (tailPriority >= MAX_PENDING_PRIORITY) return;
            pending_priority[tailPriority] = eventID;
            if (p_data != null) pending_priority_data[tailPriority] = p_data;
            tailPriority = (tailPriority + 1) % MAX_PENDING_PRIORITY;
            return;
        }
       if (tail >= MAX_PENDING) return;
       pending[tail] = eventID;
       if (p_data != null) pending_data[tail] = p_data;
		tail = (tail + 1) % MAX_PENDING;
    }

    public static string RandomTravelEvent()
    {
        int i = 0;
        while (i < 1000)
        {
            int rndNum = Random.Range(0, eventsLoaded.Length);
            if (eventsLoaded[rndNum].name.Contains("TravelEvent")) return eventsLoaded[rndNum].name;
            i++;
        }
        return "none loaded";
    }
}
