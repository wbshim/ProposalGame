using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DanceHints : MonoBehaviour {

    TextMeshProUGUI text;
    RectTransform card;
    Image cardImage;
    Vector3 startPosition;

    IEnumerator ShowCardCoroutine;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        card = GetComponent<RectTransform>();
        cardImage = GetComponent<Image>();
    }

    private void Start()
    {
        startPosition = card.localPosition;
        gameObject.SetActive(false);
    }

    public void ShowCard(string s, Color c_c, Color c_t)
    {
        ShowCardCoroutine = _ShowCard(s, c_c, c_t);
        StopCoroutine(ShowCardCoroutine);
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        StartCoroutine(ShowCardCoroutine);
    }
    IEnumerator _ShowCard(string s, Color c_c, Color c_t)
    {
        cardImage.color = c_c;
        text.color = c_t;
        text.text = s;
        card.localPosition = startPosition;
        Vector3 currentPos = card.localPosition;
        float a = 1;
        float y = currentPos.y;
        while (cardImage.color.a >= 0)
        {
            c_c = new Color(c_c.r, c_c.g, c_c.b, a);
            c_t = new Color(c_t.r, c_t.g, c_t.b, a);
            cardImage.color = c_c;
            card.localPosition = new Vector3(currentPos.x, y, currentPos.z);
            a -= 0.1f;
            y += 5;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
