using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour {
    public float YOffset = 0;
    public float RelativeSpeed = 1f;
    float speed;
    SkiGameManager gameManager;
    // Update is called once per frame
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
    }

    void FixedUpdate ()
    {
        speed = gameManager.Speed * RelativeSpeed;
        //transform.Translate(0, speed * Time.fixedDeltaTime, 0);
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.World);
	}
}
