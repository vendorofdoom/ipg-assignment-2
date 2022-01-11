using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cow : MonoBehaviour
{
    [Header("Info/Status")]
    public string cowName;
    public List<string> cowStatusOptions;
    public string currStatus;

    [Header("UI")]
    public CowUI cowUI;
    [SerializeField]
    private ParticleSystem hearts;

    [Header("Flowers")]
    public FlowersCollection flowerCollection;
    [SerializeField]
    private GameObject flowerCrown;

    [Header("NavMesh")]
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private CowBehaviour behaviour;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private enum CowBehaviour
    {
        Wander,
        Follow
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currStatus = cowStatusOptions[Random.Range(0, cowStatusOptions.Count)];
        InvokeRepeating("updateStatus", 30f, 30f);
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


    private void updateStatus()
    {
        currStatus = cowStatusOptions[Random.Range(0, cowStatusOptions.Count)];
    }

    private void OnMouseDown()
    {
        hearts.Emit(1);
        cowUI.activeCow = this;
        cowUI.UpdateUI();
        cowUI.DisplayUI();
    }

    public void WearFlowerCrown()
    {
        flowerCrown.SetActive(true);
    }

}
