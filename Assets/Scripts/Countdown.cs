using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

	public float countdownTime;
	public Text countdownText;
	public GameObject toDisable;
	public GameObject toEnable;
	public bool isResumeTimer;
	public bool justTimeout;
    public bool noText;

	private float countdownTimer;
	private Player player;

	// Use this for initialization
	void OnEnable () {
		countdownTimer = countdownTime;
		player = FindObjectOfType <Player> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (justTimeout) 
		{
			countdownTimer -= Time.deltaTime;
			if (countdownTimer <= 0) 
			{
				toDisable.SetActive (false);
			}
			return;
		}

		countdownTimer -= Time.deltaTime;
        if(!noText)
		    countdownText.text = Mathf.CeilToInt(countdownTimer).ToString();

		if (countdownTimer <= 0 && !isResumeTimer) 
		{
			toEnable.SetActive (true);
			toDisable.SetActive (false);
		}

		if (isResumeTimer) 
		{
			player.paused = true;
			if (countdownTimer <= 0) 
			{
				toEnable.SetActive (true);
				player.StartRunning ();
				countdownTimer = countdownTime;
				gameObject.SetActive (false);
			}
		}
	}
}
