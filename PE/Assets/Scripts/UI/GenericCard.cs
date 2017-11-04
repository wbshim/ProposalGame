using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericCard : MonoBehaviour {

    public enum TransitionStyle { wipe, appear, fade };
    public float TransitionTime;

    public float TitleWidth;
    public float ShowTime = 2f;

    RectTransform title;
    float currentwidth;

    // Easing
    public float EaseDuration = 10f;
    float startEaseTime;
    float currentEaseTime;

    // Text
    public Transform Text;
    TextMeshProUGUI text;

    // Use this for initialization
    private void Awake()
    {
        title = GetComponent<RectTransform>();
        if(Text!=null)
        {
            text = Text.GetComponent<TextMeshProUGUI>();
        }
    }
    void Start()
    {
        title.sizeDelta = new Vector2(0, title.rect.height);
        currentwidth = 0;
        //StartCoroutine(TitleIntro());
    }
    public IEnumerator TitleIntro()
    {
        startEaseTime = Time.time;
        while (currentwidth < TitleWidth - 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(0, TitleWidth, currentEaseTime);
            title.sizeDelta = new Vector2(currentwidth, title.rect.height);
            yield return null;
        }
        if(ShowTime > 0)
        {
            yield return new WaitForSeconds(ShowTime);
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
        }

    }
    public IEnumerator FlashCard(Color c, float totalTime, int numTimes)
    {
        while(numTimes > 0)
        {
            float elapsedTime = 0.0f;
            while(elapsedTime < totalTime/2)
            {
                elapsedTime += Time.deltaTime;
                GetComponent<Image>().color = Color.Lerp(Color.black, c, (elapsedTime/totalTime));
                yield return null;
            }
            while(elapsedTime <= totalTime)
            {
                elapsedTime += Time.deltaTime;
                GetComponent<Image>().color = Color.Lerp(c, Color.black, (elapsedTime / totalTime));
                yield return null;
            }
            numTimes--;
            yield return null;
        }
    }
    public void IncreaseScore(int s)
    {
        text.text = s.ToString();
    }
    public void DecreaseLives(int l)
    {
        text.text = l.ToString();
        StartCoroutine(FlashCard(Color.red, 1f, 1));
    }
}
