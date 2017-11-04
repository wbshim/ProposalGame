using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TestNPCQueue : MonoBehaviour {

    NavMeshAgent agent;
    Animator anim;
    const float locomotionAnimationSmoothTime = .1f; // Animation smooth time

    public LayerMask movementMask; // Tell NPC to move on ground
    public enum Line { purchase, pickup };
    Line currentLine;
    public int PlaceInLine;
    public float Smooth;
    public float TimeToOrder; // Time customer takes to order coffee
    public float TimeToPickup;
    public bool PurchasedCoffee = false;
    public bool PickedupCoffee = false;
    public Transform Coffee;

    Transform frontOfLine; // The front of the line
    IEnumerator moveCoroutine;

    // Reference Queuemanager to enqueue and dequeue
    QueueManager queueManager;

    public Material Transparent;

    // Use this for initialization
    void Start ()
    {
        queueManager = GameObject.FindGameObjectWithTag("QueueManager").GetComponent<QueueManager>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //frontOfLine = (Transform)GameObject.FindGameObjectWithTag("PurchasePoint").GetComponent<Transform>();
        frontOfLine = GameObject.FindGameObjectWithTag("PurchasePoint").transform;
        TimeToOrder = Random.Range(2f, 5f);
        TimeToPickup = Random.Range(3f, 5f);
        MoveToLine(frontOfLine, Line.purchase);

	}
    private void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
    public void MoveToLine(Transform _frontOfLine, Line line)
    {
        frontOfLine = _frontOfLine;
        //moveCoroutine = _Move();
        //StartCoroutine(moveCoroutine);
        Vector3 targetPosition = frontOfLine.position - Vector3.forward * 2.5f * PlaceInLine;
        agent.SetDestination(targetPosition);
    }
    public void MoveUp()
    {
        Vector3 targetPosition = frontOfLine.position - Vector3.forward * 2.5f * PlaceInLine;
        agent.SetDestination(targetPosition);
    }
    public void ChangeLine(Line _currentLine)
    {
        currentLine = _currentLine;
    }
    IEnumerator _Move()
    {
        Vector3 targetPosition = frontOfLine.position - Vector3.forward * 1.5f * PlaceInLine;
        while(Vector3.Distance(transform.position, targetPosition) > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Smooth * Time.deltaTime);
            yield return null;
        }
    }
    public void FadeOutAndDie()
    {
        StartCoroutine(_FadeOutAndDie());
    }
    IEnumerator _FadeOutAndDie()
    {
        float _a = 1f;
        Renderer renderer = GetComponentInChildren<Renderer>();
        Color c = renderer.material.color;
        yield return new WaitForSeconds(1f);
        renderer.material = Transparent;
        while (renderer.material.color.a >= 0)
        {
            Color _c = new Color(c.r, c.g, c.b, _a);
            renderer.material.color = _c;
            _a = _a - 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }
}
