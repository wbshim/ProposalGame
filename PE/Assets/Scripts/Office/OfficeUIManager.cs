using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
public class OfficeUIManager : MonoBehaviour {
    public Transform Title;
    titleCard title;

    public Transform FinalCard;
    FinalCard finalCard;

    public Transform IntroCard;

    private void Awake()
    {
        title = Title.GetComponent<titleCard>();
        finalCard = FinalCard.GetComponent<FinalCard>();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(PlayTitleIntro());
        StartCoroutine(StartDialogue(5));
	}
	
    IEnumerator PlayTitleIntro()
    {
        IntroCard.GetComponent<IntroCard>().PlayCard();
        yield return new WaitForSeconds(2);
        title.PlayTitleIntro();
    }

    IEnumerator StartDialogue(float t)
    {
        yield return new WaitForSeconds(t);
        FindObjectOfType<DialogueRunner>().StartDialogue("Start");
    }

    [YarnCommand("PlayFinalCard")]
    public void PlayFinalCard()
    {
        StartCoroutine(_PlayFinalCard(3));
    }
    IEnumerator _PlayFinalCard(float t)
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.SetBackgroundMusicVolume(0);
        finalCard.PlayCard("sbx");
        yield return null;
    }
}
