using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    Queue<Transform> purchaseQueue;
    Queue<Transform> pickupQueue;
    private Transform lastPurchaseNPC;
    private Transform newPurchaseNPC;
    private Transform lastPickupNPC;
    private Transform newPickupNPC;

    float newCustomerTime; // how often NPC joins line
    public float PurchaseTime; // How long customer takes to order
    public float PickupTime;
    public int MaxCustomers;
    public int TotalCustomers; // Total number of customers

    public Transform NPC;
    public Transform PurchasePoint; // NPC purchase point
    public Transform PickupPoint; // NPC pickup point
    public Transform SpawnPoint; // NPC spawn point


    public float Smooth; // Smoothing value
                         // Use this for initialization
    void Start()
    {
        purchaseQueue = new Queue<Transform>();
        pickupQueue = new Queue<Transform>();
        StartCoroutine(NewCustomerGenerator());
    }
    IEnumerator NewCustomerGenerator()
    {
        int i = 0;
        yield return new WaitForSeconds(1f);
        while(i < TotalCustomers)
        {
            if (purchaseQueue.Count < MaxCustomers && pickupQueue.Count < MaxCustomers)
            {
                Transform newNPC = (Transform)Instantiate(NPC, SpawnPoint.position, SpawnPoint.rotation);
                newNPC.name = "NPC " + i.ToString();
                TestNPCQueue NPCController = newNPC.GetComponent<TestNPCQueue>();
                newNPC.parent = PurchasePoint;
                NPCController.PlaceInLine = purchaseQueue.Count;
                if (i == TotalCustomers - 1)
                    NPCController.PlaceInLine = purchaseQueue.Count + 3;
                NPCController.ChangeLine(TestNPCQueue.Line.purchase);
                purchaseQueue.Enqueue(newNPC);
                //NPCController.MoveToLine(PurchasePoint);
                newCustomerTime = Random.Range(2, 6);
                i++;
            }
            yield return new WaitForSeconds(newCustomerTime);
        }
    }
    public void PurchaseCoffee(Transform _npc)
    {
        StartCoroutine(_PurchaseCoffee(_npc));
    }
    IEnumerator _PurchaseCoffee(Transform _npc)
    {
        //Transform npc = purchaseQueue.Peek();
        TestNPCQueue npcController = _npc.GetComponent<TestNPCQueue>();
        //Debug.Log(npc.name + " buying coffee...");
        yield return new WaitForSeconds(npcController.TimeToOrder);
        //Debug.Log(npc.name + " bought coffee.");
        purchaseQueue.Dequeue();
        pickupQueue.Enqueue(_npc);
        Debug.Log(_npc.name + " added to pickupQueue");
        _npc.parent = PickupPoint;
        npcController.PlaceInLine = pickupQueue.Count-1;
        npcController.ChangeLine(TestNPCQueue.Line.pickup);
        npcController.MoveToLine(PickupPoint, TestNPCQueue.Line.pickup);
        int i = 0;
        foreach (Transform _npcs in purchaseQueue.ToArray())
        {
            TestNPCQueue _npcsController = _npcs.GetComponent<TestNPCQueue>();
            //Debug.Log("Moving " + _npc.name + " up one spot");
            _npcsController.PlaceInLine = i;
            _npcsController.MoveUp();
            i++;
        }
        yield return null;
    }
    public void PickupCoffee(Transform _npc)
    {
        StartCoroutine(_PickupCoffee(_npc));
    }
    IEnumerator _PickupCoffee(Transform _npc)
    {
        //Transform npc = pickupQueue.Peek();
        //Debug.Log(npc.name + " is the first in pickupQueue");
        TestNPCQueue npcController = _npc.GetComponent<TestNPCQueue>();
        Debug.Log(_npc.name + " picking up coffee...");
        yield return new WaitForSeconds(npcController.TimeToPickup);
        //Debug.Log(npc.name + " picked up coffee.");
        float f = 0;
        while (f <= 1)
        {
            _npc.GetComponentInChildren<Animator>().SetFloat("HasCoffee", f);
            f = f + 0.1f;
            yield return null;
        }
        npcController.Coffee.gameObject.SetActive(true);
        pickupQueue.Dequeue();
        _npc.parent = null;
        Debug.Log(_npc.name + " removed from pickupQueue");
        npcController.MoveToLine(SpawnPoint, TestNPCQueue.Line.pickup);
        npcController.FadeOutAndDie();
        int i = 0;
        foreach (Transform _npcs in pickupQueue.ToArray())
        {
            TestNPCQueue _npcsController = _npcs.GetComponent<TestNPCQueue>();
            //Debug.Log("Moving " + _npc.name + " up one spot");
            _npcsController.PlaceInLine = i;
            _npcsController.MoveUp();
            i++;
        }
        //Destroy(npc.gameObject);
        yield return null;
    }
    IEnumerator PickupLineManager()
    {
        while(true)
        {
            while (pickupQueue.Count > 0)
            {
                Transform firstNPC = pickupQueue.Peek();
                TestNPCQueue NPCController = firstNPC.GetComponent<TestNPCQueue>();
                yield return new WaitForSeconds(NPCController.TimeToPickup);
                pickupQueue.Dequeue();
                Debug.Log("Destroying " + firstNPC.name);
                Destroy(firstNPC.gameObject);
                foreach (Transform NPC in pickupQueue.ToArray())
                {
                    TestNPCQueue _NPCController = NPC.GetComponent<TestNPCQueue>();
                    _NPCController.MoveUp();
                }
            }
            yield return null;
        }
    }
    public void EnqueuePurchaseLine(Transform _transform)
    {
        purchaseQueue.Enqueue(_transform);
    }
    public void DequeuePurchaseLine()
    {
        purchaseQueue.Dequeue();
    }
    public void EnqueuePickupLine(Transform _transform)
    {
        pickupQueue.Enqueue(_transform);
    }
    public void DequeuePickupLine()
    {
        pickupQueue.Dequeue();
    }


}
