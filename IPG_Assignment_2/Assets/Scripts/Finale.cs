using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finale : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    public Transform endTriggerSubject;
    public Transform endTriggerTarget;
    public float endTriggerDist;

    private void Start()
    {
        text.CrossFadeAlpha(0f, 0f, true);
        text.CrossFadeAlpha(1f, 5f, false);
    }

    private void Update()
    {
        if (Vector3.Distance(endTriggerSubject.position, endTriggerTarget.position) <= endTriggerDist)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
