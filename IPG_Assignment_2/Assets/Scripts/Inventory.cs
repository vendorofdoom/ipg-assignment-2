using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> flowerCount;

    public CanvasGroup InventoryUI;
    public TMPro.TextMeshProUGUI purpleCountText;
    public TMPro.TextMeshProUGUI pinkCountText;
    public TMPro.TextMeshProUGUI orangeCountText;

    private void Awake()
    {
        flowerCount = new Dictionary<string, int>();
        foreach(string flowerType in System.Enum.GetNames(typeof(Flower.FlowerType)))
        {
            flowerCount.Add(flowerType, 0);
        }

        UpdateUI();
    }

    public void addFlower(Flower.FlowerType flowerType)
    {
        flowerCount[flowerType.ToString()] += 1;
        Debug.Log("Added flower of type: " + flowerType.ToString() + " Total count: " + flowerCount[flowerType.ToString()]);
        UpdateUI();
    }

    private void UpdateUI()
    {
        purpleCountText.text = flowerCount[Flower.FlowerType.Purple.ToString()].ToString();
        pinkCountText.text = flowerCount[Flower.FlowerType.Pink.ToString()].ToString();
        orangeCountText.text = flowerCount[Flower.FlowerType.Orange.ToString()].ToString();
    }

    public void DisplayUI()
    {
        if (InventoryUI.gameObject.activeSelf != true)
        {
            InventoryUI.gameObject.SetActive(true);
        }
    }

}
