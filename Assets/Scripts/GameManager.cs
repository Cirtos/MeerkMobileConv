using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    
	public float defaultSpeed = 3;
	public float highScore;
	public int gold;
    public int meersmashes, superCandies, saveMes, tutMeersmashes;
    public bool firstTime;
    public float sfxVolume, musVolume;

    public string sceneToLoad;
	public static GameManager instance = null;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);

		Application.targetFrameRate = 60;

		DontDestroyOnLoad(gameObject);

        gold = PlayerPrefs.GetInt("gold");
        meersmashes = PlayerPrefs.GetInt("meersmashes");
        superCandies = PlayerPrefs.GetInt("superCandies");
        saveMes = PlayerPrefs.GetInt("saveMes");
        firstTime = PlayerPrefs.GetInt("firstTime") == 1 ? false : true;

        if (!PlayerPrefs.HasKey("musVol"))
        {
            sfxVolume = 1;
            musVolume = 1;
            PlayerPrefs.SetFloat("sfxVol", 1);
            PlayerPrefs.SetFloat("musVol", 1);
        }
        else
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVol");
            musVolume = PlayerPrefs.GetFloat("musVol");
        }
    }

	// Use this for initialization
	void Start () {
		highScore = PlayerPrefs.GetFloat("HighScore");
		gold = PlayerPrefs.GetInt("Gold");
		//ints for powerup numbers here
	}
	
	// Update is called once per frame
	public void ClearData () {
        PlayerPrefs.DeleteAll();
	}

    public void LowerSFX()
    {
        if(sfxVolume > 0.2f)
        {
            sfxVolume -= .2f;
            PlayerPrefs.SetFloat("sfxVol", sfxVolume);
        }
    }

    public void RaiseSFX()
    {
        if (sfxVolume < 1)
        {
            sfxVolume += .2f;
            PlayerPrefs.SetFloat("sfxVol", sfxVolume);
        }
    }

    public void LowerMusic()
    {
        if (musVolume > 0.2f)
        {
            musVolume -= .2f;
            PlayerPrefs.SetFloat("musVol", musVolume);
        }
    }

    public void RaiseMusic()
    {
        if (musVolume < 1)
        {
            musVolume += .2f;
            PlayerPrefs.SetFloat("musVol", musVolume);
        }
    }
}
