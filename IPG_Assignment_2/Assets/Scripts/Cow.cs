using UnityEngine;

public class Cow : MonoBehaviour
{
    public string cowName;
    public int cowAge;
    public string cowStatus;
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

    private void OnMouseDown()
    {
        hearts.Emit(1);

        textName.text = cowName;
        textAbout.text = cowName + ", aged " + cowAge.ToString() + ", is feeling " + cowStatus;
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
