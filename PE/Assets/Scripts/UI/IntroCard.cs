using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroCard : MonoBehaviour {
    public float CardWidth;
    public float CardShowTime = 0.5f;

    RectTransform card;
    float currentwidth;

    public Transform Message;
    TextMeshProUGUI message;

    // Easing
    public float EaseDuration = 0.5f;
    float startEaseTime;
    float currentEaseTime;

    private void Awake()
    {
        card = GetComponent<RectTransform>();
        message = Message.GetComponent<TextMeshProUGUI>();
        //message.text = "";
    }

    void Start()
    {
        //card.sizeDelta = new Vector2(0, card.sizeDelta.y);
    }

    public void PlayCard()
    {
        StartCoroutine(_PlayCard());
    }
    IEnumerator _PlayCard()
    {
        yield return new WaitForSeconds(CardShowTime);
        currentwidth = CardWidth;
        // Wipe out
        card.pivot = new Vector2(1, card.pivot.y);
        card.localPosition = new Vector2(400, card.localPosition.y);
        startEaseTime = Time.time;
        while (currentwidth > 0)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(CardWidth, 0, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, card.rect.height);
            yield return null;
        }
        Destroy(gameObject);
    }
    public void SetText(string s)
    {
        message.text = s;
    }
}
