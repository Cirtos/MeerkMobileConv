using UnityEngine;
using System.Collections;

public class URLButton : MonoBehaviour {

    public string url;
    public bool fallback;
    public string fallbackURL;

    private bool leftApp = false;

	// Use this for initialization
	public void OnPress () {
        if (!fallback)
            Application.OpenURL(url);
        else
            StartCoroutine(OpenPage());
	}

    IEnumerator OpenPage()
    {
        Application.OpenURL(url);
        yield return new WaitForSeconds(2);
        print("left app? : " + leftApp);
        if (leftApp)
        {
            leftApp = false;
        }
        else
        {
            Application.OpenURL(fallbackURL);
        }
    }

    void OnApplicationPause()
    {
        leftApp = true;
    }
}
