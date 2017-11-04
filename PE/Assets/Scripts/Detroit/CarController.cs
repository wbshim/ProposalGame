using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {
    public Transform car;
    public float speed;
    public float TargetVelocity;
    public float acceleration;

    Rigidbody rigidBody;
    Vector3 position;
    Vector3 velocity;

	// Use this for initialization
	void Start () {
        rigidBody = car.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        velocity = car.forward * speed;
        rigidBody.MovePosition(car.position + velocity * Time.fixedDeltaTime);
	}
}
