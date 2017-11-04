using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KayakAnimator : MonoBehaviour {

    Animator animator;
    public Transform Player;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = Player.GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	}
}
