using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public bool PlayTitleOnStart;

    public Transform TitleCard;
    public Transform SuccessCard;

    public float TitleWaitTime;
    public float SuccessWaitTime;

    RectMask2D titleMask;
    Animation titleAnimation;

    public Transform StartCard;
    public Transform EndCard;
    public Transform FinalCard;
    public Transform FinalCardBlank;

    public Transform IntroCard;

    public string NextLevel = "dance";

    public void Start()
    {
        if (PlayTitleOnStart)
            PlayTitleIntro();
        IntroCard.GetComponent<IntroCard>().PlayCard();
    }

    public void PlayTitleIntro()
    {
        TitleCard.GetComponent<titleCard>().PlayTitleIntro();
    }

    public void PlaySuccessCard()
    {
        SuccessCard.GetComponent<SuccessCard>().ShowCard();
    }
    public void ShowStartCard()
    {
        StartCard.GetComponent<StartCard>().PlayCard();
    }
    public void ShowFinishCard()
    {
        EndCard.GetComponent<EndCard>().PlayCard();
    }
    public void ShowFinalCard()
    {
        FinalCard.GetComponent<FinalCard>().PlayCard(NextLevel);
    }
    public void ShowFinalCardBlank()
    {
        StartCoroutine(_ShowFinalCardBlank());
    }
    IEnumerator _ShowFinalCardBlank()
    {
        yield return new WaitForSeconds(3f);
        FinalCardBlank.gameObject.SetActive(true);
    }
}
