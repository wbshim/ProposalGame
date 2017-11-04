using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuContent : MonoBehaviour {

    RectTransform rect;

    float currentPosX;
    public float TargetPosX;

    // Easing
    public float EaseDuration = 0.5f;
    float startEaseTime;
    float currentEaseTime;

    private void Awake()
    {
        currentPosX = -100;
        rect = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start () {
        rect.anchoredPosition = new Vector2(currentPosX, 0);
	}
	
	public void Show()
    {
        StartCoroutine(_Show());
    }
    IEnumerator _Show()
    {
        startEaseTime = Time.time;
        while (currentPosX < TargetPosX)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentPosX = Mathf.SmoothStep(-100, TargetPosX, currentEaseTime);
            rect.anchoredPosition = new Vector2(currentPosX, 0);
            yield return null;
        }
    }
    public void Hide()
    {
        StartCoroutine(_Hide());
    }
    IEnumerator _Hide()
    {
        startEaseTime = Time.time;
        currentPosX = 100;
        while (currentPosX > -100)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentPosX = Mathf.SmoothStep(100, -100, currentEaseTime);
            rect.anchoredPosition = new Vector2(currentPosX, 0);
            yield return null;
        }
    }
    
}
