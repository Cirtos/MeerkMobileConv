using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float shakeTimer;
    public float shakeAmount;

	private bool justShook;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

		if (shakeTimer > 0) {
			if (!justShook) {
				Vector2 ShakePos = (Random.insideUnitCircle * 0.1f) * shakeAmount;
				transform.position = new Vector3 (transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
				justShook = true;
			} else {
				transform.position = new Vector3 (0, 0, transform.position.z);
				justShook = false;
			}
			shakeTimer -= Time.deltaTime;
		}
	}

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmount = shakePwr;
        shakeTimer = shakeDur;
    }
}
