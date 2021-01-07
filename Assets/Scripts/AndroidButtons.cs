using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AndroidButtons : MonoBehaviour {

    public GameObject menuObjectToLoad;
    public GameObject runningObjectToLoad;
    public GameObject tutorialObjectToLoad;
    public static AndroidButtons instance = null;
    
    private string sceneName;

    // Use this for initialization
    void Start () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sceneName == "Menu")
                menuObjectToLoad.SetActive(true);
            else if (sceneName == "Running")
            {
                FindObjectOfType<Player>().Pause();
                runningObjectToLoad.SetActive(true);
            }
            /* else if (sceneName == "Tutorial")
            {
                FindObjectOfType<Player>().Pause();
                tutorialObjectToLoad.SetActive(true);
            }
            */
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
