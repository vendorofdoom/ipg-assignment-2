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
    public ParticleSystem hearts;

    [Header("Flowers")]
    public FlowersCollection flowerCollection;
    [SerializeField]
    private GameObject flowerCrown;
    public bool isWearingCrown = false;

    [Header("NavMesh")]
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform signPost;
    [SerializeField]
    private Transform herdsPerson;
    [SerializeField]
    private CowBehaviour behaviour;
    public List<Vector3> wanderDestinations;
    public float wanderTimeoutMin = 1f;
    public float wanderTimeoutMax = 10f;
    private float timeout;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    [Header("Sound")]
    [SerializeField]
    private AudioSource audioSource;
    
    private enum CowBehaviour
    {
        Wander,
        GoToSignpost,
        Beckoned
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currStatus = cowStatusOptions[Random.Range(0, cowStatusOptions.Count)];
        InvokeRepeating("updateStatus", 30f, 30f);
        timeout = Random.Range(0f, 1.5f);
    }

    private void Update()
    {
        switch (behaviour)
        {
            case CowBehaviour.Wander: // https://docs.unity3d.com/Manual/nav-AgentPatrol.html

                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 1.5f) // TODO: Assuming we don't need to handle case where we've swapped from wander to follow behaviour (i.e. cow will continue to old follow dest and not switch until there) but need to check in testing.
                {
                    if (timeout < 0.01f)
                    {
                        GoToNextPoint();
                        timeout = Random.Range(wanderTimeoutMin, wanderTimeoutMax);
                    }
                    else
                    {
                        timeout = Mathf.Max(0f, timeout - Time.deltaTime);
                    }

                }
                break;

            case CowBehaviour.GoToSignpost:
                navMeshAgent.SetDestination(signPost.position);
                break;

            case CowBehaviour.Beckoned:
                navMeshAgent.SetDestination(herdsPerson.position);
                if (timeout < 0.01f)
                {
                    if (isWearingCrown)
                    {
                        behaviour = CowBehaviour.GoToSignpost;
                    }
                    else
                    {
                        behaviour = CowBehaviour.Wander;
                    }
                }
                else
                {
                    timeout = Mathf.Max(0f, timeout - Time.deltaTime);
                }
                break;
        }

        
        animator.SetBool("isWalking", navMeshAgent.velocity != Vector3.zero);
    }


    private void updateStatus() // called by InvokeRepeating in Awake()
    {
        currStatus = cowStatusOptions[Random.Range(0, cowStatusOptions.Count)];
    }

    public void Select()
    {
        hearts.Emit(1);
        StopAndSayMoo();
        cowUI.activeCow = this;
        cowUI.UpdateUI();
        cowUI.DisplayUI();
    }

    public void WearFlowerCrown()
    {
        flowerCrown.SetActive(true);
        isWearingCrown = true;
        behaviour = CowBehaviour.GoToSignpost; // TODO: Implement follow herdsperson logic
    }

    private void GoToNextPoint() // https://docs.unity3d.com/Manual/nav-AgentPatrol.html
    {
        if (wanderDestinations.Count == 0)
            return;

        navMeshAgent.destination = wanderDestinations[Random.Range(0, wanderDestinations.Count)];
    }

    private void StopAndSayMoo()
    {
        navMeshAgent.ResetPath();
        timeout = 5f;
        animator.SetTrigger("doMoo");
    }

    private void SayMoo()
    {
        audioSource.PlayOneShot(audioSource.clip); // Triggered by animation event
    }

    public void Beckon()
    {
        timeout = 10f;
        behaviour = CowBehaviour.Beckoned;
    }

}