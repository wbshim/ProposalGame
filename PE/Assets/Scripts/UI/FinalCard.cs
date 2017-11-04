using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalCard : MonoBehaviour {

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
        card.sizeDelta = new Vector2(0, card.sizeDelta.y);
    }

    public void PlayCard(string scene)
    {
        StartCoroutine(_PlayCard(scene));
    }
    IEnumerator _PlayCard(string scene)
    {
        startEaseTime = Time.time;
        while (currentwidth < CardWidth)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(0, CardWidth, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, card.rect.height);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        if(scene != "")
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void SetText(string s)
    {
        message.text = s;
    }

}
