using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour {
    
    public bool isMusic;

    private Image bar;
    private GameManager gManager;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GameManager>();
        bar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isMusic)
            bar.fillAmount = gManager.musVolume;
        else
            bar.fillAmount = gManager.sfxVolume;

	}
}
