using UnityEngine;
using System.Collections;

public class BuyButton : MonoBehaviour {

    public int cost;
    public bool meersmash, superCandy, saveMe;
    public bool coins;
    public GameObject buyCoinsPrompt;

    private GameManager gManager;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	public void OnPress () {
        //DEBUG FOR TESTING
        if(coins)
        {
            gManager.gold += cost;
            PlayerPrefs.SetInt("gold", gManager.gold);
            return;
        }
        if (gManager.gold >= cost)
        {
            if (meersmash)
            {
                gManager.meersmashes += 1;
                PlayerPrefs.SetInt("meersmashes", gManager.meersmashes);
            }
            else if (superCandy)
            {
                gManager.superCandies += 1;
                PlayerPrefs.SetInt("superCandies", gManager.superCandies);
            }
            else if (saveMe)
            {
                gManager.saveMes += 1;
                PlayerPrefs.SetInt("saveMes", gManager.saveMes);
            }

            gManager.gold -= cost;
            PlayerPrefs.SetInt("gold", gManager.gold);
        }
        else
            buyCoinsPrompt.SetActive(true);
	}
}
