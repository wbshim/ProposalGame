using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public bool objectHit = false;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something hit a tree");
        //if(!objectHit)
        //{
            if (other.tag == "Player")
            {
                objectHit = true;
                Debug.Log(objectHit);
                Debug.Log("Player hit tree!");
                GameObject.Find("Player").GetComponent<SkiPlayerController>().Die();
            }
        //}

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    objectHit = false;
    //    Debug.Log(objectHit);
    //}
}
