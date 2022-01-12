using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowPat : MonoBehaviour
{
    public float slowDownModifier = 0.8f;
    public float speedUpModifier = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Herdsperson"))
        {
            other.GetComponent<HerdspersonController>().ScaleVelocity(0.5f);
            other.GetComponent<HerdspersonController>().ModifySpeed(slowDownModifier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Herdsperson"))
        {
            other.GetComponent<HerdspersonController>().ModifySpeed(speedUpModifier);
        }
    }
}
