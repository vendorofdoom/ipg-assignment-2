using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Signpost : MonoBehaviour
{
    private void OnMouseDown()
    {
        // TODO: Only allow if all cows have their flower crowns on
        SceneManager.LoadScene("Ending");
    }
}
