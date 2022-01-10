using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public string cowName;
    public List<string> cowStatusOptions;
    private string currStatus;
    public bool wearingFlowers;
    public GameObject flowerCrown;

    public int requiredPurpleFlowers;
    public int requiredPinkFlowers;
    public int requiredOrangeFlowers;

    public int numPurpleFlowers;
    public int numPinkFlowers;
    public int numOrangeFlowers;

    public ParticleSystem hearts;
    public CanvasGroup cowStatsUI;

    public TMPro.TextMeshProUGUI textName;
    public TMPro.TextMeshProUGUI textAbout;
    public TMPro.TextMeshProUGUI textPurpleFlowers;
    public TMPro.TextMeshProUGUI textPinkFlowers;
    public TMPro.TextMeshProUGUI textOrangeFlowers;


    // TODO: Split some functionality out into CowInventory script perhaps?

    // TODO: Implement ReceiveFlower()

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

        textName.text = cowName;
        textAbout.text = currStatus;
        textPurpleFlowers.text = numPurpleFlowers.ToString() + "/" + requiredPurpleFlowers.ToString();
        textPinkFlowers.text = numPinkFlowers.ToString() + "/" + requiredPinkFlowers.ToString();
        textOrangeFlowers.text = numOrangeFlowers.ToString() + "/" + requiredOrangeFlowers.ToString();
        
        
        if (cowStatsUI.gameObject.activeSelf != true)
        {
            cowStatsUI.gameObject.SetActive(true);
        }
            
    }

    private void Update()
    {
        flowerCrown.SetActive(wearingFlowers); // TODO: move out of update later
    }
}
