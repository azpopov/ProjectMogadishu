using UnityEngine;
using System.Collections;

public class GameElement : MonoBehaviour
{
	public Game app { get { return GameObject.FindObjectOfType<Game>(); }}
    public virtual void OnNotification(string p_event_path, System.Object p_target, params object[] p_data)
    {

    }
}

