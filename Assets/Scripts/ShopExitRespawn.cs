using UnityEngine;
using System.Collections;

public class ShopExitRespawn : MonoBehaviour {

    public GameObject shopWindow, deathPanel, finalScore, tutDeath;

    private GameManager gManager;
    private Player player;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
	}

    // Update is called once per frame
    public void OnPress()
    {
        shopWindow.SetActive(false);

        if (player.paused)
            return;
        if (gManager.firstTime)
        {
            tutDeath.SetActive(true);
            return;
        }
        if (gManager.saveMes >= 1)
        {
            deathPanel.SetActive(true);
        }
        else
            finalScore.SetActive(true);
    }
}
