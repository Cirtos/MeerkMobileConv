using UnityEngine;
using System.Collections;

public class DistanceMarker : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.tutorial)
            return;
		transform.Translate (-transform.up * player.currentSpeed * Time.deltaTime);
	}
}
