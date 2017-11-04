using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiUIManager : MonoBehaviour {

    public Transform TitleCard;
    public Transform ScoreCard;
    public Transform FinalScoreCard;
    public Transform StartCard;
    public Transform IntroCard;

    GenericCard scoreCard;
    public Transform LivesCard;
    GenericCard livesCard;

    // Use this for initialization
    void Awake () {
        scoreCard = ScoreCard.GetComponent<GenericCard>();
        livesCard = LivesCard.GetComponent<GenericCard>();
	}

    public void PlayTitleIntro()
    {
        StartCoroutine(_playTitleIntro());
    }
    public void ShowUI()
    {
        StartCoroutine(scoreCard.TitleIntro());
        StartCoroutine(livesCard.TitleIntro());
    }
    public void IncreaseScore(int s)
    {
        scoreCard.IncreaseScore(s);
    }
    public void FlashScore()
    {
        StartCoroutine(scoreCard.FlashCard(Color.yellow, 0.5f, 3));
    }
    public void DecreaseLife(int l)
    {
        livesCard.DecreaseLives(l);
    }
    IEnumerator _playTitleIntro()
    {
        IntroCard.GetComponent<IntroCard>().PlayCard();
        yield return new WaitForSeconds(4);
        TitleCard.GetComponent<titleCard>().PlayTitleIntro();
    }
    public void ShowFinalScore(int score)
    {
        StartCoroutine(FinalScoreCard.GetComponent<SkiFinalScore>().PlayCard(score));
    }
    public void PlayStartCard()
    {
        StartCard.GetComponent<StartCard>().PlayCard();
    }

}
