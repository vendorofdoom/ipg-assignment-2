using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadeWalk : MonoBehaviour
{
    public Transform target;
    [Range(0f, 1f)]
    public float speed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
    }

}
