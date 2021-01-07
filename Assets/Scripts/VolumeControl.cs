using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour {

    public bool isMusic;

    private AudioSource aSource;
    private GameManager gManager;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
        aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isMusic)
        {
            if (aSource.volume != gManager.musVolume)
                aSource.volume = gManager.musVolume;
        }
        else
        {
            if (aSource.volume != gManager.sfxVolume)
                aSource.volume = gManager.sfxVolume;
        }
	}
}
