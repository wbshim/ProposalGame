using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KayakController : MonoBehaviour {
    Rigidbody rigidBody;
    Kayak kayak;

    Quaternion deltaRotation;
    Vector3 direction;

    public float TargetSpeed = 10f;
    public float SmoothSpeedUp = 10f;
    public float SmoothSpeedDown = 0.01f;
    public float SmoothAngularSpeedUp = 10f;
    public float SmoothAngularSpeedDown = 1f;
    public float Distance = 10f;
    public float TargetAngularVelocity = 4f;

    float currentSpeed;
    float currentAngularVelocity;
    float targetDeltaAngularVelocity;
    bool isAccelerating;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        kayak = GetComponent<Kayak>();
        currentSpeed = 0;
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
        if(isAccelerating)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, TargetSpeed, SmoothSpeedUp* Time.fixedDeltaTime);
            currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, targetDeltaAngularVelocity, SmoothAngularSpeedUp * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, SmoothSpeedDown * Time.fixedDeltaTime);
            currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, 0f, SmoothAngularSpeedDown * Time.fixedDeltaTime);
        }
        Vector3 eulerAngleVelocity = new Vector3(0, currentAngularVelocity, 0);
        deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        if (currentSpeed >= TargetSpeed-0.2f)
        {
            isAccelerating = false;
        }
        rigidBody.MovePosition(rigidBody.position + direction * currentSpeed * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    public void moveKayak(Vector3 _direction, float _relativeInputPosition)
    {
        isAccelerating = true;
        targetDeltaAngularVelocity = -TargetAngularVelocity * _relativeInputPosition;
        direction = _direction;
    }
}
