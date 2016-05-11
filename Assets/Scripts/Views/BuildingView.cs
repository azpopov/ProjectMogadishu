using UnityEngine;
using System.Collections;

public class BuildingView : MonoBehaviour {
    protected virtual int GlowSprite
    {
        set;
        get;
    }
    //Variables for switching the Sprite between empty -> produced
    internal SpriteRenderer spriteRnd;
    internal Sprite defaultSprite;
    public Sprite[] glowSprite;


    void Awake()
    {
        spriteRnd = GetComponent<SpriteRenderer>();

        if (spriteRnd != null)
            defaultSprite = spriteRnd.sprite;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
