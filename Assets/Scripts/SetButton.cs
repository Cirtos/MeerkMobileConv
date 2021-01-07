using UnityEngine;
using System.Collections;

public class SetButton : MonoBehaviour {

    private AndroidButtons aBut;

	// Use this for initialization
	void Start () {
        aBut = FindObjectOfType<AndroidButtons>();
        aBut.runningObjectToLoad = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
