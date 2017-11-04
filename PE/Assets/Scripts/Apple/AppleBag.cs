using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBag : MonoBehaviour {

    public Transform AppleGameManager;
    AppleGameManager appleGameManager;
	// Use this for initialization
	void Start () {
        appleGameManager = AppleGameManager.GetComponent<AppleGameManager>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Apple")
        {
            Debug.Log("Apple entered bag");
            CollectApple(other.gameObject);
        }
        else
        {
            Debug.Log("Something entered bag");
        }
    }
    void CollectApple(GameObject apple)
    {
        Destroy(apple);
        appleGameManager.IncreaseScore();
    }
}
