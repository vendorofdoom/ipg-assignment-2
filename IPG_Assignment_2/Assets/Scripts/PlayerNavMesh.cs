using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform target;

    public bool isMoving;

    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        navMeshAgent.destination = target.position;
        animator.SetBool("isWalking", navMeshAgent.hasPath);
    }
}
