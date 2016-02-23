using UnityEngine;
using System.Collections;

public class MoveMe : MonoBehaviour {

	public float speed = 1f;
	public float Mdistance = 1f;
	private bool switchdir = false;
	private Vector3 pos;
	private Vector3 orgPos;

	void Start() {
		pos = transform.position;
		orgPos = transform.position;
	}

	void Update () {
		Move ();
	}

	void Move() {
		if(switchdir == false) {

			transform.Translate(-Vector3.right * speed * Time.deltaTime, Space.World);

			pos = transform.position;
			if(pos.x <= orgPos.x-Mdistance) {
				switchdir = true;
			}
		} else {
			transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

			pos = transform.position;
			if(pos.x >= orgPos.x+Mdistance) {
				switchdir = false;
			}
		}
	}
}
