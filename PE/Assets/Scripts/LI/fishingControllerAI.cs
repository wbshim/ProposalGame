using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fishingControllerAI : MonoBehaviour {

    fishingGameManager gameManager;

    int numFishCaught;
    bool fishHooked;
    float fishReelPercent;
    public float targetPowerLevel;

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

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<fishingGameManager>();
    }
    // Use this for initialization
    void Start () {
        playerAnimator = GetComponent<Animator>();
        fishingPoleAnimator = FishingPole.GetComponent<Animator>();

        fishController = Fish.GetComponent<fishController>();

        fishHooked = false;
        numFishCaught = 0;
        StartCoroutine(waitForFish());

        fishIcons = FishIconsGrid.GetComponentsInChildren<Image>();
        fishIconTransforms = FishIconsGrid.GetComponentsInChildren<RectTransform>();
    }

    public IEnumerator waitForFish()
    {
        if(!gameManager.gameHasEnded)
        {
            float waitTime = Time.time + Random.Range(2, 5);
            while (Time.time < waitTime)
            {
                yield return null;
            }
            fishHooked = true;
            fishBit = 0;
            if(!gameManager.gameHasEnded)
            {
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
        while (fishReelPercent < 100)
        {
            fishReelPercent = Mathf.RoundToInt(fishReelPercent + 5 * targetPowerLevel);
            yield return new WaitForSeconds(0.1f);
            playerAnimator.SetFloat("reelingPower", 1);
        }
        numFishCaught++;
        gameManager.JiyeonScore++;

        while (fishBit > 0)
        {
            fishingPoleAnimator.SetFloat("fishBit", fishBit);
            fishBit = fishBit - animationSmooth;
            playerAnimator.SetFloat("reelingPower", animationReelingPower);
            animationReelingPower = animationReelingPower - animationSmooth;
            yield return null;
        }
        Debug.Log("Ji-Yeon caught fish! Total number of fish caught: " + numFishCaught);
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
            fishIconTransforms[numFishCaught].pivot = new Vector2(i, 1);
            yield return null;
        }
        #endregion
        //fishIcons[numFishCaught - 1].color = Color.white;
        StartCoroutine(waitForFish());
    }
}
