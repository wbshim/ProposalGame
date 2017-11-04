using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class test_joystick : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        previousQuadrant = 1;
        previousRotationDirection = 0;
        startQuadrant = 0;
        currentPowerLevel = 0;
        targetPowerLevel = 0;
        idleTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        joystickInput = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"), 0f);
        if(joystickInput.magnitude > 0)
        {
            if(startQuadrant == 0)
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
                targetPowerLevel = Mathf.Clamp((joystickInput.magnitude * 0.1f)/timeToChangeQuadrant,0,1f);
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
        if(currentPowerLevel < targetPowerLevel)
            currentPowerLevel = Mathf.Lerp(currentPowerLevel, targetPowerLevel, RampUpSmoothSpeed * Time.deltaTime);
        else
            currentPowerLevel = Mathf.Lerp(currentPowerLevel, targetPowerLevel, RampDownSmoothSpeed * Time.deltaTime);
        Debug.Log(currentPowerLevel);
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
    void GetRotationInformation()
    {

    }
}
