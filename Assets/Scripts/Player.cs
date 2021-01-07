using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    
	public bool blocked;
    public float gameSpeed;
    public float currentSpeed;
    public float storedGameSpeed;
    public float storedCurrentSpeed;
    public float speedIncrease;
	public GameObject distanceMarker;
	public float timeTilDie;
	public float timeRemaining;
    public float SuperCandyDuration, MeersmashDuration, powerUpDuration, currentPowerUp;
    public bool MeersmashActive;
	public float finalScore;
	public int coinsThisRun;
	public bool paused;
	public AudioClip candy, coin, enemyCollide, stars, mysteryBox, smashCollision;
	public GameObject[] mBoxPopups;
    public float basespeed;
    public bool flashing;
    public bool tutorial, tutLeftBlock, tutRightBlock;
    public Tutorial tut;
    public GameObject particle; // starParts;
    public GameObject headStars;
    public float particleFreq;
    public bool notStarved;
    public bool playerLeftFromTut, playerRightFromTut;

    private float curParticleTime;
    private bool powerUpActive;
    private bool running;
	private GameManager gManager;
	private UIManager uiManager;
    private EmitterManager emiManager;
	private Animator anim;
    private SpriteRenderer sprite;
	private Invuln invul;
    private bool invincible;
	private AudioSource audSource;
    private bool invis;
    private float currentFlashTime, flashInterval = 0.25f, currentFlashInterval;
    private int smashCollisions, candyMunched;
    private string sceneName;

    void Awake ()
    {
        gManager = FindObjectOfType<GameManager>();
        tutorial = gManager.firstTime;
    }

    void Start () {
		// Set references
		uiManager = FindObjectOfType<UIManager> ();
        emiManager = FindObjectOfType<EmitterManager>();
		anim = GetComponent<Animator> ();
		invul = FindObjectOfType<Invuln> ();
		audSource = GetComponent <AudioSource> ();
        sprite = GetComponent<SpriteRenderer>();
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        //Check for tutorial
        if (tutorial || sceneName != "Running")
        {
            tut = FindObjectOfType<Tutorial>();
            tutLeftBlock = true;
            tutRightBlock = true;
        }

        //Set up scores/timers
		timeRemaining = timeTilDie;
		coinsThisRun = 0;
        currentSpeed = 0;
        basespeed = gameSpeed;
        smashCollisions = 0;
        candyMunched = 0;
        curParticleTime = particleFreq;
	}
	
	// Update is called once per frame
	void Update () {

		if (paused) 
		{
			anim.SetBool ("Running", false);
			currentSpeed = 0;
			return;
		}


        if (running)
            {
                //if the screens being touched
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0); // get the first touch
                                                     //if it's on the left side and not past the edge
                    if (touch.position.x < Screen.width / 2 && transform.position.x > -2.33)
                    {
                        if (tutLeftBlock)
                            return;
                        Dodging(-1);
                    }
                    else if (touch.position.x > Screen.width / 2 && transform.position.x < 2.33)
                    {
                        if (tutRightBlock)
                            return;
                        Dodging(1);
                    }
                }
                else if (playerLeftFromTut)
                {
                    Dodging(-1);
                }
                else if (playerRightFromTut)
                {
                    Dodging(1);
                }

            // Debug settings, allowing mouse controls
            else if (Input.GetMouseButton(0))
                {
                    if (Input.mousePosition.x < Screen.width / 2 && transform.position.x > -2.33 && !tutLeftBlock)
                    {
                        Dodging(-1);
                    }
                    else if (Input.mousePosition.x > Screen.width / 2 && transform.position.x < 2.33 && !tutRightBlock)
                    {
                        Dodging(1);
                    }
                }
                else if (blocked)
                {
                    currentSpeed = 0;
                    anim.SetBool("Running", false);
                }
                else
                {
                    currentSpeed = gameSpeed;
                    anim.SetFloat("RunDir", 0);
                    anim.SetBool("Running", true);
                }

                //distance = ((transform.position.y - distanceMarker.transform.position.y) / 2);
                if (!powerUpActive)
                {
                    if (tutorial || sceneName == "Tutorial")
                        timeRemaining -= (Time.deltaTime / 2);
                    else
                        timeRemaining -= Time.deltaTime;

                    if (timeRemaining <= (timeTilDie / 4))
                    {
                        uiManager.redFlashBool = true;
                        if (timeRemaining <= 0)
                        {
                            Starve();
                        }
                    }
                    else if (uiManager.redFlashBool)
                        uiManager.redFlashBool = false;
                }
                else
                {
                    currentPowerUp -= Time.deltaTime;
                    uiManager.powerUp = true;

                    if (currentPowerUp <= 3 && !flashing)
                        StartFlashing(3);

                    if (currentPowerUp <= 0)
                    {
                        uiManager.powerUp = false;
                        MeersmashActive = false;
                        powerUpActive = false;
                        anim.SetBool("Smash", false);
                    }
                }

                if (flashing)
                {
                    currentFlashInterval -= Time.deltaTime;
                    currentFlashTime -= Time.deltaTime;
                    if (currentFlashInterval <= 0)
                    {
                        if (invis)
                        {
                            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 255);
                            invis = false;
                        }
                        else
                        {
                            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 0);
                            invis = true;
                        }
                        currentFlashInterval = flashInterval;
                    }

                    if (currentFlashTime <= 0)
                    {
                        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 255);
                        flashing = false;
                        if (invincible)
                            invincible = false;
                    }
                }

                curParticleTime -= Time.deltaTime;
                if (curParticleTime <= 0)
                {
                    Instantiate(particle, transform.position, Quaternion.identity);
                    curParticleTime = particleFreq;
                }
            }
	}

    //When moving left/right
	void Dodging (float direction) {
        //Stop scroll
        currentSpeed = 0;
        //Tell animator which direction to run
        anim.SetFloat ("RunDir", direction);
		anim.SetBool ("Running", true);
		//gManager.runSpeed = 0;
		Vector2 dir = new Vector2(direction, 0);
		transform.Translate(dir * (gameSpeed/1.6f) * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (running) {
            //If we hit anything
            if (col.gameObject.tag == "Block")
                blocked = true;
            else if (col.gameObject.tag == "Candy")
            {
                audSource.PlayOneShot(candy);
                timeRemaining = timeTilDie;
                candyMunched++;
                Destroy(col.gameObject);
            }
            else if (col.gameObject.tag == "Enemy")
            {
                if (invincible)
                    return;
                //Debug Invul
                else if (invul != null && invul.invul)
                    return;
                float dir = col.transform.position.x - transform.position.x;
                if (MeersmashActive)
                {
                    col.GetComponent<MovingObstacle>().meerSmashed = true;
                    col.GetComponent<Rigidbody2D>().velocity = new Vector2((dir * 3), 7);
                    smashCollisions++;
                    audSource.PlayOneShot(smashCollision);
                }
                else
                    Death(dir);
            }
            else if (col.gameObject.tag == "MB")
            {
                audSource.PlayOneShot(coin);
                coinsThisRun += 5;
                Destroy(col.gameObject);
            }
            else if (col.gameObject.tag == "BCan")
            {
                FindObjectOfType<CameraShake>().ShakeCamera(1, 5);
                Destroy(col.gameObject);
            }
            else if (col.gameObject.tag == "Mys")
            {
                MysteryBox();
                Destroy(col.gameObject);
            }
            else if (col.gameObject.tag == "Emi")
            {
                emiManager.StartEmitters();
            }
            else if (col.gameObject.tag == "Tut")
            {
                tut.DisplayNext();
                if (tut.tutorial != 11)
                    paused = true;
            }
            else if (col.gameObject.tag == "Tut2")
            {
                tut.DisplayNextTutLevel();
                paused = true;
            }

            //achievements
            if (candyMunched == 5)
            {
                uiManager.ShowAchievement("candy");
                candyMunched = 0;
            }
            if (smashCollisions == 10)
            {
                uiManager.ShowAchievement("smash");
                smashCollisions = 0;
            }
		}
	}

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
            blocked = false;
    }

    void Starve()
    {
        //spawn popup for out of candy
        uiManager.redFlashBool = false;
        anim.SetBool("Death", true);
        running = false;
        storedGameSpeed = gameSpeed;
        gameSpeed = 0;
        storedCurrentSpeed = currentSpeed;
        currentSpeed = 0;
        finalScore = ((transform.position.y - distanceMarker.transform.position.y) / 2);
        if (finalScore > gManager.highScore)
        {
            gManager.highScore = finalScore;
            PlayerPrefs.SetFloat("HighScore", finalScore);
        }

        if (coinsThisRun > 0)
        {
            gManager.gold += coinsThisRun;
            PlayerPrefs.SetInt("Gold", gManager.gold);
        }
        //snapshot
        StartCoroutine(uiManager.HandleDeath());
    }

	void Death(float dir) 
	{
        //Play sound, change colour, play anim, stop movement etc.
		audSource.PlayOneShot (enemyCollide);
        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 255);
        uiManager.redFlashBool = false;
        if (dir >= 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        anim.SetBool ("Death", true);
		running = false;
        //Store speed for $ continue
        storedGameSpeed = gameSpeed;
		gameSpeed = 0;
        storedCurrentSpeed = currentSpeed;
        currentSpeed = 0;
		finalScore = ((transform.position.y - distanceMarker.transform.position.y)/2);
		if (finalScore > gManager.highScore) 
		{
			gManager.highScore = finalScore;
			PlayerPrefs.SetFloat("HighScore", finalScore);
		}

		if (coinsThisRun > 0) 
		{
			gManager.gold += coinsThisRun;
			PlayerPrefs.SetInt("Gold", gManager.gold);
		}
        //snapshot
		StartCoroutine(uiManager.HandleDeath());
        notStarved = true;
    }

	void OnCollisionExit2D(Collision2D col)
	{
		blocked = false;
	}

	public void StartRunning()
	{
        // Called from UI "tap to start"
        paused = false;
		running = true;
		anim.Play ("MeerKashRun");
        currentSpeed = gameSpeed;
        emiManager.StartEmitters();
    }

	public void Pause()
	{
		paused = true;
	}

    public void SpeedUp()
    {
        gameSpeed += speedIncrease;
    }

	public void AfterDeathAnim()
	{
		audSource.PlayOneShot (stars);
        if(notStarved)
            headStars.SetActive(true);
        //else out of candy
	}

	public void MysteryBox()
	{
		audSource.PlayOneShot (mysteryBox);

		//if solo mode 4 drops, if competitive 2 drops (3&4)
		int boxReward = Random.Range (1, 5);
		if (boxReward == 1) {
            gManager.superCandies += 1;
			mBoxPopups [0].SetActive (true);
		} else if (boxReward == 2) {
            gManager.meersmashes += 1;
			mBoxPopups [1].SetActive (true);
		}
		else if (boxReward == 3) {
			FindObjectOfType<CameraShake> ().ShakeCamera (1, 5);
			mBoxPopups [2].SetActive (true);
		} 
		else if (boxReward == 4) {
			coinsThisRun += 250;
			mBoxPopups [3].SetActive (true);
		}
	}

    public void SuperCandy()
    {
        if (running)
        {
            if (!powerUpActive)
            {
                powerUpActive = true;
                powerUpDuration = SuperCandyDuration;
                currentPowerUp = powerUpDuration;
                gManager.superCandies -= 1;
                timeRemaining = timeTilDie;
            }
            //Else to stop double use?
        }
    }

    public void Meersmash()
    {
        if (running)
        {
            if (!powerUpActive)
            {
                powerUpActive = true;
                MeersmashActive = true;
                anim.SetBool("Smash", true);
                powerUpDuration = MeersmashDuration;
                currentPowerUp = powerUpDuration;

                if (tutorial)
                {
                    currentPowerUp = currentPowerUp / 1.5f;
                    gManager.tutMeersmashes -= 1;
                }
                else
                    gManager.meersmashes -= 1;
            }
        }
    }

    public void StartFlashing(float flashTime)
    {
        currentFlashTime = flashTime;
        flashing = true;
    }

    public void Respawn()
    {
        //Handle continuing with $ item
        StartFlashing(5);
        invincible = true;
        anim.SetBool("Death", false);
        running = true;
        gameSpeed = storedGameSpeed;
        currentSpeed = storedCurrentSpeed;
        timeRemaining = timeTilDie;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        headStars.SetActive(false);
        notStarved = false;
    }


    //For disabling controls during tutorial
    public void TutLeft()
    {
        tutLeftBlock = false;
        StartRunning();
        Dodging(-1);
    }

    public void TutRight()
    {
        tutRightBlock = false;
        tutLeftBlock = true;
        StartRunning();
        Dodging(1);
    }

    public void TutLockRemove()
    {
        StartRunning();
        tutRightBlock = false;
        tutLeftBlock = false;
    }
}
