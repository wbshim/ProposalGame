using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerApple : MonoBehaviour {

    public Transform Title;
    public Transform Timer;
    public Transform StartCard;
    public Transform EndCard;
    public Transform ScoreCard;
    public Transform IntroCard;

    public Transform Score;
    TextMeshProUGUI ScoreText;
    RectTransform ScoreImage;
    RectTransform LeftButtonUI;
    RectTransform RightButtonUI;

	// Use this for initialization
	void Start () {
        ScoreText = Score.GetComponentInChildren<TextMeshProUGUI>();
        ScoreImage = GameObject.Find("AppleScoreImage").GetComponent<RectTransform>();
        RectTransform ScoreRect = Score.GetComponent<RectTransform>();
        ScoreRect.sizeDelta = new Vector2(0, ScoreRect.sizeDelta.y);
        RectTransform TimerRect = Timer.GetComponent<RectTransform>();
        TimerRect.sizeDelta = new Vector2(0, ScoreRect.sizeDelta.y);
        LeftButtonUI = GameObject.Find("Left").GetComponent<RectTransform>();
        LeftButtonUI.localScale = Vector3.zero;
        RightButtonUI = GameObject.Find("Right").GetComponent<RectTransform>();
        RightButtonUI.localScale = Vector3.zero;
    }
	public void ShowTitle()
    {
        StartCoroutine(_ShowTitle());
    }
    IEnumerator _ShowTitle()
    {
        IntroCard.GetComponent<IntroCard>().PlayCard();
        yield return new WaitForSeconds(4f);
        Title.GetComponent<titleCard>().PlayTitleIntro();
    }
    public void ShowHUD()
    {
        StartCoroutine(_ShowHUD());
    }

    IEnumerator _ShowHUD()
    {
        StartCoroutine(ShowScore());
        StartCoroutine(ShowTimer());
        StartCoroutine(ShowControls());
        yield return null;
    }
    IEnumerator ShowScore()
    {
        RectTransform ScoreRect = Score.GetComponent<RectTransform>();
        float startEaseTime = Time.time;
        float currentEaseTime;
        float currentwidth = 0f;
        float targetWidth = 165f;
        float easeDuration = 1;
        while (currentwidth < targetWidth - 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / easeDuration;
            currentwidth = Mathf.SmoothStep(0, targetWidth, currentEaseTime);
            ScoreRect.sizeDelta = new Vector2(currentwidth, ScoreRect.sizeDelta.y);
            yield return null;
        }
    }
    IEnumerator ShowTimer()
    {
        RectTransform TimerRect = Timer.GetComponent<RectTransform>();
        float startEaseTime = Time.time;
        float currentEaseTime;
        float currentwidth = 0f;
        float targetWidth = 135f;
        float easeDuration = 1;
        while (currentwidth < targetWidth - 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / easeDuration;
            currentwidth = Mathf.SmoothStep(0, targetWidth, currentEaseTime);
            TimerRect.sizeDelta = new Vector2(currentwidth, TimerRect.sizeDelta.y);
            yield return null;
        }
    }
    IEnumerator ShowControls()
    {
        float s = 0;
        while (s <= 1)
        {
            LeftButtonUI.localScale = Vector3.one * s;
            RightButtonUI.localScale = Vector3.one * s;
            s = s + 0.05f;
            yield return null;
        }
    }
    public void UpdateScore(string score)
    {
        StartCoroutine(_UpdateScore(score));
    }

    IEnumerator _UpdateScore(string score)
    {
        ScoreText.text = score;
        float currentScaleMagnitude = 1;
        while(currentScaleMagnitude <= 1.5f)
        {
            Vector3 currentScale = new Vector3(currentScaleMagnitude, currentScaleMagnitude, 1);
            ScoreImage.localScale = currentScale;
            currentScaleMagnitude = currentScaleMagnitude + 0.1f;
            yield return null;
        }
        while (currentScaleMagnitude >= 1)
        {
            Vector3 currentScale = new Vector3(currentScaleMagnitude, currentScaleMagnitude, 1);
            ScoreImage.localScale = currentScale;
            currentScaleMagnitude = currentScaleMagnitude - 0.1f;
            yield return null;
        }

        yield return null;
    }
    public void StartLowTimeMode()
    {
        Timer.GetComponent<Image>().color = new Color(255, 0, 0);
    
    }
    public void ShowStart()
    {
        StartCard.GetComponent<StartCard>().PlayCard();
    }
    public void ShowEnd(int score, string scene)
    {
        StartCoroutine(_ShowEnd(score, scene));
    }
    IEnumerator _ShowEnd(int score, string scene)
    {
        EndCard.GetComponent<EndCard>().PlayCard();
        yield return new WaitForSeconds(1f);
        ScoreCard.GetComponent<FinalScoreCard>().PlayCard(score, scene);
        FindObjectOfType<AudioManager>().SetBackgroundMusicVolume(0.025f);
        yield return null;
    }

}
