using UnityEngine;
using System.Collections;

public class PanelMove : MonoBehaviour {

    private RectTransform rt;
    private Vector2 startLoc;

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();
        startLoc = rt.anchoredPosition;
	}

    public void Move(float toMove, bool up)
    {
        if (!up)
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, (startLoc.y + -toMove));
        else
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, (startLoc.y + -toMove));
    }

    public void Reset()
    {
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, startLoc.y);
    }
}
