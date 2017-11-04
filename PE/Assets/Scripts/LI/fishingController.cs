using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CnControls;

public class fishingController : MonoBehaviour {

    #region ALL JOYSTICK CONTROLS VARIABLES
    Vector3 joystickInput;
    int startQuadrant;
    int currentQuadrant;
    int previousQuadrant;
    float idleStart; // Time when joystick stopped moving
    float idleTime; // Time spent idle
    float lastQuadrantChangeTime;
    float currentQuadrantChangeTime;
    float timeToChangeQuadrant;
    int currentRotationDirection; // 1 = CCW; -1 = CW
    int previousRotationDirection; // 1 = CCW; -1 = CW
    float currentPowerLevel;
    float targetPowerLevel;

    public float TargetIdleTime; // How long to wait to check for idle;
    public float RampUpSmoothSpeed;
    public float RampDownSmoothSpeed;
    #endregion

    bool fishHooked;
    float fishReelPercent;
    int numFishCaught;

    fishingGameManager gameManager;

    Animator playerAnimator;
    float animationReelingPower;

    public Transform FishingPole;
    Animator fishingPoleAnimator;
    float fishBit;
    public float animationSmooth;

    public Transform Fish;
    fishController fishController;

    public Transform FishIconsGrid;
    Image[] fishIcons;
    RectTransform[] fishIconTransforms;

    fishingUIManager uiManager;
    AudioManager audioManager;
    AudioSource failSound;

