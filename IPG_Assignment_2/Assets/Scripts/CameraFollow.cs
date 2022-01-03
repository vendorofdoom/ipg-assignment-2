using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;

    public float xMin;
    public float xMax;

    public float zMin;
    public float zMax;

    private void Start()
    {
        transform.position = target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position;
        targetPos.x = Mathf.Clamp(targetPos.x, xMin, xMax);
        targetPos.z = Mathf.Clamp(targetPos.z, zMin, zMax);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
