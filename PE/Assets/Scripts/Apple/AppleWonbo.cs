using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class AppleWonbo : MonoBehaviour {

    public Transform Player;
    public float MoveSpeed;
    public float Smooth;
    public float Direction;
    
    Animator animator;
    Quaternion targetRotation;
    Rigidbody rigidBody;

    public bool GameActive = false;

    // Use this for initialization
    void Start () {
        rigidBody = Player.GetComponent<Rigidbody>();
        animator = Player.GetComponent<Animator>();
        Direction = 0;
        GameActive = false;
        animator.SetFloat("Speed", 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(GameActive)
        {
            Vector3 moveInput = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, 0);
            if (moveInput.magnitude > 0)
                targetRotation = Quaternion.LookRotation(moveInput);
            else
                targetRotation = Quaternion.LookRotation(Vector3.forward);
            Vector3 moveVelocity = moveInput.normalized * MoveSpeed;
            rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime);
            Player.rotation = Quaternion.Lerp(Player.rotation, targetRotation, Smooth * Time.fixedDeltaTime);
            animator.SetFloat("Speed", moveVelocity.magnitude / MoveSpeed);
        }
        else
        {
            Vector3 moveInput = new Vector3(0, 0, 0);
            Vector3 moveVelocity = moveInput.normalized * MoveSpeed;
            animator.SetFloat("Speed", moveVelocity.magnitude / MoveSpeed);
        }

    }
    public void MoveLeft()
    {
        Direction = -1;
    }
    public void MoveRight()
    {
        Direction = 1;
    }
    public void Stop()
    {
        Direction = 0;
    }
}
