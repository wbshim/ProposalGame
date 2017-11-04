using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class StarbucksGameManager : MonoBehaviour {

    UIManager uiManager;
    AudioSource coffeeShopAmbience;
    public Button arrow;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        arrow = FindObjectOfType<Button>();
    }
    // Use this for initialization
    void Start () {
        
        FindObjectOfType<AudioManager>().SetBackgroundMusic("BackgroundWeepies");
        coffeeShopAmbience = FindObjectOfType<AudioManager>().Play2("CoffeeShopAmbience",0.08f,1);
        arrow.HideButton();
    }
    [YarnCommand("playFinalCard")]
    public void PlayFinalCard()
    {
        uiManager.ShowFinalCardBlank();
        uiManager.ShowFinalCard();
        FindObjectOfType<DialogueUI>().textSpeed = 0;
        FindObjectOfType<DialogueUI>().WaitTime = 5;
        FindObjectOfType<DialogueRunner>().StartDialogue("End");
        FindObjectOfType<AudioManager>().Stop(coffeeShopAmbience, 1f);
        FindObjectOfType<AudioManager>().SetBackgroundMusicVolume(0.5f);
    }
    [YarnCommand("PlaySound")]
    public void PlaySound(string s)
    {
        FindObjectOfType<AudioManager>().Play(s, 0.2f);
    }
    [YarnCommand("ShowArrow")]
    public void ShowArrow()
    {
        arrow.StartArrowIndicator();
    }
}
