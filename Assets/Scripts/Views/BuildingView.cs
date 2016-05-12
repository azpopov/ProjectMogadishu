using UnityEngine;
using System.Collections;

public class BuildingView : GameElement {

    //Variables for switching the Sprite between empty -> produced
    internal SpriteRenderer spriteRnd;
    [SerializeField]
    internal Sprite defaultSprite;
    public Sprite glowSprite;


    void Awake()
    {
        spriteRnd = GetComponent<SpriteRenderer>();
        if (spriteRnd == null) 
            Debug.Log("Sprite Renderer Not Found");
        defaultSprite = spriteRnd.sprite;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetDefaultSprite()
    {
        spriteRnd.sprite = defaultSprite;
    }

    public void SetGlowSprite()
    {
        spriteRnd.sprite = glowSprite;
    }
}
