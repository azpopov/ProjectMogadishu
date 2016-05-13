using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventSystem : MonoBehaviour {

    public static int head = 0;

    static int MAX_PENDING = 10;
    public static int tail;



    static int[] pending = new int[MAX_PENDING];
    static object[][] pending_data = new object[MAX_PENDING][];
	public static List<TradeMission> pendingMissions;

	public static GameObject eventPresent;

    static Dictionary<string, int> eventDic;
	public string[] eventsLoaded;
    public GameObject[] events; 

    void Awake()
    {
		pendingMissions = new List<TradeMission> ();
        eventPresent = null;
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
        if (eventPresent == null && head != tail)
        {
			eventPresent = CreateEvent(pending[head], pending_data[head]);
			head = (head + 1) % MAX_PENDING;
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
        OccurEvent(eventID, p_data);
    }

    static void OccurEvent(int eventID, params object[] p_data)
    {
//		for (int i = head; i != tail; i = (i + 1) % MAX_PENDING)
//			if (pending[i] == eventID) return;
       if (tail >= MAX_PENDING) return;
       pending[tail] = eventID;
       if (p_data != null) pending_data[tail] = p_data;
		tail = (tail + 1) % MAX_PENDING;
    }

    public string RandomTravelEvent()
    {
        int i = 0;
        var newDic = eventDic.Keys;

        while (i < 1000)
        {
            int rndNum = Random.Range(0, events.Length);
            if (events[rndNum].name.Contains("TravelEvent")) return events[rndNum].name;
            i++;
        }
        return "none loaded";
    }
}
