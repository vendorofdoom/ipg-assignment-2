using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerdspersonController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetNewTarget(Vector3 Position)
    {
        navMeshAgent.SetDestination(Position);
    }

    private void Update()
    {
        animator.SetBool("isWalking", navMeshAgent.velocity != Vector3.zero);
    }
}
