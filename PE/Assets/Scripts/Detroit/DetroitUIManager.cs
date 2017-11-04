using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DetroitUIManager : MonoBehaviour {
    public Transform TitleCard;
    public Transform SuccessCard;
    public Transform FailureCard;
    public Transform CanadaFlag;
    public Transform CanadaLabel;
    public Transform FinalCard;
    public Transform IntroCard;
    public Transform QTECard;
    //public float SuccessWaitTime;
    private void Start()
    {
    }
    public void PlayIntroCard()
    {
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
    IEnumerator _playSuccessCard()
    {
        yield return new WaitForSeconds(0.2f);
        SuccessCard.GetComponent<SuccessCard>().ShowCard();
    }
    public void PlayFailureCard()
    {
        StartCoroutine(_playFailureCard());
    }
    IEnumerator _playFailureCard()
    {
        yield return new WaitForSeconds(0.2f);
        FailureCard.GetComponent<FailureCard>().ShowCard();
        yield return new WaitForSeconds(2f);
        RectTransform flagRect = CanadaFlag.GetComponent<RectTransform>();
        float targetYpos = 0;
        float currentYPos = flagRect.localPosition.y;
        Debug.Log(targetYpos);
        FindObjectOfType<AudioManager>().SetBackgroundMusic("Canada");
        while (currentYPos > targetYpos)
        {
            
            flagRect.localPosition= new Vector2(flagRect.localPosition.x, currentYPos);
            currentYPos = Mathf.Lerp(currentYPos, targetYpos-10, 3 * Time.deltaTime);
            yield return null;
            Debug.Log(currentYPos);
        }
        flagRect.localPosition = new Vector2(flagRect.localPosition.x, 0);
        yield return new WaitForSeconds(0.5f);
        float alpha2 = 0f;
        Color c2 = CanadaLabel.GetComponent<Image>().color;
        while (alpha2 <= 1)
        {
            alpha2 = alpha2 + 0.05f;
            c2.a = alpha2;
            CanadaLabel.GetComponent<Image>().color = c2;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        RestartLevel();

    }
    public void ShowQTECard(bool show)
    {
        QTECard.gameObject.SetActive(show);
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    [YarnCommand("LoadNextLevel")]
    public void PlayFinalCard()
    {
        FinalCard.GetComponent<FinalCard>().PlayCard("sb"); // load office

    }

}
