using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour {

	// Arrays of prefabs to spawn from depending on obstacle type/zone
	public GameObject[] safariAnimals, safariLevels;
	public GameObject[] jungleAnimals, jungleLevels;
	public GameObject[] arcticAnimals, arcticLevels;
    public GameObject tutorial;
    public GameObject menuTut;
    public GameObject[] candies, badCandies;
    public GameObject moneyBag, mystBox;

	private GameObject[][] animals = new GameObject[3][];
	private GameObject[][] levels = new GameObject[3][];

	public Vector3 levelSpawn = new Vector3(0,11,0);

	public Player player;
	public int currentZone; //0, 1 or 2 depending on safari, jungle or arctic

	// This bool stops static obstacles being spawned two lines in a row
	private Vector3 changeAnimalFacing;
	private bool isFirstLevel;

    private string sceneName;


	// This will spawn the first screen of obstacles + the next lane
	void Start ()
	{
		currentZone = 0;

		animals [0] = safariAnimals;
		animals [1] = jungleAnimals;
		animals [2] = arcticAnimals;

		levels [0] = safariLevels;
		levels [1] = jungleLevels;
		levels [2] = arcticLevels;

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        isFirstLevel = true;
        Vector2 center = new Vector2(0, 0);

        player = FindObjectOfType<Player>();

        if (sceneName == "Tutorial")
        {
            Instantiate(menuTut);
        }
        else if (player.tutorial)
        {
            Instantiate(tutorial);
            isFirstLevel = false;
        }
        else
		    SpawnNextLevel (center);
	}

	public void SpawnNext (Transform[] i) 
	{		
		foreach (Transform t in i) 
		{
			int numberOfSpawns = Random.Range (1, 3);
			int firstSpawnSide = Random.Range (1, 3);
			int firstAnimal = Random.Range (0, 3);
			GameObject obs1 = Instantiate (animals [currentZone] [firstAnimal], t.transform.position, Quaternion.identity) as GameObject;

			//If spawning left side
			if (firstSpawnSide == 1) 
			{
				Vector2 firstSpawn = new Vector2 ((-3.5f - Random.Range(0, 6)), obs1.transform.position.y + Random.Range(-0.5f, 0.5f));
				obs1.transform.position = firstSpawn;
				changeAnimalFacing.x = -Mathf.Abs(obs1.transform.localScale.x) * 2;
				//BUG: This used to inverse the direction of animals in the version of unity it was built in, no longer does so, causes moonwalking
				obs1.transform.localScale += changeAnimalFacing;
				if (numberOfSpawns == 2) 
				{
					int secondAnimal = Random.Range (0, 3);
					//Counter repeats
					while (secondAnimal == firstAnimal) 
					{
						secondAnimal = Random.Range (0, 3);
					}
					GameObject obs2 = Instantiate (animals [currentZone] [secondAnimal], t.transform.position, Quaternion.identity) as GameObject;
                    obs2.GetComponent<MovingObstacle>().secondSpawn = true;
					Vector2 secondSpawn = new Vector2 ((3.5f - Random.Range(0, 6)), obs1.transform.position.y + Random.Range(-0.5f, 0.5f));
					obs2.transform.position = secondSpawn;

					//Make sure animals layer correctly
					if (obs2.transform.position.y > obs1.transform.position.y)
						obs1.GetComponent<SpriteRenderer> ().sortingOrder = obs2.GetComponent<SpriteRenderer> ().sortingOrder + 1;
					else
						obs2.GetComponent<SpriteRenderer> ().sortingOrder = obs1.GetComponent<SpriteRenderer> ().sortingOrder + 1;
				}
			} 
			else 
			{
				Vector2 firstSpawn = new Vector2 ((3.5f - Random.Range(0, 6)), obs1.transform.position.y + Random.Range(-0.5f, 0.5f));
				obs1.transform.position = firstSpawn;
				if (numberOfSpawns == 2) 
				{
					int secondAnimal = Random.Range (0, 3);
					//Counter repeats
					while (secondAnimal == firstAnimal) 
					{
						secondAnimal = Random.Range (0, 3);
					}
					GameObject obs2 = Instantiate (animals [currentZone] [secondAnimal], t.transform.position, Quaternion.identity) as GameObject;
                    obs2.GetComponent<MovingObstacle>().secondSpawn = true;
                    Vector2 secondSpawn = new Vector2 ((-3.5f - Random.Range(0, 6)), obs1.transform.position.y + Random.Range(-0.5f, 0.5f));
					obs2.transform.position = secondSpawn;
					changeAnimalFacing.x = -Mathf.Abs(obs2.transform.localScale.x) * 2;
					//BUG: This used to inverse the direction of animals in the version of unity it was built in, no longer does so, causes moonwalking
					obs2.transform.localScale += changeAnimalFacing;

					//Make sure animals layer correctly
					if (obs2.transform.position.y > obs1.transform.position.y)
						obs1.GetComponent<SpriteRenderer> ().sortingOrder = obs2.GetComponent<SpriteRenderer> ().sortingOrder + 1;
					else
						obs2.GetComponent<SpriteRenderer> ().sortingOrder = obs1.GetComponent<SpriteRenderer> ().sortingOrder + 1;
				}
			}	
		}
	}
		
	public void SpawnNextLevel(Vector2 t)
	{
		//if first, spawn at 0,0,0
		if (isFirstLevel) 
		{
			Instantiate (levels [currentZone] [Random.Range (0, levels [currentZone].Length)]); 
			isFirstLevel = false;
		}
		else
			Instantiate (levels [currentZone] [Random.Range (0, levels [currentZone].Length)], t, Quaternion.identity); 
	}

    public void TutorialSpawnNext(Transform[] i)
    {
        //only single spawns in tutorial
        foreach (Transform t in i)
        {
            if (i == null)
                print("i is null");
            else
            {
                int firstSpawnSide = Random.Range(1, 3);
                int firstAnimal = Random.Range(0, 3);

                if(t == null)
                    print ("t is null");
            
                GameObject obs1 = Instantiate(animals[currentZone][firstAnimal], t.transform.position, Quaternion.identity) as GameObject;

                if (firstSpawnSide == 1)
                {
                    Vector2 firstSpawn = new Vector2((-3.5f - Random.Range(0, 6)), obs1.transform.position.y + Random.Range(-0.5f, 0.5f));
                    obs1.transform.position = firstSpawn;
                    changeAnimalFacing.x = -Mathf.Abs(obs1.transform.localScale.x) * 2;
                    obs1.transform.localScale += changeAnimalFacing;
                }
                else
                {
                    Vector2 firstSpawn = new Vector2((3.5f - Random.Range(0, 6)), obs1.transform.position.y + Random.Range(-0.5f, 0.5f));
                    obs1.transform.position = firstSpawn;
                }
            }
        }
    }

    public void SpawnCollectibles (Transform[] candy, Transform[] badCandy, Transform[] moneyBags, Transform[] mysteryBox, GameObject level)
    {
        foreach (Transform t in candy)
        {
            GameObject c = Instantiate(candies[currentZone], t.transform.position, Quaternion.identity) as GameObject;
            c.gameObject.transform.parent = level.transform;
        }
        foreach (Transform t in badCandy)
        {
            GameObject bc = Instantiate(badCandies[currentZone], t.transform.position, Quaternion.identity) as GameObject;
            bc.gameObject.transform.parent = level.transform;
        }
        foreach (Transform t in moneyBags)
        {
            GameObject mb = Instantiate(moneyBag, t.transform.position, Quaternion.identity) as GameObject;
            mb.gameObject.transform.parent = level.transform;
        }
        foreach (Transform t in mysteryBox)
        {
            GameObject mysb = Instantiate(mystBox, t.transform.position, Quaternion.identity) as GameObject;
            mysb.gameObject.transform.parent = level.transform;
        }
    }
}
