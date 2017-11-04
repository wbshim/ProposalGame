using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fishingGameManager : MonoBehaviour {
    
    public Transform TimerTextUI;
    public int TimeLimit;
    float startTime;
    float elapsedTime;
    TextMeshProUGUI timerText;
    fishingUIManager uiManager;
    AudioManager audioManager;
    AudioSource oceanSound;
    // Players
    public Transform Wonbo;
    public Transform JiYeon;
    public int WonboScore;
    public int JiyeonScore;

    public bool gameHasEnded = true;

    public string NextLevel = "GR";

    CPC_CameraPath cameraPath;
    private void Awake()
    {
        //Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 2;
        uiManager = GameObject.Find("Canvas").GetComponent<fishingUIManager>();
        timerText = TimerTextUI.GetComponentInChildren<TextMeshProUGUI>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    void Start()
    {
        uiManager.PlayTitleIntro();
        Camera cam = Camera.main;
        cameraPath = (CPC_CameraPath)cam.GetComponent<CPC_CameraPath>();
        timerText.text = TimeLimit.ToString();
        StartCoroutine(GameStart(10));
        oceanSound = audioManager.Play2("Ocean",0.15f, 2);
    }
	// Update is called once per frame
	void Update ()
    {
        if(!gameHasEnded)
        {
            if (elapsedTime < TimeLimit)
            {
                elapsedTime = Time.time - startTime;
                timerText.SetText(Mathf.Ceil(TimeLimit - elapsedTime).ToString());
                if (TimeLimit - elapsedTime <= 5)
                {
                    uiManager.StartLowTimeMode();
                }
            }
            else
            {
                GameOver();
            }
        }

	}
    void GameOver()
    {
        if(!gameHasEnded)
        {
            audioManager.Play("Win");
            gameHasEnded = true;
            string s;
            if (WonboScore > JiyeonScore)
                s = "Wonbo caught more fish! But that's not what actually happened...";
            else if (WonboScore == JiyeonScore)
                s = "Wonbo and Ji-Yeon caught the same amount of fish! What are the chances??";
            else // JiyeonScore > WonboScore
                s = "Ji-Yeon caught more fish! Which is what actually happened.";
            uiManager.PlayFinalCard(s, NextLevel);
            audioManager.Stop(oceanSound, 2f);
            Debug.Log("Game Over!");
        }
        
    }
    IEnumerator GameStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime - 3);
        cameraPath.PlayPath(3f);
        uiManager.ShowHUD();
        yield return new WaitForSeconds(3);
        if(gameHasEnded)
        {
            startTime = Time.time;
            gameHasEnded = false;
            uiManager.ShowStart();
            StartCoroutine(ShowWaitCard(1));
            StartCoroutine(Wonbo.GetComponent<fishingController>().waitForFish());
            StartCoroutine(JiYeon.GetComponent<fishingControllerAI>().waitForFish());
        }

    }
    IEnumerator ShowWaitCard(float t)
    {
        yield return new WaitForSeconds(t);
        uiManager.ShowWaitCard();
    }
}
