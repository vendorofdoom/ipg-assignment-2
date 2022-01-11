using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerHUD : MonoBehaviour
{
    public FlowersCollection flowersCollection;

    public TMPro.TextMeshProUGUI purpleCountText;
    public TMPro.TextMeshProUGUI pinkCountText;
    public TMPro.TextMeshProUGUI orangeCountText;

    public void UpdateUI()
    {
        purpleCountText.text = flowersCollection.Status(Flower.FlowerType.Purple, false);
        pinkCountText.text = flowersCollection.Status(Flower.FlowerType.Pink, false);
        orangeCountText.text = flowersCollection.Status(Flower.FlowerType.Orange, false);
    }

}
