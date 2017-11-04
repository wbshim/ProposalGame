using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fishingUIManager : MonoBehaviour {

    public Transform Timer;
    RectTransform TimerRect;
    public Transform Title;
    titleCard title;
    public Transform StartCard;
    public Transform HeaderWonbo;
    RectTransform headerWonbo;
    public Transform HeaderJiYeon;
    RectTransform headerJiyeon;
    public Transform JoystickBase;
    RectTransform joystickBase;
    public Transform Joystick;
    RectTransform joystick;
    public Transform EndCard;
    EndCard endCard;
    public Transform IntroCard;
    IntroCard introCard;
    public Transform FinalCard;
    FinalCard finalCard;
    public Transform WaitCard;
    LIWaitCard waitCard;

    void Awake()
    {
        title = Title.GetComponent<titleCard>();
        TimerRect = Timer.GetComponent<RectTransform>();
        headerWonbo = HeaderWonbo.GetComponent<RectTransform>();
        headerJiyeon = HeaderJiYeon.GetComponent<RectTransform>();
        joystickBase = JoystickBase.GetComponent<RectTransform>();
        joystick = Joystick.GetComponent<RectTransform>();
        endCard = EndCard.GetComponent<EndCard>();
        introCard = IntroCard.GetComponent<IntroCard>();
        finalCard = FinalCard.GetComponent<FinalCard>();
        waitCard = WaitCard.GetComponent<LIWaitCard>();

    }
    private void Start()
    {
        TimerRect.sizeDelta = new Vector2(0, TimerRect.sizeDelta.y);
        headerWonbo.sizeDelta = new Vector2(0, headerWonbo.sizeDelta.y);
        headerJiyeon.sizeDelta = new Vector2(0, headerJiyeon.sizeDelta.y);
        joystickBase.localScale = Vector2.zero;
        joystick.localScale = Vector2.zero;

    }
    public void ShowHUD()
    {
        StartCoroutine(ShowTimer());
        StartCoroutine(ShowPlayerHeader(headerWonbo,93));
        StartCoroutine(ShowPlayerHeader(headerJiyeon,98));
        StartCoroutine(ShowControls());
    }
    public void ShowWaitCard()
    {
        waitCard.ShowCard();
        WaitCardSetState("wait");
    }
    IEnumerator ShowTimer()
    {
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
    IEnumerator ShowPlayerHeader(RectTransform header, float targetWidth)
    {
        float startEaseTime = Time.time;
        float currentEaseTime;
        float currentwidth = 0f;
        float easeDuration = 1;
        while (currentwidth < targetWidth - 2)
        {
            currentEaseTime = (Time.time - startEaseTime) / easeDuration;
            currentwidth = Mathf.SmoothStep(0, targetWidth, currentEaseTime);
            header.sizeDelta = new Vector2(currentwidth, header.sizeDelta.y);
            yield return null;
        }
    }
    public void PlayTitleIntro()
    {
        StartCoroutine(_PlayTitleIntro());
    }
    IEnumerator _PlayTitleIntro()
    {
        introCard.PlayCard();
        yield return new WaitForSeconds(4);
        title.PlayTitleIntro();
    }
    public void ShowStart()
    {
        StartCard.GetComponent<StartCard>().PlayCard();
    }
    IEnumerator ShowControls()
    {
        float s = 0;
        while (s <= 1.3)
        {
            joystickBase.localScale = Vector3.one * s;
            joystick.localScale = Vector3.one * s;
            s = s + 0.05f;
            yield return null;
        }
    }

    public void PlayFinalCard(string s, string _scene)
    {
        StartCoroutine(_PlayEndCards(s, _scene));
    }
    IEnumerator _PlayEndCards(string s, string _scene)
    {
        endCard.PlayCard();
        yield return new WaitForSeconds(3);
        finalCard.PlayCard(_scene);
        finalCard.SetText(s);
    }
    public void StartLowTimeMode()
    {
        Timer.GetComponent<Image>().color = new Color(255, 0, 0);
    }
    public void WaitCardSetState(string state)
    {
        switch (state)
        {
            case "wait":
                waitCard.SetText("wait");
                waitCard.SetColor(new Color(0.43f, 0.48f, 0.56f));
                break;
            case "reel":
                waitCard.SetText("reel!");
                waitCard.SetColor(new Color(0.27f, 0.56f, 1));
                break;
            case "early":
                StartCoroutine(TooEarly());
                break;
        }
    }
    IEnumerator TooEarly()
    {
        waitCard.SetText("too early");
        waitCard.SetColor(new Color(0.67f, 0, 0));
        yield return new WaitForSeconds(0.5f);
        waitCard.SetText("wait");
        waitCard.SetColor(new Color(0.43f, 0.48f, 0.56f));
    }
}
