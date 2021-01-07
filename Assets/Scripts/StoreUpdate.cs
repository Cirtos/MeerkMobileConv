using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreUpdate : MonoBehaviour {

    public bool isMeersmash, isSuperCandy, isSaveMe;

    private GameManager gManager;
    private Text text;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isMeersmash)
            text.text = "You have: " + gManager.meersmashes.ToString();
        else if (isSuperCandy)
            text.text = "You have: " + gManager.superCandies.ToString();
        else if (isSaveMe)
            text.text = "You have: " + gManager.saveMes.ToString();
    }
}
