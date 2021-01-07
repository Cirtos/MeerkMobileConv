using UnityEngine;
using System.Collections;

public class Dust : MonoBehaviour {

    public float lifeTime;
    public float speed;
    
    private SpriteRenderer sprite;
    private Player player;
    private float fullLife;
    private float startAlpha;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        sprite = GetComponent<SpriteRenderer>();
        fullLife = lifeTime;
        startAlpha = sprite.color.a;
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;

        if(player.currentSpeed == 0)
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        else
        {
            transform.Translate(Vector2.down * (player.currentSpeed / 2) * Time.deltaTime);
        }

        float lerp = (lifeTime * 100) / (fullLife * 100);
        float alpha = Mathf.Lerp(startAlpha, 0, lerp);

        sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
