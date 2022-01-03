using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alan's game: https://youtu.be/_Oi6MuesXnw 

public class Rotate : MonoBehaviour
{
    public Vector3 speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
