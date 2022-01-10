using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour
{
    public GameObject hp;
    public GameObject cow1;
    public GameObject cow2;
    public GameObject cow3;

    public Vector3 endPos;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        hp.transform.position = Vector3.Lerp(hp.transform.position, endPos, speed * Time.deltaTime);
        cow1.transform.position = Vector3.Lerp(cow1.transform.position, endPos, speed * Time.deltaTime);
        cow2.transform.position = Vector3.Lerp(cow2.transform.position, endPos, speed * Time.deltaTime);
        cow3.transform.position = Vector3.Lerp(cow3.transform.position, endPos, speed * Time.deltaTime);
    }
}
