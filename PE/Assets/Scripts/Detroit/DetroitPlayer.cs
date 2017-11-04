using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroitPlayer : MonoBehaviour {

    public Transform GameManager;
    DetroitGameManager gameManager;

    TouchInput swipeControls;

    public bool QTEActive = false;

	// Use this for initialization
	void Start () {
        swipeControls = GetComponent<TouchInput>();
        gameManager = GameManager.GetComponent<DetroitGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if(QTEActive)
        {
            if (swipeControls.SwipeLeft)
            {
                gameManager.QTEInputGiven = gameManager.QTEInputCorrect = true;
            }
            if (swipeControls.SwipeRight)
            {
                gameManager.QTEInputGiven = true;
                gameManager.QTEInputCorrect = false;
            }
        }

	}
}
