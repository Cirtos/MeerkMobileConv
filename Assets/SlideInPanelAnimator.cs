using UnityEngine;
using System.Collections;

public class SlideInPanelAnimator : MonoBehaviour {

    public bool loadAfterIn;
    public bool loadAfterOut;
    public GameObject thingToLoad;
    public bool disableParent;

    private Animator anim;
    private bool disabling;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void OnEnable () {
        anim.Play("MenuPanelSlidefromBottom");
    }

    public void WhenDisabled()
    {
        anim.SetTrigger("Disable");
    }

    void AnimFinished()
    {
        if (loadAfterIn)
        {
            thingToLoad.SetActive(true);
            return;
        }
        if (loadAfterOut)
        {
            thingToLoad.SetActive(true);
        }
        if (disableParent)
            transform.parent.gameObject.SetActive(false);
        else
            gameObject.SetActive(false);
    }

}
