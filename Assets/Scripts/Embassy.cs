using UnityEngine;
using System.Collections;

public class Embassy : Building
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	protected override void CheckProduction ()
	{
		throw new System.NotImplementedException ();
	}

	protected override int GlowSprite {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
		}
	}

	protected override void OnDisable ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void OnEnable ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void OnMouseDown ()
	{
		throw new System.NotImplementedException ();
	}

	public override void ProductionTick ()
	{
		throw new System.NotImplementedException ();
	}
}
