using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class AppleJiYeon : MonoBehaviour {
    public Transform TargetPosition;

    Rigidbody rb;
    Animator anim;
    NavMeshAgent agent;
    const float locomotionAnimationSmoothTime = .1f;
    public float rotationSpeed = 10f;

    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
    public void IntroMove()
    {
        MoveToPoint(TargetPosition.position);
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.updateRotation = true;
        agent.SetDestination(point);
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
        renderer.material.shader = Shader.Find("Transparent/Diffuse");
        while (renderer.material.color.a >= 0)
        {
            Color _c = new Color(c.r, c.g, c.b, _a);
            renderer.material.color = _c;
            _a = _a - 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }
    public void RotateTowardsTarget(Transform target)
    {
        StartCoroutine(_RotateTowardsTarget(target));
    }
    IEnumerator _RotateTowardsTarget(Transform target)
    {
        agent.updateRotation = false;
        float startTime = Time.time;
        float elapsedTime = Time.time - startTime;
        while(elapsedTime < 1)
        {
            elapsedTime = Time.time - startTime;
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
    }
}
