using UnityEngine;
using System.Collections;

public class StaticObject : MonoBehaviour {

    public bool isPickup;

	private Player player;
	private SpriteRenderer rend;
	private float bounds;
    private bool front;
    private Transform shadow;
	private SpriteRenderer shadowRend;

	void Start() {
        if (!isPickup)
            rend = GetComponent<SpriteRenderer> ();
        else
            rend = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();

        player = FindObjectOfType<Player> ();

        if (isPickup)
        {
            shadow = gameObject.transform.GetChild(0);
			shadowRend = shadow.GetComponent<SpriteRenderer>();
            rend.sortingOrder = -Mathf.RoundToInt(shadow.transform.position.y * 10);
			shadowRend.sortingOrder = rend.sortingOrder;
		}
        else
        {
            bounds = (rend.bounds.size.y / 2) - (rend.bounds.size.y / 5);
            rend.sortingOrder = -Mathf.RoundToInt(transform.position.y * 10);
        }
    }

	void Update()
	{
        if (!front)
        {
            if ((transform.position.y - bounds) < player.gameObject.transform.position.y)
                ToFront();
        }
		
		if (transform.position.y <= -15)
			Destroy (gameObject);
	}

    void ToFront()
	{
		if (isPickup) 
		{
			shadow = gameObject.transform.GetChild(0);
			shadowRend = shadow.GetComponent<SpriteRenderer>();
			shadowRend.sortingOrder += 800;
		}
		rend.sortingOrder += 800;
        player.SpeedUp();
        front = true;
    }
}
