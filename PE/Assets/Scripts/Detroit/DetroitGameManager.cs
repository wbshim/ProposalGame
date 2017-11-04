using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DetroitGameManager : MonoBehaviour
{
    DetroitCameraController camController;
    Quaternion playerTargetRotation;
    public Transform Car;
    public bool IntroSequencePlayed;
    public bool GamePlayActive;
    public Transform QTEPosition;
    Vector3 cameraOffset;

    CarControllerPhysics carController;
    DetroitPlayer Player;
    public DialogueRunner DialogueRunner;

    // QTE variables
    public float QTEDuration;
    public bool QTEInputGiven = false;
    public bool QTEInputCorrect;
    
    // UI
    public Canvas canvas;
    DetroitUIManager uiManager;
    public Transform TimeBar;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    // Use this for initialization
    void Start()
    {
        camController = Camera.main.GetComponent<DetroitCameraController>();
        carController = Car.GetComponent<CarControllerPhysics>();
        Player = Car.GetComponent<DetroitPlayer>();
        uiManager = canvas.GetComponent<DetroitUIManager>();
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        uiManager.PlayIntroCard();
        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManager>().SetBackgroundMusic("BackgroundJustin");
        camController.BeginCameraPath();
    }

    public void StartGamePlay()
    {
        GamePlayActive = true;
        Vector3 carPosition = Car.position;
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraOffset = cameraPosition - carPosition;
        playerTargetRotation = Camera.main.transform.rotation;
        Debug.Log("Xoffset: " + cameraOffset.x.ToString() + "YOffset: " + cameraOffset.y.ToString() + "ZOffset: " + cameraOffset.z.ToString());
        camController.SetTarget(Car, cameraOffset, playerTargetRotation);
        // Change traffic light to green

        // Ramp up car speed
        carController.Accelerate();

        // Begin dialogue
        DialogueRunner.StartDialogue();
    }
    public void StartQTE()
    {
        StartCoroutine(_startQTE(QTEPosition));
    }
    IEnumerator _startQTE(Transform _target)
    {
        camController.SetTarget(_target, Vector3.zero, _target.rotation);
        StartCoroutine(_qteManager(QTEDuration));
        yield return null;
    }
    IEnumerator _qteManager(float _duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0;
        float fullTimeBarWidth;
        float timeBarWidth;
        float timeBarHeight;
        RectTransform timeBar = TimeBar.GetComponent<RectTransform>();
        fullTimeBarWidth = timeBarWidth = timeBar.sizeDelta.x;
        timeBarHeight = timeBar.sizeDelta.y;
        Player.QTEActive = true;

        // Show QTE instructions
        uiManager.ShowQTECard(true);
        while (elapsedTime < _duration && !QTEInputGiven)
        {
            elapsedTime = Time.time - startTime;
            Debug.Log("Elapsed time: " + elapsedTime.ToString());
            timeBarWidth = fullTimeBarWidth * (_duration - elapsedTime / _duration);
            timeBar.sizeDelta = new Vector2(timeBarWidth, timeBarHeight);
            yield return null;
        }
        Player.QTEActive = false;
        uiManager.ShowQTECard(false);
        if (QTEInputGiven)
        {
            if(QTEInputCorrect)
            {
                // WIN
                uiManager.PlaySuccessCard();
                carController.TurnLeft();
                yield return new WaitForSeconds(1.2f);
                camController.SetTarget(Car, cameraOffset, playerTargetRotation);
                Camera.main.transform.rotation = playerTargetRotation;
            }
            else
            {
                uiManager.PlayFailureCard();
                carController.TurnRight();
            }
        }
        else
        {
            uiManager.PlayFailureCard();
            carController.TurnRight();
        }
        yield return null;
    }

}

