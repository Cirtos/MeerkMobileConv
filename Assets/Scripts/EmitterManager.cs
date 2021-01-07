using UnityEngine;
using System.Collections;

public class EmitterManager : MonoBehaviour {

    public GameObject desertEmitters, jungleEmitters, arcticEmitters;

    private SpawnManager sManager;
    private int currentArea;
    private ParticleSystem[] desertParts, jungleParts, arcticParts;

	// Use this for initialization
	void Start () {

        sManager = FindObjectOfType<SpawnManager>();

        desertParts = desertEmitters.GetComponentsInChildren<ParticleSystem>();
        jungleParts = jungleEmitters.GetComponentsInChildren<ParticleSystem>();
        arcticParts = arcticEmitters.GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentArea != sManager.currentZone)
        {
            Despawn(currentArea);
            currentArea = sManager.currentZone;
        }
	}

    void Despawn(int current)
    {
        if(current == 0)
        {
            foreach(ParticleSystem ps in desertParts)
            {
                ps.Stop();
            }
        }
        else if (current == 1)
        {
            foreach (ParticleSystem ps in jungleParts)
            {
                ps.Stop();
            }
        }
        else if (current == 2)
        {
            foreach (ParticleSystem ps in arcticParts)
            {
                ps.Stop();
            }
        }
    }

    public void StartEmitters()
    {
        if (currentArea == 0)
        {
            arcticEmitters.SetActive(false);
            desertEmitters.SetActive(true);
        }
        else if (currentArea == 1)
        {
            desertEmitters.SetActive(false);
            jungleEmitters.SetActive(true);
        }
        else if (currentArea == 2)
        {
            jungleEmitters.SetActive(false);
            arcticEmitters.SetActive(true);
        }
    }
}
