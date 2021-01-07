using UnityEngine;
using System.Collections;

public class MenuBird : MonoBehaviour {

    public float speed;

    private float xDirection;
    private float yDirection;
    private Vector2 direction;

	// Use this for initialization
	void Start () {
        ChangeHeading();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed);
	}

    void ChangeHeading()
    {
        xDirection = Random.Range(-1f, 1f);
        yDirection = Random.Range(-1f, 1f);

        direction = new Vector2(xDirection, yDirection);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ChangeHeading();
    }
}
