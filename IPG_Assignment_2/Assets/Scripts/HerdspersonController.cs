using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerdspersonController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    [Header("Speed Modifiers")]
    private float normalSpeed;
    private float normalAngularSpeed;
    private float normalAcceleration;
    public bool speedModified = false;
    public float speedModifierTimeout = 5f;

    [Header("Alpine Horn")]
    public bool isPlayingHorn = false;
    public AudioSource audioSource;
    public AudioClip hornSound;
    public GameObject alpineHorn;


    private float time;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        normalSpeed = navMeshAgent.speed;
        normalAcceleration = navMeshAgent.acceleration;
        normalAngularSpeed = navMeshAgent.angularSpeed;
    }

    private void Update()
    {
        if (speedModified)
        {
            if (time < speedModifierTimeout)
            {
                time = Mathf.Min(speedModifierTimeout, time + Time.deltaTime);
            }
            else
            {
                SetNormalSpeed();
            }

        }

        animator.SetBool("isWalking", navMeshAgent.velocity != Vector3.zero);

    }
    public void SetNewTarget(Vector3 Position)
    {
        navMeshAgent.SetDestination(Position);
    }

    public void CancelMove()
    {
        navMeshAgent.ResetPath();
    }

    private void SetNormalSpeed()
    {
        navMeshAgent.speed = normalSpeed;
        navMeshAgent.angularSpeed = normalAngularSpeed;
        navMeshAgent.acceleration = normalAcceleration;
        speedModified = false;
        time = speedModifierTimeout;
    }

    public void ModifySpeed(float speedModifier)
    {
        navMeshAgent.speed = normalSpeed * speedModifier;
        navMeshAgent.angularSpeed = normalAngularSpeed * speedModifier;
        navMeshAgent.acceleration = normalAcceleration * speedModifier;
        speedModified = true;
        time = 0f;
    }

    public void ScaleVelocity(float scaleFactor)
    {
        navMeshAgent.velocity *= scaleFactor;
    }

    public void PlayAlpineHorn()
    {
        animator.SetTrigger("playHorn");
    }

    private void HornStart()
    {
        isPlayingHorn = true;
        audioSource.PlayOneShot(hornSound); // Triggered by animation event
        alpineHorn.SetActive(true);
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
    }

    private void HornStop()
    {
        // Triggered by animation event
        isPlayingHorn = false;
        alpineHorn.SetActive(false);
        navMeshAgent.isStopped = false;
    }
}

