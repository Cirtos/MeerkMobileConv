using UnityEngine;
using System.Collections;

public class AnimatedButton : MonoBehaviour {

    public bool thingsToActive, thingsToDeactivate;
    public GameObject toActivate, toDeactivate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void AnimationEnd () {
        if (thingsToActive)
            toActivate.SetActive(true);
        if (thingsToDeactivate)
            toDeactivate.SetActive(true);
	}
}
