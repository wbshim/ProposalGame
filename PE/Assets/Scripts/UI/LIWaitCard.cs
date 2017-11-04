using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LIWaitCard : MonoBehaviour {

    TextMeshProUGUI text;
    RectTransform card;
    Image cardImage;
    public float CardWidth;
    float currentWidth;

    // Easing
    public float EaseDuration = 0.5f;
    float startEaseTime;
    float currentEaseTime;

    // Target Follow
    public Transform target;
    public RectTransform canvas;
    public float offsetX;
    public float offsetY;

    float canvasWidth;
    float canvasHeight;

    // Use this for initialization
    void Awake () {
        text = GetComponentInChildren<TextMeshProUGUI>();
        card = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();

    }

    private void Start()
    {
        card.sizeDelta = new Vector2(0, card.sizeDelta.y);
        canvasWidth = canvas.rect.width;
        canvasHeight = canvas.rect.height;
    }

    private void Update()
    {
        Vector2 targetPosition = (Camera.main.WorldToViewportPoint(target.position) - new Vector3(0.5f, 0.5f));
        transform.localPosition = new Vector2(targetPosition.x * canvasWidth + offsetX, targetPosition.y * canvasHeight + offsetY);
    }

    public void ShowCard()
    {
        card.sizeDelta = new Vector2(CardWidth, card.rect.height);
    }
    public void SetText(string t)
    {
        text.text = t;
    }
    public void SetColor(Color c)
    {
        cardImage.color = c;
    }

}
