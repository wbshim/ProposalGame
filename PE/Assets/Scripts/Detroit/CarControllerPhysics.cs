using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CarControllerPhysics : MonoBehaviour {

    public float Speed = 5;
    public float TargetThrustTorque;
    float thrustTorque;
    public float SteerTorque;
    public float TargetSpeed;
    public float MaxSteeringAngle = 45;
    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelBL;
    public WheelCollider WheelBR;
    Rigidbody rigidBody;
    bool accelerating;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(accelerating)
        {
            if (rigidBody.velocity.magnitude < Speed)
            {
                thrustTorque = TargetThrustTorque;
            }
            else
            {
                thrustTorque = 0;
            }

            WheelFL.motorTorque = thrustTorque;
            WheelFR.motorTorque = thrustTorque;
            WheelBL.motorTorque = thrustTorque;
            WheelBR.motorTorque = thrustTorque;
            WheelFL.transform.Rotate(0, 0, WheelBR.rpm / 60 * -360 * Time.fixedDeltaTime);
            WheelFR.transform.Rotate(0, 0, WheelBR.rpm / 60 * -360 * Time.fixedDeltaTime);
            WheelBL.transform.Rotate(0, 0, WheelBR.rpm / 60 * -360 * Time.fixedDeltaTime);
            WheelBR.transform.Rotate(0, 0, WheelBR.rpm / 60 * -360 * Time.fixedDeltaTime);
        }

    }
    public void Accelerate()
    {
        accelerating = true;
    }
    public void TurnLeft()
    {
        StartCoroutine(_TurnLeft());
    }
    IEnumerator _TurnLeft()
    {
        float s = 0;
        while(s <= 1)
        {
            Debug.Log(s);
            WheelFL.steerAngle = MaxSteeringAngle * -s;
            WheelFR.steerAngle = MaxSteeringAngle * -s;
            s = s + 0.25f;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        while (s >= 0)
        {
            WheelFL.steerAngle = MaxSteeringAngle * -s;
            WheelFR.steerAngle = MaxSteeringAngle * -s;
            s = s - 0.25f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (s <= 1)
        {
            WheelFL.steerAngle = MaxSteeringAngle * s;
            WheelFR.steerAngle = MaxSteeringAngle * s;
            s = s + 0.25f;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        while (s >= 0)
        {
            WheelFL.steerAngle = MaxSteeringAngle * s;
            WheelFR.steerAngle = MaxSteeringAngle * s;
            s = s - 0.25f;
            yield return null;
        }
        //GameObject.Find("DialogueContainerWonbo").gameObject.SetActive(false);
        FindObjectOfType<DialogueRunner>().StartDialogue("Success");
        GameObject.Find("DialogueContainerJiYeon").gameObject.SetActive(false);
    }
    public void TurnRight()
    {
        StartCoroutine(_TurnRight());
    }
    IEnumerator _TurnRight()
    {
        yield return new WaitForSeconds(0.5f);
        float s = 0; // steering angle
        while(s <= 1)
        {
            WheelFL.steerAngle = MaxSteeringAngle * s;
            WheelFR.steerAngle = MaxSteeringAngle * s;
            s = s + 0.1f;
            yield return null;
        }
    }
}
