using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegasUIManager : MonoBehaviour {
    RectTransform btnDance;
    RectTransform btnDanceHalo;
    public Transform Title;

    Transform progressBar;
    RectTransform progressBarIcon;
    RectTransform progressBarBG;
    RectTransform progressBarFill;

    float progressBarWidth;

    public Transform StartCard;
    public Transform EndCard;
    public Transform FinalCard;
    public Transform IntroCard;

    // Score keeping
    float targetScore;
    float currentScore;

    VegasGameManager gameManager;

    private void Awake()
    {
        btnDance = GameObject.Find("btnDance").GetComponent<RectTransform>();
        btnDanceHalo = btnDance.FindChild("btnDanceHalo").GetComponent<RectTransform>();
        progressBar = GameObject.Find("ProgressBar").GetComponent<Transform>();
        progressBarIcon = GameObject.Find("ProgressBarIcon").GetComponent<RectTransform>();
        progressBarBG = progressBar.GetComponent<RectTransform>();
        progressBarFill = progressBar.FindChild("Fill").GetComponent<RectTransform>();
        gameManager = GameObject.Find("GameManager").GetComponent<VegasGameManager>();
    }
    private void Start()
    {
        btnDance.localScale = Vector3.zero;
        progressBarWidth = progressBarBG.sizeDelta.x;
        progressBar.gameObject.SetActive(false);
        targetScore = gameManager.TargetScore;
    }
    public void PlayIntro()
    {
        StartCoroutine(_PlayIntro());
    }
    IEnumerator _PlayIntro()
    {
        IntroCard.GetComponent<IntroCard>().PlayCard();
        yield return new WaitForSeconds(3);
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.SetBackgroundMusic("BackgroundSafeAndSound");
        yield return new WaitForSeconds(1);
        Title.GetComponent<titleCard>().PlayTitleIntro();
    }
    public void ShowControls()
    {
        StartCoroutine(_ShowControls());
        StartCoroutine(_ShowProgressBar());
    }
    IEnumerator _ShowControls()
    {
        float s = 0;
        while (s <= 1)
        {
            btnDance.localScale = Vector3.one * s;
            s = s + 0.05f;
            yield return null;
        }
        PlayStartCard();
    }
    void PlayStartCard()
    {
        StartCard.GetComponent<StartCard>().PlayCard();
        GameObject.Find("btnDance").GetComponent<Button>().StartButtonHint();
    }
    IEnumerator _ShowProgressBarIcon()
    {
        progressBarIcon.localScale = Vector3.zero;
        float s = 0;
        while(s <= 1)
        {
            progressBarIcon.localScale = Vector3.one * s;
            s = s + 0.025f;
            yield return null;
        }
    }
    IEnumerator _ShowProgressBar()
    {
        progressBar.gameObject.SetActive(true);
        StartCoroutine(_ShowProgressBarIcon());
        float targetWidth = progressBarBG.sizeDelta.x;
        float w = 0;
        float h = progressBarBG.sizeDelta.y;
        progressBarBG.sizeDelta = new Vector2(0, h);
        progressBarFill.sizeDelta = new Vector2(0, h);

        // Show progressBar
        while(w <= targetWidth)
        {
            w = Mathf.Lerp(w, targetWidth + 10, 2.5f * Time.deltaTime);
            progressBarBG.sizeDelta = new Vector2(w, h);
            yield return null;
        }
    }
    public void PressButton()
    {
        IEnumerator c = _PressButton();
        StopCoroutine(c);
        StartCoroutine(c);
        GameObject.Find("btnDance").GetComponent<Button>().StopButtonHint();
    }
    IEnumerator _PressButton()
    {
        float s = 0.6f; // scale
        float a = 1; // alpha
        Image i = btnDanceHalo.GetComponent<Image>();
        while(a >= 0)
        {
            s = Mathf.Lerp(s, 2.5f, 5 * Time.deltaTime);
            a = Mathf.Lerp(a, -0.5f, 5 * Time.deltaTime);
            Color c = new Color(255, 255, 255, a);
            i.color = c;
            btnDanceHalo.localScale = new Vector3(s, s,1);
            
            yield return null;
        }
        btnDanceHalo.localScale = new Vector3(0.6f, 0.6f, 1);
        a = 1;
    }
    public void UpdateProgressBar(int score)
    {
        IEnumerator c = _UpdateProgressBar(score);
        StopCoroutine(c);
        StartCoroutine(c);
    }
    IEnumerator _UpdateProgressBar(int score)
    {
        currentScore = score;
        float targetWidth = Mathf.Clamp((currentScore / targetScore) * progressBarWidth,0f,progressBarWidth);
        float currentWidth = progressBarFill.sizeDelta.x;
        float h = progressBarFill.sizeDelta.y;
        while (currentWidth <= targetWidth)
        {
            currentWidth = Mathf.Lerp(currentWidth, targetWidth + 5f, 5f * Time.deltaTime);
            progressBarFill.sizeDelta = new Vector2(currentWidth, h);
            yield return null;
        }
    }
    public void PlayEndCard()
    {
        StartCoroutine(_PlayEndCard());
    }
    IEnumerator _PlayEndCard()
    {
        EndCard.GetComponent<EndCard>().PlayCard();
        yield return new WaitForSeconds(2f);
        FinalCard.GetComponent<FinalCard>().PlayCard("orchard");
    }
}
