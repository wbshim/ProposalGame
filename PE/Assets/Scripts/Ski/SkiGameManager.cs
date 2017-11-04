using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SkiGameManager : MonoBehaviour {
    public float Speed; // Player speed
    public int Score = 0;
    public bool GameOver;
    SkiUIManager uiManager;

    private void Awake()
    {
        //Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 2;
        uiManager = GameObject.Find("Canvas").GetComponent<SkiUIManager>();
    }
    private void Start()
    {
        GameOver = false;
        uiManager.PlayTitleIntro();
        StartCoroutine(StartDialogue(5));
        FindObjectOfType<AudioManager>().SetBackgroundMusic("BackgroundDeadmau5");
    }

    public void IncreaseScore()
    {
        Score+=10;
        uiManager.IncreaseScore(Score);
        if(Score >=300)
        {
            StartCoroutine(EndGame());
        }
        else if (Score % 100 == 0) // Every 100 points
        {
            Debug.Log("Increasing speed");
            StartCoroutine(IncreaseSpeed(Speed + 3));
            StartCoroutine(IncreaseTrees());
            FindObjectOfType<AudioManager>().Play("LevelUp", 0.3f);
            uiManager.FlashScore(); // Flash score label yellow
        }
        else
            FindObjectOfType<AudioManager>().Play("Collect", 0.3f);
    }
    IEnumerator IncreaseSpeed(float targetSpeed)
    {
        while(Speed < targetSpeed)
        {
            Speed += 0.5f;
            yield return null;
        }
    }
    IEnumerator IncreaseTrees()
    {
        if(GameObject.Find("TreesManager").GetComponent<TreesController>().TreeGenerateTime >= 1.5f)
            GameObject.Find("TreesManager").GetComponent<TreesController>().TreeGenerateTime -= 0.2f;
        yield return null;
    }
    public IEnumerator ResetSpeed(float targetSpeed)
    {
        GameObject.Find("TreesManager").GetComponent<TreesController>().SetGenerating(false);
        GameObject.Find("HillManager").GetComponent<HillsController>().SetGenerating(false);
        GameObject.Find("BeerManager").GetComponent<BeerController>().SetGenerating(false);
        GameObject.Find("SkiLiftNodeManager").GetComponent<SkiLiftNodesController>().SetGenerating(false);
        GameObject.Find("SkiLiftManager").GetComponent<SkiLiftController>().SetGenerating(false);
        while (Speed > 3)
        {
            Speed -= 1f;
            yield return null;
        }
        while(Speed < targetSpeed)
        {
            Speed += 0.2f;
            yield return null;
        }
        GameObject.Find("TreesManager").GetComponent<TreesController>().SetGenerating(true);
        GameObject.Find("HillManager").GetComponent<HillsController>().SetGenerating(true);
        GameObject.Find("BeerManager").GetComponent<BeerController>().SetGenerating(true);
        GameObject.Find("SkiLiftNodeManager").GetComponent<SkiLiftNodesController>().SetGenerating(true);
        GameObject.Find("SkiLiftManager").GetComponent<SkiLiftController>().SetGenerating(true);
    }
    public IEnumerator EndGame()
    {
        GameOver = true;
        GameObject.Find("TreesManager").GetComponent<TreesController>().SetGenerating(false);
        GameObject.Find("HillManager").GetComponent<HillsController>().SetGenerating(false);
        GameObject.Find("BeerManager").GetComponent<BeerController>().SetGenerating(false);
        GameObject.Find("SkiLiftNodeManager").GetComponent<SkiLiftNodesController>().SetGenerating(false);
        GameObject.Find("SkiLiftManager").GetComponent<SkiLiftController>().SetGenerating(false);
        while (Speed > 1)
        {
            Speed--;
            yield return null;
        }
        Speed = 0;
        FindObjectOfType<AudioManager>().Play("Win");
        uiManager.ShowFinalScore(Score);
        Debug.Log("Speed=" + Speed);
    }
    IEnumerator BeginGameplay(float t)
    {
        uiManager.ShowUI();
        uiManager.PlayStartCard();
        yield return new WaitForSeconds(t);
        GameObject.Find("TreesManager").GetComponent<TreesController>().SetGenerating(true);
        GameObject.Find("HillManager").GetComponent<HillsController>().SetGenerating(true);
        GameObject.Find("BeerManager").GetComponent<BeerController>().SetGenerating(true);
        GameObject.Find("SkiLiftNodeManager").GetComponent<SkiLiftNodesController>().SetGenerating(true);
        GameObject.Find("SkiLiftManager").GetComponent<SkiLiftController>().SetGenerating(true);
    }
    IEnumerator StartDialogue(float t)
    {
        yield return new WaitForSeconds(t);
        FindObjectOfType<DialogueRunner>().StartDialogue("Start");
    }
    [YarnCommand("BeginGameplay")]
    public void BeginGameplay()
    {
        StartCoroutine(BeginGameplay(1.5f));
    }
}
