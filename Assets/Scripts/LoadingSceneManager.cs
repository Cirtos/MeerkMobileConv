using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    public Image backGround;
    public Sprite[] backGrounds;
    public GameObject[] sprites;
    public Image loadBar;
    public GameObject loadBG;
    public GameObject continueButton;
    public bool isSplash;
    public Text tipText;
    public string tip1;
    public string tip2;
    public string tip3;
    public string tip4;
    public string tip5;
    public string tip6;

    private string[] tips = new string[6];
    private bool loading;
    private GameManager gManager;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
        loading = true;
        tips[0] = tip1;
        tips[1] = tip2;
        tips[2] = tip3;
        tips[3] = tip4;
        tips[4] = tip5;
        tips[5] = tip6;
        if (!isSplash)
        {
            int bg = Random.Range(0, backGrounds.Length - 1);
            int spr = Random.Range(0, sprites.Length - 1);
            int tp = Random.Range(0, tips.Length - 1);

            backGround.sprite = backGrounds[bg];
            if (bg != backGrounds.Length - 1)
                sprites[spr].SetActive(true);


            tipText.text = tips[tp];
            tipText.text = tipText.text.Replace("\\n", "\n");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (loading)
        {
            if (loadBar.fillAmount <= 0.3)
                loadBar.fillAmount += ((1 * Time.deltaTime) / 3);
            else if (loadBar.fillAmount <= 0.5)
                loadBar.fillAmount += ((1 * Time.deltaTime) / Random.Range(4, 6));
            else if (loadBar.fillAmount <= 0.6)
                loadBar.fillAmount += ((1 * Time.deltaTime) / Random.Range(1,3));
            else if (loadBar.fillAmount < 1)
                loadBar.fillAmount += ((1 * Time.deltaTime) / Random.Range(1, 1.5f));
            else if (loadBar.fillAmount >= 1)
            {
                loading = false;
                continueButton.SetActive(true);
                loadBG.SetActive(false);
                loadBar.gameObject.SetActive(false);
            }
        }
	}

    public void ContinuePressed()
    {
        SceneManager.LoadScene(gManager.sceneToLoad);
    }
}
