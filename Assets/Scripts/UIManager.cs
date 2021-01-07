using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public Text distanceText, scoreText, coinText, finalScoreText, highScoreText, finalCoinText;
	public GameObject distanceMarker, deathFirstPanel, tutDeathPanel, starvedPanel, powerUpBar;
	public Image lifeBar,powerBar;
    public bool redFlashBool, powerUp;
    public Image redFlash;
    public GameObject meersmashes, superCandies, tutMeersmashes;
    public Text meersmashCount, superCandyCount, tutMeersmashCount;
    public GameObject noSaves;
    public GameObject candyAchi, smashAchi, distAchi;
    public GameObject audSource;
    public Camera snapshotCam;
    public GameObject pauseButton;

    private Player player;
	private GameManager gManager;
    private float redFlashTime = 1;
    private float currentRedFlash;
    private Color co = Color.red;
    private Color orig;
    private string sceneName;

    // Use this for initialization
    void Start () {
		player = FindObjectOfType<Player>();
		gManager = FindObjectOfType<GameManager> ();
        currentRedFlash = redFlashTime;
        powerUpBar.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        co.a = 0;
        orig = redFlash.color;
    }
	
	// Update is called once per frame
	void Update () {

        if (sceneName != "Tutorial")
        {
            distanceText.text = ((player.gameObject.transform.position.y - distanceMarker.transform.position.y) / 2).ToString("F2");
            coinText.text = player.coinsThisRun.ToString();
            scoreText.text = player.finalScore.ToString("F2");
            finalScoreText.text = player.finalScore.ToString("F2");
            finalCoinText.text = player.coinsThisRun.ToString();
            highScoreText.text = gManager.highScore.ToString("F2");
        }
            lifeBar.fillAmount = (player.timeRemaining / player.timeTilDie);
        

            if (redFlashBool)
        {
            redFlash.enabled = true;
            audSource.SetActive(true);
            RedFlash();
        }
        else
        {
            redFlash.enabled = false;
            audSource.SetActive(false);
        }

        if (sceneName != "Tutorial")
        {
            if (powerUp)
            {
                powerUpBar.SetActive(true);
                powerBar.fillAmount = (player.currentPowerUp / player.powerUpDuration);
            }
            else
                powerUpBar.SetActive(false);

            if (gManager.meersmashes >= 1 && !player.tutorial)
            {
                meersmashes.SetActive(true);
                meersmashCount.text = gManager.meersmashes.ToString();
            }
            else
                meersmashes.SetActive(false);

            if (player.tutorial && gManager.tutMeersmashes >= 1)
            {
                tutMeersmashes.SetActive(true);
                tutMeersmashCount.text = gManager.tutMeersmashes.ToString();
            }
            else
                tutMeersmashes.SetActive(false);

            if (gManager.superCandies >= 1 && !player.tutorial)
            {
                superCandies.SetActive(true);
                superCandyCount.text = gManager.superCandies.ToString();
            }
            else
                superCandies.SetActive(false);

            if (player.tutorial)
            {
                scoreText.text = "";
                pauseButton.SetActive(false);
            }
            else
                pauseButton.SetActive(true);
        }
    }

	public IEnumerator HandleDeath()
	{
        yield return new WaitForSeconds(3);

        snapshotCam.Render();

        if (!player.notStarved)
            starvedPanel.SetActive(true);
        else if (player.tutorial || sceneName != "Running")
            tutDeathPanel.SetActive(true);
        else
            deathFirstPanel.SetActive(true);
		StopCoroutine (HandleDeath ());
	}

    public void RedFlash()
    {
        currentRedFlash -= Time.deltaTime;
        if (currentRedFlash <= 0)
        {
            currentRedFlash = redFlashTime;
            redFlash.color = orig;
        }
        redFlash.color = Color.LerpUnclamped(co, orig, currentRedFlash);
    }

    public void SaveMe()
    {
        if (gManager.saveMes >= 1)
        {
            gManager.saveMes -= 1;
            player.Respawn();
            deathFirstPanel.SetActive(false);
        }
        else
        {
            noSaves.SetActive(true);
            deathFirstPanel.SetActive(false);
        }
    }

    public void ShowAchievement(string name)
    {
        if (name == "candy")
            candyAchi.SetActive(true);
        else if (name == "smash")
            smashAchi.SetActive(true);
        else if (name == "dist")
            distAchi.SetActive(true);
    }
}