using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SkiFinalScore : MonoBehaviour
{

    public float CardWidth;
    public float CardShowTime = 0.5f;

    RectTransform card;
    public Transform FinalScoreCount;
    TextMeshProUGUI finalScoreCount;
    public Transform FinalScoreMessage;
    TextMeshProUGUI finalScoreMessage;
    float currentwidth;
    public Transform[] CardTexts;
    // Easing
    public float EaseDuration = 0.5f;
    float startEaseTime;
    float currentEaseTime;

    // Use this for initialization
    void Start()
    {
        card = GetComponent<RectTransform>();
        finalScoreCount = FinalScoreCount.GetComponent<TextMeshProUGUI>();
        finalScoreMessage = FinalScoreMessage.GetComponent<TextMeshProUGUI>();
        finalScoreMessage.text = "";
        card.sizeDelta = new Vector2(0, card.sizeDelta.y);
    }

    public IEnumerator PlayCard(int score)
    {
        Debug.Log("Showing final score");
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
            yield return new WaitForSeconds(0.05f);
            currentScore+=10;
        }
        yield return new WaitForSeconds(0.5f);
        string scoreMessage = "";
        if (currentScore <= 100)
        {
            scoreMessage = "Stupid sexy flanders";
        }
        else if (currentScore > 100 && currentScore <= 200)
        {
            scoreMessage = "See? That wasn't so bad.";
        }
        else if (currentScore > 200 && currentScore <= 250)
        {
            scoreMessage = "That went longer than I expected";
        }
        else
        {
            scoreMessage = "Okay that's enough skiing";
        }
        finalScoreMessage.text = scoreMessage;
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("LI", LoadSceneMode.Single);

        //Destroy(gameObject);
    }
}