    bool PlayedFailSound = false;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<fishingGameManager>();
        uiManager = FindObjectOfType<fishingUIManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        failSound = audioManager.GetSource("GotHit");
    }
    // Use this for initialization
    void Start()
    {
        failSound.volume = 0.15f;
        playerAnimator = GetComponent<Animator>();
        fishingPoleAnimator = FishingPole.GetComponent<Animator>();
        previousQuadrant = 1;
        previousRotationDirection = 0;
        startQuadrant = 0;
        currentPowerLevel = 0;
        targetPowerLevel = 0;
        idleTime = 0;

        fishController = Fish.GetComponent<fishController>();

        numFishCaught = 0;
        StartCoroutine(waitForFish());

        fishIcons = FishIconsGrid.GetComponentsInChildren<Image>();
        fishIconTransforms = FishIconsGrid.GetComponentsInChildren<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        #region JOYSTICK CONTROLS
        joystickInput = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"), 0f);
        if (joystickInput.magnitude > 0)
        {
            if (startQuadrant == 0)
            {
                startQuadrant = CircleQuadrant(joystickInput);
            }
        }
        //Debug.Log(CircleQuadrant(joystickInput));
        currentQuadrant = CircleQuadrant(joystickInput);
        // If quadrant changes
        if (currentQuadrant != previousQuadrant)
        {
            // Joystick moved
            // Reset idle time
            idleTime = idleStart = 0;
            // Determine direction of rotation
            if (currentQuadrant > previousQuadrant)
            {
                if (currentQuadrant == 4 && previousQuadrant == 1)
                    currentRotationDirection = -1;
                else
                    currentRotationDirection = 1;
            }
            else if (currentQuadrant < previousQuadrant)
            {
                if (currentQuadrant == 1 && previousQuadrant == 4)
                    currentRotationDirection = 1;
                else
                    currentRotationDirection = -1;
            }
            // If direction of rotation changes
            if (currentRotationDirection != previousRotationDirection)
            {
                targetPowerLevel = currentPowerLevel = 0;
            }
            else
            {
                currentQuadrantChangeTime = Time.time;
                timeToChangeQuadrant = currentQuadrantChangeTime - lastQuadrantChangeTime;
                lastQuadrantChangeTime = currentQuadrantChangeTime;
                targetPowerLevel = Mathf.Clamp((joystickInput.magnitude * 0.1f) / timeToChangeQuadrant, 0, 1f);
            }
            //Debug.Log("currentPowerLevel = " + currentPowerLevel);
            //Debug.Log("currentQuadrant = " + currentQuadrant);
            previousQuadrant = currentQuadrant;
            previousRotationDirection = currentRotationDirection;
        }
        else
        {
            if (idleStart == 0)
            {
                idleStart = Time.time;
            }
            idleTime = Time.time - idleStart;
            if (idleTime > 1f)
                targetPowerLevel = 0;
        }
        if (joystickInput.magnitude == 0) // if joystick released
        {
            targetPowerLevel = 0;
            startQuadrant = 0;
        }
        if (currentPowerLevel < targetPowerLevel)
            currentPowerLevel = Mathf.Lerp(currentPowerLevel, targetPowerLevel, RampUpSmoothSpeed * Time.deltaTime);
        else
            currentPowerLevel = Mathf.Lerp(currentPowerLevel, targetPowerLevel, RampDownSmoothSpeed * Time.deltaTime);
        //Debug.Log(currentPowerLevel);
        #endregion
    }
    int CircleQuadrant(Vector3 circle)
    {
        int quadrant;
        if (circle.x >= 0 && circle.y >= 0)
            quadrant = 1;
        else if (circle.x < 0 && circle.y >= 0)
            quadrant = 2;
        else if (circle.x < 0 && circle.y < 0)
            quadrant = 3;
        else
            quadrant = 4;
        return quadrant;
    }
    public IEnumerator waitForFish()
    {
        if (!gameManager.gameHasEnded)
        {
            float waitTime = Time.time + Random.Range(2, 5);
            uiManager.WaitCardSetState("wait");
            Debug.Log("Waiting for a bite...");
            while (Time.time < waitTime)
            {
                if (joystickInput.magnitude > 0)
                {
                    playerAnimator.SetTrigger("TooEarly");
                    uiManager.WaitCardSetState("early");
                    if (!failSound.isPlaying)
                        failSound.Play();
                    Debug.Log("Wait before you reel! Waiting again...");
                    waitTime = Time.time + Random.Range(2, 5);
                }
                yield return null;
            }
            fishBit = 0;
            if(!gameManager.gameHasEnded)
            {
                uiManager.WaitCardSetState("reel");
                while (fishBit < 1)
                {
                    fishingPoleAnimator.SetFloat("fishBit", fishBit);
                    fishBit = fishBit + animationSmooth;
                    yield return null;
                }
                fishReelPercent = 0;
                StartCoroutine(fishReelProgress());
            }

        }
    }

    IEnumerator fishReelProgress()
    {
        AudioSource reelSound = audioManager.GetSource("Reel");
        reelSound.volume = 0.1f;
        while (fishReelPercent < 100)
        {
            fishReelPercent = Mathf.RoundToInt(fishReelPercent + 2 * currentPowerLevel);
            yield return null;
            playerAnimator.SetFloat("reelingPower", currentPowerLevel);
            if (currentPowerLevel > 0.05f)
            {
                if (!reelSound.isPlaying)
                    reelSound.Play();
            }
            else
                reelSound.Stop();
        }
        numFishCaught++;
        gameManager.WonboScore++;

        reelSound.Stop();
        while (fishBit > 0)
        {
            fishingPoleAnimator.SetFloat("fishBit", fishBit);
            fishBit = fishBit - animationSmooth;
            playerAnimator.SetFloat("reelingPower", animationReelingPower);
            animationReelingPower = animationReelingPower - animationSmooth;
            yield return null;
        }
        audioManager.Play("Collect");
        currentPowerLevel = 0;
        Debug.Log("Fish caught! Total number of fish caught: " + numFishCaught);
        fishController.throwFish();
        yield return new WaitForSeconds(1f);
        fishHooked = false;
        #region ANIMATE ICONS
        float i = 0;
        while (i < 1)
        {
            i = Mathf.Lerp(i, 1.1f, 0.1f);
            Color iconColor = fishIcons[numFishCaught - 1].color;
            iconColor.a = i;
            fishIcons[numFishCaught - 1].color = iconColor;
            fishIconTransforms[numFishCaught].pivot = new Vector2(1-i, 1);
            yield return null;
        }
        #endregion
        StartCoroutine(waitForFish());
    }
}
