using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureCard : MonoBehaviour {

    public float LabelWidthStart;
    public float LabelWidthEnd;
    public float LabelShowTime = 2f;

    RectTransform title;
    float currentwidth;

    // Easing
    public float EaseDuration = 10f;
    float startEaseTime;
    float currentEaseTime;

    // Use this for initialization
    void Start () {
        title = GetComponent<RectTransform>();
        title.sizeDelta = new Vector2(0, title.rect.height);
        currentwidth = LabelWidthStart;
        //StartCoroutine(TitleIntro());
	}
    public void ShowCard()
    {
        StartCoroutine(_showCard());
    }
    IEnumerator _showCard()
    {
        startEaseTime = Time.time;
        while (currentwidth < LabelWidthEnd - 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(LabelWidthStart, LabelWidthEnd, currentEaseTime);
            title.sizeDelta = new Vector2(currentwidth, title.rect.height);
            yield return null;
        }
        yield return new WaitForSeconds(LabelShowTime);
        startEaseTime = Time.time;
        while (currentwidth > 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(LabelWidthEnd, LabelWidthStart, currentEaseTime);
            title.sizeDelta = new Vector2(currentwidth, title.rect.height);
            yield return null;
        }
        title.sizeDelta = new Vector2(0, title.rect.height);
        gameObject.SetActive(false);
    }
	

}
