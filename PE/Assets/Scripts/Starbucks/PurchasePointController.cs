using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasePointController : MonoBehaviour {

    public bool customerPurchasing;
    string currentCustomer;

	// Use this for initialization
	void Start () {
        customerPurchasing = false;
        currentCustomer = "";
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something enterd");
            if (other.tag == "NPCCoffee")
            {
                TestNPCQueue npcController = other.transform.parent.parent.GetComponent<TestNPCQueue>();
                if (!npcController.PurchasedCoffee)
                {
                    currentCustomer = other.transform.parent.parent.name;
                    //Debug.Log(other.transform.parent.parent.name + " entered purchase zone");
                    customerPurchasing = true;
                    npcController.PurchasedCoffee = true;
                    other.transform.root.GetComponent<QueueManager>().PurchaseCoffee(other.transform.parent.parent);
                }
            }



    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.parent.parent.name == currentCustomer)
    //        customerPurchasing = false;
    //}
}
