using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishController : MonoBehaviour {

    public Transform StartPoint;
    public Transform EndPoint;
    public Transform Fish;
    public float TargetTime;
    public float Gravity;

    Vector3 initialPosition;
    Vector3 currentPosition;
    Vector3 acceleration;
    Vector3 initialVelocity;
    float startTime;
    float currentTime;

	// Use this for initialization
	void Start () {
        initialPosition = StartPoint.position;
        currentPosition = initialPosition;
        acceleration = new Vector3(0, -Gravity, 0);
        initialVelocity = (EndPoint.position - StartPoint.position - 0.5f * acceleration * TargetTime * TargetTime) / TargetTime;
	}
	
    public void throwFish()
    {
        startTime = Time.time;
        currentTime = 0;
        Fish.position = initialPosition;
        StartCoroutine(_throwFish());
    }

    IEnumerator _throwFish()
    {
        while (currentTime < TargetTime)
        {
            currentTime = Time.time - startTime;
            currentPosition = initialPosition + initialVelocity * currentTime + 0.5f * acceleration * currentTime * currentTime;
            Fish.position = currentPosition;
            yield return null;
        }
    }
}
