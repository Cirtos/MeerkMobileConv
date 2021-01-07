using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public Transform[] movingObstacleSpawns;
    public Transform[] candySpawns, badCandySpawns, moneyBagSpawns, mysteryBoxSpawns;
    public Transform pointToSpawn;
    public bool isTutorial;
    public bool isMenuTut;

    private SpawnManager sManager;
	private Player player;

	// Use this for initialization
	void Start () {
		sManager = FindObjectOfType<SpawnManager> ();
		player = FindObjectOfType<Player> ();

        if (!isMenuTut)
        {
            if (isTutorial)
                sManager.TutorialSpawnNext(movingObstacleSpawns);
            else
                sManager.SpawnNext(movingObstacleSpawns);

            sManager.SpawnCollectibles(candySpawns, badCandySpawns, moneyBagSpawns, mysteryBoxSpawns, gameObject);
        }
        else
        {
            sManager.TutorialSpawnNext(movingObstacleSpawns);
        }
	}

	void Update () {
		transform.Translate (-transform.up * player.currentSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isTutorial)
            {
                sManager.currentZone++;
                if (sManager.currentZone >= 3)
                    sManager.currentZone = 0;
            }
            sManager.SpawnNextLevel(new Vector2(pointToSpawn.position.x, pointToSpawn.position.y));
        }
    }
}