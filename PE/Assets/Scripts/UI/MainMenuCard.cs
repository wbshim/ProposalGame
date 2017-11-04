using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCard : MonoBehaviour {

    RectTransform card;
    float currentwidth;
    public float CardWidth;
    float cardHeight;

    // Easing
    public float EaseDuration = 0.5f;
    float startEaseTime;
    float currentEaseTime;

    private void Awake()
    {
        card = GetComponent<RectTransform>();
    }
    // Use this for initialization
    void Start ()
    {
        cardHeight = 450;
        card.sizeDelta = new Vector2(800, cardHeight);
    }
	
	public void ShowMenu()
    {
        StartCoroutine(_ShowMenu());
    }
    IEnumerator _ShowMenu()
    {
        currentwidth = 800;
        startEaseTime = Time.time;
        while (currentwidth > CardWidth)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(800, CardWidth, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, cardHeight);
            yield return null;
        }
        FindObjectOfType<MainMenuContent>().Show();
    }
    public void CloseMenu()
    {
        FindObjectOfType<MainMenuContent>().Hide();
        StartCoroutine(_CloseMenu());
    }
    IEnumerator _CloseMenu()
    {
        currentwidth = 200;
        CardWidth = 800;
        startEaseTime = Time.time;
        while (currentwidth < CardWidth)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(200, CardWidth, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, cardHeight);
            yield return null;
        }
    }
}
