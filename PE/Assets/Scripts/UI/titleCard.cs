using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleCard : MonoBehaviour {

    public float TitleWidth;
    public float TitleShowTime = 2f;

    RectTransform title;
    float currentwidth;

    // Easing
    public float EaseDuration = 10f;
    float startEaseTime;
    float currentEaseTime;

    private void Awake()
    {
        title = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start () {
        title.sizeDelta = new Vector2(0, title.rect.height);
        currentwidth = 0;
	}
    public void PlayTitleIntro()
    {
        StartCoroutine(TitleIntro());
    }
    IEnumerator TitleIntro()
    {
        startEaseTime = Time.time;
        while (currentwidth < TitleWidth - 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(0, TitleWidth, currentEaseTime);
            title.sizeDelta = new Vector2(currentwidth, title.rect.height);
            yield return null;
        }
        yield return new WaitForSeconds(TitleShowTime);
        startEaseTime = Time.time;
        while (currentwidth > 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(TitleWidth, 0, currentEaseTime);
            title.sizeDelta = new Vector2(currentwidth, title.rect.height);
            yield return null;
        }
        title.sizeDelta = new Vector2(0, title.rect.height);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
	

}
