using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPointController : MonoBehaviour {

    // Use this for initialization
    private int numObjects;

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "NPCCoffee")
        {
            TestNPCQueue npcController = other.transform.parent.parent.GetComponent<TestNPCQueue>();
            if (npcController.PurchasedCoffee)
            {
                if(!npcController.PickedupCoffee)
                {
                    //Debug.Log(other.transform.parent.parent.name + " entered pickup zone");
                    npcController.PickedupCoffee = true;
                    other.transform.root.GetComponent<QueueManager>().PickupCoffee(other.transform.parent.parent);
                }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {

    }
}
