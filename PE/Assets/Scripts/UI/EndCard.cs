using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCard : MonoBehaviour {

    public float CardWidth;
    public float CardShowTime = 0.5f;

    RectTransform card;
    public Transform CardText;
    RectTransform cardText;
    float currentwidth;

    // Easing
    public float EaseDuration = 10f;
    float startEaseTime;
    float currentEaseTime;

    // Use this for initialization
    void Start () {
        card = GetComponent<RectTransform>();
        cardText = CardText.GetComponent<RectTransform>();
        card.sizeDelta = new Vector2(0, card.sizeDelta.y);
	}

    public void PlayCard()
    {
        StartCoroutine(_PlayCard());
    }
    IEnumerator _PlayCard()
    {
        startEaseTime = Time.time;
        while (currentwidth < CardWidth)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(0, CardWidth, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, card.rect.height);
            yield return null;
        }
        yield return new WaitForSeconds(CardShowTime);
        card.pivot = new Vector2(1, card.pivot.y);
        card.localPosition = new Vector2(400, card.localPosition.y);
        cardText.anchorMin = new Vector2(1, 0.5f);
        cardText.anchorMax = new Vector2(1, 0.5f);
        startEaseTime = Time.time;
        while (currentwidth > 0)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(CardWidth, 0, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, card.rect.height);
            yield return null;
        }
        //card.sizeDelta = new Vector2(0, card.rect.height);
        gameObject.SetActive(false);
        yield return null;
    }
}
