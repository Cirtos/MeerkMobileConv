using UnityEngine;
using System.Collections;

public class Meertraveller : MonoBehaviour {

    private UIManager uiManager;
	// Use this for initialization
	void Start () {
        uiManager = FindObjectOfType<UIManager>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) {
        uiManager.ShowAchievement("dist");
	}
}
