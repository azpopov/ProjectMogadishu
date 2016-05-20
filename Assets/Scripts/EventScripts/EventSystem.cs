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

    static System.Random rndGen = new System.Random();

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
        //Checks if an event is on screen
        eventPresent = null;
        tail = 0; //tail of the queue
        tailPriority = 0; // trail of the priorityqueue which is used for result events
        eventDic = new Dictionary<string, int>(); //Gives easy access to events by their index
        int i = 0;
		eventsLoaded = new GameObject[events.Length];
		foreach (GameObject _event in events) {
			eventDic.Add(_event.name, i);
			eventsLoaded[i] = _event;
			i++;
		}
    }   

	// Update is called once per frame
	void Update () {
        if (eventPresent == null) // if no event present
        {
            //priority queue fires first
            if (headPriority != tailPriority) //if tail hasn't reached head
            {
                //ensures no 2 events can play at the same time
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
    //returns the gameobject of the event just instantiated and passes the data to it
    GameObject CreateEvent(int eventID, params object[] p_data)
    {
        //Creates an instance of the vent on scree
        GameObject uiEvent = Instantiate(events[eventID], new Vector3(0, 0), Quaternion.identity) as GameObject;
		//Sets the hierarchichal parent of the event to the UI to ensure it's in the correct position
        uiEvent.transform.SetParent (GameObject.Find ("UI").transform, false);
        // if there is any data in p_data pass it into the event
        if (p_data != null) uiEvent.GetComponent<CustomEvent>().data = p_data;
        //show the event on screen
        uiEvent.SetActive(true);
        return uiEvent;
    }
    //Allows to call events by their string names
    public static void OccurEvent(string eventName, params object[] p_data)
    {
        //get the event from the dictionarty
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

    public static string RandomTravelEvent(string eventName)
    {
        int i = 0;
        while (i < 1000)
        {
            int rndNum = rndGen.Next(eventsLoaded.Length);
            if (eventsLoaded[rndNum].name.Contains(eventName)) return eventsLoaded[rndNum].name;
            i++;
        }
        return "none loaded";
    }
}
