using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScoreCard : MonoBehaviour {

    public float CardWidth;
    public float CardShowTime = 0.5f;

    RectTransform card;
    public Transform FinalScoreCount;
    TextMeshProUGUI finalScoreCount;
    public Transform FinalScoreMessage;
    TextMeshProUGUI finalScoreMessage;
    float currentwidth;

    // Easing
    public float EaseDuration = 0.5f;
    float startEaseTime;
    float currentEaseTime;

    // Use this for initialization
    void Start () {
        card = GetComponent<RectTransform>();
        finalScoreCount = FinalScoreCount.GetComponent<TextMeshProUGUI>();
        finalScoreMessage = FinalScoreMessage.GetComponent<TextMeshProUGUI>();
        finalScoreMessage.text = "";
        card.sizeDelta = new Vector2(0, card.sizeDelta.y);
	}

    public void PlayCard(int score, string scene)
    {
        StartCoroutine(_PlayCard(score, scene));
    }
    IEnumerator _PlayCard(int score, string scene)
    {
        yield return new WaitForSeconds(2f);
        int currentScore = 0;
        startEaseTime = Time.time;
        while (currentwidth < CardWidth)
        {
            currentEaseTime = (Time.time - startEaseTime) / EaseDuration;
            currentwidth = Mathf.SmoothStep(0, CardWidth, currentEaseTime);
            card.sizeDelta = new Vector2(currentwidth, card.rect.height);
            yield return null;
        }
        while (currentScore <= score)
        {
            finalScoreCount.text = currentScore.ToString();
            yield return new WaitForSeconds(0.1f);
            currentScore++;
        }
        yield return new WaitForSeconds(0.5f);
        string scoreMessage = "";
        if(currentScore == 0)
        {
            scoreMessage = "Did you even try?!";
        }
        if(currentScore > 0 && currentScore <= 3)
        {
            scoreMessage = "That was uh... a good effort!";
        }
        else if(currentScore> 3 && currentScore <= 13)
        {
            scoreMessage = "Mmm apple pies for the next 3 weeks.";
        }
        else
        {
            scoreMessage = "Please stop, I can only eat so many apple fritters...";
        }
        finalScoreMessage.text = scoreMessage;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
