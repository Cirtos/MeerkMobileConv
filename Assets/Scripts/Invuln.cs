using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Invuln : MonoBehaviour {

	public bool invul;

	private Player player;
	private Image button;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType <Player> ();
		button = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		Color color;
		if (invul) {
			player.timeRemaining = player.timeTilDie;
			color = Color.red;
			button.color = color;
		} else {
			color = Color.white;
			button.color = color;
		}
	}

	public void OnClick() {
		
		if (invul)
			invul = false;
		else if (!invul) 
			invul = true;
	}
}
