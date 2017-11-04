using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class AppleGameManager : MonoBehaviour {

    // Apple Tree for Apple Spawning
    public Transform[] AppleTrees;

    public Transform Apple;
    Transform appleToThrow;
    Rigidbody appleRB;

    public float TargetTime;
    public float Gravity;
    public float Thrust;
    public float Torque;

    // Timer;
    public Transform TimerTextUI;
    public int TimeLimit;
    float gameStartTime;
    float gameElapsedTime;
    TextMeshProUGUI timerText;
    bool gameActive;

    // Score
    int numApples;

    // Players
    public Transform Wonbo;
    AppleWonbo appleWonbo;
    public Transform JiYeon;
    AppleJiYeon appleJiYeon;

    // UI
    public Canvas AppleUI;
    UIManagerApple uiManager;

    // Audio manager
    AudioManager audioManager;

    // Next level
    public string NextLevel = "detroit";
    private void Awake()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    void Start () {

        numApples = 0;
        // Start game timer
        timerText = TimerTextUI.GetComponentInChildren<TextMeshProUGUI>();
        timerText.text = TimeLimit.ToString();

        // Get Ji-Yeon apple class
        appleJiYeon = JiYeon.GetComponent<AppleJiYeon>();
        appleWonbo = Wonbo.GetComponent<AppleWonbo>();
        // Get UI manager
        uiManager = AppleUI.GetComponent<UIManagerApple>();

        // Play intro sequence
        StartCoroutine(PlayIntro());
    }
    private void Update()
    {
        if(gameActive)
        {
            if (gameElapsedTime < TimeLimit)
            {
                gameElapsedTime = Time.time - gameStartTime;
                timerText.SetText(Mathf.Ceil(TimeLimit - gameElapsedTime).ToString());
                if(TimeLimit - gameElapsedTime <= 5)
                {
                    TargetTime = 0.5f;
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
        if (gameActive)
        {
            gameActive = false;
            StartCoroutine(EndGame());
            audioManager.Play("Win");
            Debug.Log("Game Over!");
        }
    }
    IEnumerator SpawnApple()
    {
        while(gameActive)
        {
            int i = Random.Range(0, AppleTrees.Length);
            ThrowApple(AppleTrees[i].position);
            yield return new WaitForSeconds(TargetTime);
        }
    }
    void ThrowApple(Vector3 startPosition)
    {
        Vector3 shootProjection = new Vector3(0, 10, -1);
        Vector3 spinVector = new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1));
        spinVector = spinVector.normalized;
        shootProjection = shootProjection.normalized;
        appleToThrow = Instantiate(Apple, startPosition, Quaternion.identity) as Transform;
        appleRB = appleToThrow.GetComponent<Rigidbody>();
        appleRB.AddForce(shootProjection * Thrust, ForceMode.Impulse);
        appleRB.AddTorque(spinVector * Torque, ForceMode.Impulse);
    }
    public void IncreaseScore()
    {
        numApples++;
        audioManager.Play("Collect");
        uiManager.UpdateScore(numApples.ToString());
        Debug.Log("Score: " + numApples);
    }
    IEnumerator PlayIntro()
    {
        // Play intro title
        uiManager.ShowTitle();
        yield return new WaitForSeconds(4f);
        FindObjectOfType<DialogueRunner>().StartDialogue();
    }
    [YarnCommand("BeginGameplay")]
    public void BeginGameplay()
    {
        StartCoroutine(_BeginGameplay());
    }
    IEnumerator _BeginGameplay()
    {
        appleJiYeon.IntroMove();
        yield return new WaitForSeconds(1f);
        appleJiYeon.FadeOutAndDie();
        uiManager.ShowHUD();
        yield return new WaitForSeconds(2f);
        uiManager.ShowStart();
        yield return new WaitForSeconds(2f);
        appleWonbo.GameActive = true;
        gameActive = true;
        gameStartTime = Time.time;
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnApple());
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.ShowEnd(numApples, NextLevel);
        appleWonbo.GameActive = false;
        yield return null;
    }
}
