using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExpandUiButton : MonoBehaviour {

    public GameObject explanation;
    public RectTransform background;
    public float timeToExpand;
    public PanelMove[] thingsToMove;
    public Sprite more;
    public Sprite less;

    private bool expanding;
    private bool closing;
    private bool open;
    private bool block;
    private float currentTimeToExpand;
    private float startPoint;
    private float endPoint;
    private float originalBGOffMin;
    private Image image;
    private ExpandUiButton[] expands;

    void Start()
    {
        image = GetComponent<Image>();
        startPoint = 0;
        endPoint = 160;
        explanation.SetActive(false);
        originalBGOffMin = background.offsetMin.y;
        expands = FindObjectsOfType<ExpandUiButton>();
    }

    void Update()
    {
        
        if (expanding)
        {
            currentTimeToExpand += Time.deltaTime;
            //issues with decimals causing zeros
            float ler = (currentTimeToExpand * 100) / (timeToExpand * 100) ;
            float botPos = Mathf.Lerp(startPoint, endPoint, ler);
            background.offsetMin = new Vector2(background.offsetMin.x, originalBGOffMin - botPos);
            //issues with it moving based on position of first recttransform
            foreach (PanelMove Expand in thingsToMove)
            {
                Expand.Move(botPos, false);
            }
            if (currentTimeToExpand >= timeToExpand)
            {
                image.sprite = less;
                explanation.SetActive(true);
                open = true;
                expanding = false;
            }
        }
        else if (closing)
        {
            explanation.SetActive(false);
            currentTimeToExpand += Time.deltaTime;
            float ler = (currentTimeToExpand * 100) / (timeToExpand * 100);
            float botPos = Mathf.Lerp(endPoint, startPoint, ler);
            background.offsetMin = new Vector2(background.offsetMin.x, originalBGOffMin - botPos);
            foreach (PanelMove Expand in thingsToMove)
            {
                Expand.Move(botPos, true);
            }
            if (currentTimeToExpand >= timeToExpand)
            {
                image.sprite = more;
                open = false;
                closing = false;
            }
        }
    }

	public void ExpandClose ()
    {
        if (!open)
        {
            if (AnyOpen(expands))
            {
                return;
            }
        }
        
        if (!open)
        {
            currentTimeToExpand = 0;
            expanding = true;
        }
        else
        {
            currentTimeToExpand = 0;
            closing = true;
        }
	}

    bool AnyOpen(ExpandUiButton[] array)
    {
        bool answer = false;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].open)
            {
                answer = true;
                break;
            }
        }
        print(answer);
        return answer;
    }

    void OnDisable()
    {
        if (open)
        {
            explanation.SetActive(false);
            background.offsetMin = new Vector2(background.offsetMin.x, originalBGOffMin);
            foreach (PanelMove Expand in thingsToMove)
            {
                Expand.Reset();
            }
                image.sprite = more;
                open = false;
                closing = false;
        }
    }
}
