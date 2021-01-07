using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour {

	//Name of level to be loaded on press, set in inspector
	public string levelToLoad;
    public bool showLoadingScene;

    private GameManager gManager;

    void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }

	public void OnClick()
	{
        if (showLoadingScene)
        {
            gManager.sceneToLoad = levelToLoad;
            SceneManager.LoadScene("Loading");
        }
        else
		    SceneManager.LoadScene (levelToLoad);
	}
}
