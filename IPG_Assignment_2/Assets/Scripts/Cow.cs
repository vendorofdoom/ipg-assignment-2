using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject flowerCrown;

    private void Start()
    {
        currStatus = cowStatusOptions[Random.Range(0, cowStatusOptions.Count)];
        InvokeRepeating("updateStatus", 30f, 30f);
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
