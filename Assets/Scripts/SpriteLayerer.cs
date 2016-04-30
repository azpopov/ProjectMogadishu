using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteLayerer : MonoBehaviour {
		
	public Collider2D col2D;
	public BuildingPlacer buildPlacer;

	List<SpriteRenderer> conflictSprRnd = new List<SpriteRenderer>();
	void Awake()
	{
		col2D = GetComponent<Collider2D> ();
		buildPlacer = GetComponent<BuildingPlacer> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		int n = conflictSprRnd.Count;
//
//		while(true)
//		{
//			bool swapped = false;
//			for(int i = 1; i < n-1; i++)
//			{
//				if(col2D.transform.position > conflictSprRnd[i].transform.position.y)
//				{
//					col2D.gameObject.GetComponent<SpriteRenderer>().sortingOrder++;
//					conflictSprRnd[i].sortingOrder--;
//
//				}
//			}
//		}
		
	}

//	void OnTriggerEnter2D(Collider2D _col){
//		if (buildPlacer != null && !conflictSprRnd.Contains(_col)) {
//			conflictSprRnd.Add(_col);
//		}
//		return;
//	}
//
//	void OnTriggerExit2D(Collider2D _col){
//		if (buildPlacer != null && !conflictSprRnd.Contains(_col)) {
//			conflictSprRnd.Remove(_col);
//		}
//		return;
//	}
}
