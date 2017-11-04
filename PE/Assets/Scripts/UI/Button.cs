using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    public float Speed; // # seconds per cycle
    public float MaxScale; // Max size of button
    RectTransform button;
    IEnumerator buttonHintCoroutine;
    Vector3 startPosition;

    private void Awake()
    {
        button = GetComponent<RectTransform>();
        startPosition = button.localPosition;
        
    }
    public void HideButton()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    public void StartArrowIndicator()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        buttonHintCoroutine = ButtonUpDown(MaxScale, Speed);
        StartCoroutine(buttonHintCoroutine);
    }
    public void StopArrowIndicator()
    {
        if (buttonHintCoroutine != null)
        {
            StopCoroutine(buttonHintCoroutine);
        }
        StartCoroutine(FadeOut());
    }

    public void StartButtonHint()
    {
        buttonHintCoroutine = ButtonHint(MaxScale, Speed);
        StartCoroutine(buttonHintCoroutine);
    }
	
    public void StopButtonHint()
    {
        if(buttonHintCoroutine!=null)
        {
            StopCoroutine(buttonHintCoroutine);
        }
        button.localScale = Vector3.one;
    }

    IEnumerator ButtonHint(float _scale, float _speed)
    {
        float scale = 1;
        float step = (2f) * (_scale - scale) * Time.deltaTime;
        while(true)
        {
            scale = 1;
            button.localScale = Vector3.one;
            float startTime = Time.time;
            float elapsedTime = Time.time - startTime;
            while(elapsedTime < _speed/2)
            {
                _scale += step;
                button.localScale = Vector3.one * _scale;
                elapsedTime = Time.time - startTime;
                yield return null;
            }
            while (elapsedTime < _speed)
            {
                _scale -= step;
                button.localScale = Vector3.one * _scale;
                elapsedTime = Time.time - startTime;
                yield return null;
            }
            yield return null;
        }
    }
    IEnumerator ButtonUpDown(float _scale, float _speed)
    {
        float scale = 0;
        float step = (_scale - scale) * Time.deltaTime;
        while(true)
        {
            float startTime = Time.time;
            float elapsedTime = Time.time - startTime;
            float currentScale = 0;
            button.localPosition = startPosition;
            while (elapsedTime < _speed / 2)
            {
                button.localPosition = startPosition + Vector3.up * currentScale;
                currentScale += step;
                elapsedTime = Time.time - startTime;
                yield return null;
            }
            while (elapsedTime < _speed)
            {
                button.localPosition = startPosition + Vector3.up * currentScale;
                currentScale -= step;
                elapsedTime = Time.time - startTime;
                yield return null;
            }
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        Image i = GetComponent<Image>();
        Color c = i.color;
        float a = c.a;
        Vector3 currentPos = button.localPosition;
        float y = currentPos.y;
        while (a >= 0)
        {
            c = new Color(c.r, c.g, c.b, a);
            i.color = c;
            button.localPosition = new Vector3(currentPos.x, y, currentPos.z);
            a -= 0.1f;
            y += 5;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
