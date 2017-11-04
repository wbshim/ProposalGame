using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkiPlayerController : MonoBehaviour {

    float xPos;
    float targetXPos;
    public float Smooth = 3f;
    public bool Skiing;
    public int NumLives = 3;
    bool respawning;

    // Characters
    public Transform Wonbo;
    SkiWonbo wb;
    public Transform JiYeon;
    SkiJiYeon jk;

    SkiGameManager gameManager;
    SkiUIManager uiManager;

    Rigidbody rb;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<SkiUIManager>();
        rb = GetComponentInChildren<Rigidbody>();
    }
    private void Start()
    {
        wb = Wonbo.GetComponent<SkiWonbo>();
        jk = JiYeon.GetComponent<SkiJiYeon>();

    }
    // Update is called once per frame
    void Update () {
        if (!gameManager.GameOver)
        {
            targetXPos = GameObject.Find("Slider").GetComponent<Slider>().value;
            xPos = Mathf.Lerp(xPos, targetXPos, Smooth * Time.deltaTime);
            float xVel = (targetXPos - xPos) / (Smooth * Time.deltaTime);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(30, -xVel, 0);
            float turnAngle = Mathf.Clamp(-xVel / 60, -1, 1);
            wb.AnimateTurnAngle(turnAngle);
            jk.AnimateTurnAngle(turnAngle);
        }
    }

    public void Die()
    {
        if(!gameManager.GameOver)
        {
            if (!respawning)
            {
                Skiing = false;
                FindObjectOfType<AudioManager>().Play("GotHit");
                StartCoroutine(Respawn(2));
                NumLives--;
                uiManager.DecreaseLife(NumLives);
                if (NumLives <= 0)
                {
                    if (!gameManager.GameOver)
                        StartCoroutine(gameManager.EndGame());
                }
                else
                {
                    StartCoroutine(gameManager.ResetSpeed(20));
                }
            }
        }


    }
    IEnumerator Respawn(float t)
    {
        respawning = true;
        yield return new WaitForSeconds(t);
        respawning = false;
    }
}
