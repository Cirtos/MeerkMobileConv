using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour {

    public int tutorial;
    public GameObject[] tutorials;

    private GameManager gManager;
    private Player player;
    private string sceneName;


    // Use this for initialization
    void Start () {
        gManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (player.tutorial)
            DisplayNext();
        else if (sceneName == "Tutorial")
            DisplayNextTutLevel();
	}
	
	// Update is called once per frame
	public void DisplayNext () {
        print("displayNextCalled");
        if(tutorial != 10)
        tutorials[tutorial].SetActive(true);

        if (tutorial == 1)
            player.tutLeftBlock = false;
        else if (tutorial == 2)
            player.tutRightBlock = false;
        else if (tutorial == 7)
            gManager.tutMeersmashes += 1;


        tutorial++;

        if (tutorial == 11)
        {
            gManager.firstTime = false;
            player.tutorial = false;
            PlayerPrefs.SetInt("firstTime", 1);
        }
        // display tut complete, turn back on ui elements
	}

    public void DisplayNextTutLevel()
    {
        tutorials[tutorial].SetActive(true);
        if (tutorial == 1)
        {
            player.tutLeftBlock = false;
            player.tutRightBlock = true;
        }
        else if (tutorial == 2)
        {
            player.tutLeftBlock = true;
            player.tutRightBlock = false;
        }
        else if (tutorial == 3)
        {
            player.tutLeftBlock = false;
            player.tutRightBlock = false;
        }

        tutorial++;
    }

    public void ClearTut()
    {
        tutorials[tutorial-1].SetActive(false);
    }
}
