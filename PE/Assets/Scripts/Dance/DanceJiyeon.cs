using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceJiyeon : MonoBehaviour {

    DanceHints danceHint;
    VegasGameManager gameManager;

    Animator anim;
    bool isDancing;

    float startTime;
    float elapsedTime;

    float bpm;
    float spb; // seconds per beat

    int danceCount = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<VegasGameManager>();
        danceHint = FindObjectOfType<DanceHints>();
    }
    private void Start()
    {
        startTime = Time.time;
        isDancing = false;
        bpm = gameManager.BPM;
        spb = 60 / bpm;
    }
    private void Update()
    {
        elapsedTime = Time.time - startTime;
        if (elapsedTime > 0.6f)
        {
            if(isDancing)
            {
                StopDancing();
                anim.speed = 1;
                isDancing = false;
            }
        }
    }

    void StopDancing()
    {
        anim.SetBool("isDancing", false);
    }
    public void Dance()
    {
        isDancing = true;
        anim.SetBool("isDancing", true);
        if(elapsedTime < 0.48f)
        {
            anim.speed = 0.48f / elapsedTime;
        }
        startTime = Time.time;
    }
    public void UpdateScore()
    {
        int score;
        float accuracy = Mathf.Abs(elapsedTime - spb) / spb; // accuracy of input

        if (danceCount == 0)
        {
            score = 100;
            danceCount++;
        }
        else
        {
            if (accuracy < 0.25f)
            {
                score = 100;
                danceHint.ShowCard("Perfect", Color.green, Color.white);
            }
            else if (accuracy < 0.5f)
            {
                score = 50;
                if (elapsedTime > spb)
                    danceHint.ShowCard("Too Late", Color.red, Color.white);
                else
                    danceHint.ShowCard("Too Early", Color.yellow, Color.black);
            }
            else
            {
                score = 0;
                if (elapsedTime > spb)
                    danceHint.ShowCard("Too Late", Color.red, Color.white);
                else
                    danceHint.ShowCard("Too Early", Color.yellow, Color.black);
            }
        }

        gameManager.UpdateScore(score);
    }
}
