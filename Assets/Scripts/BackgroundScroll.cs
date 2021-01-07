using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

	public bool isFirst;

	void Update(){

		if(isFirst) 
		{
			Player player = FindObjectOfType<Player>();
			transform.Translate (-transform.up * player.currentSpeed * Time.deltaTime);
		}
			

		if (transform.position.y <= -15)
			Destroy (gameObject);


	}
}
