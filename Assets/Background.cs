using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public Sprite day, night;
    private SpriteRenderer sprite;
    private float hour;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        hour = System.DateTime.Now.Hour;

        if (hour >= 18 || hour <= 5)
            sprite.sprite = night;
        else
            sprite.sprite = day;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
