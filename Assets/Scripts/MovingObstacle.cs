using UnityEngine;
using System.Collections;

public class MovingObstacle : MonoBehaviour {

	public float speed;
    public bool secondSpawn;
    public bool meerSmashed;
    public Sprite smashed;

	private Animator anim;
	private Vector3 repeatReturn = new Vector3(13,0, 0);
	private Player player;
	private SpriteRenderer rend;
    private bool front;
	private float bounds;
    private float speedIncrease;

	// Use this for initialization
	void Start () {
		//slightly offset to randomize
		anim = GetComponent<Animator> ();
		rend = GetComponent<SpriteRenderer> ();
		player = FindObjectOfType<Player> ();

		bounds = (rend.bounds.size.y / 2);

		rend.sortingOrder = -Mathf.RoundToInt((transform.position.y - bounds) * 10);
        speedIncrease = (player.gameSpeed - player.basespeed / 2);
	}
	
	// FixedUpdate is called during physics updates
	void Update() {

		if (player.paused)
		{
			anim.enabled = false;
			return;
		}

		anim.enabled = true;

        if (!meerSmashed)
        {
            //move left at speed divided by time to render last frame
            transform.Translate(-transform.right * (speed + speedIncrease) * Time.deltaTime);
            transform.Translate(-transform.up * player.currentSpeed * Time.deltaTime);
        }
        else
        {
            speed = 0;
            anim.enabled = false;
            rend.sprite = smashed;
        }

        //If offscreen
        if (transform.position.x <= -4.5f)
        {
            if (meerSmashed)
                Destroy(gameObject);
            else
                transform.position += repeatReturn;
        }
        else if (transform.position.x >= 4.5f)
        {
            if (meerSmashed)
                Destroy(gameObject);
            else
                transform.position -= repeatReturn;
        }

        //Move in front of player layer once passed
        if (!front)
        {
            if (transform.position.y < player.gameObject.transform.position.y)
            {
                ToFront();
            }
        }

        //If off bottom
		if (transform.position.y <= -15f)
			Destroy (gameObject);
	}

    void ToFront()
    {
        if (!secondSpawn)
        {
            if (!player.tutorial)
                player.SpeedUp();
        }
		rend.sortingOrder += 800;
        front = true;
    }
}
