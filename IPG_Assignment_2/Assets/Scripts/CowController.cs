using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private CowBehaviour behaviour;

    private enum CowBehaviour
    {
        Wander,
        Follow
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (behaviour)
        {
            case CowBehaviour.Wander:
                //navMeshAgent.isStopped = true;
                break;
            case CowBehaviour.Follow:
                navMeshAgent.SetDestination(target.position);
                break;
        }

        animator.SetBool("isWalking", navMeshAgent.velocity != Vector3.zero);
    }
}
