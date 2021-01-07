using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinUpdate : MonoBehaviour {

    private GameManager gManager;
    private Text text;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
            text.text = gManager.gold.ToString();
	}
}
