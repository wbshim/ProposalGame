using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotorStarbucks : MonoBehaviour {

    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if(target!=null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }
    public void FollowTarget(StarbucksJiYeon newTarget)
    {
        target = newTarget.InteractionTransform;
    }
    public void StopFollowingTarget()
    {
        target = null;
    }
}
