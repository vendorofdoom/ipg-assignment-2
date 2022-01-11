using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersCollection : MonoBehaviour
{
    private Dictionary<Flower.FlowerType, int[]> collection;
    private int countIdx = 0;
    private int requiredIdx = 1;

    [SerializeField]
    private int requiredPurpleFlowers;
    [SerializeField]
    private int requiredPinkFlowers;
    [SerializeField]
    private int requiredOrangeFlowers;

    public void Awake()
    {
        collection = new Dictionary<Flower.FlowerType, int[]>();
        collection.Add(Flower.FlowerType.Purple, new int[] { 0, requiredPurpleFlowers});
        collection.Add(Flower.FlowerType.Pink, new int[] { 0, requiredPinkFlowers});
        collection.Add(Flower.FlowerType.Orange, new int[] { 0, requiredOrangeFlowers});
    }    

    public void AddFlower(Flower.FlowerType flowerType)
    {
        collection[flowerType][countIdx]++;
    }

    public void RemoveFlower(Flower.FlowerType flowerType)
    {
        collection[flowerType][countIdx]--;
    }

    public string Status(Flower.FlowerType flowerType, bool includeRequired = false)
    {
        if (includeRequired)
        {
            return collection[flowerType][countIdx].ToString() + "/" + collection[flowerType][requiredIdx].ToString();
        }
        else
        {
            return collection[flowerType][countIdx].ToString();
        }
            
    }

    public bool HasEnough(Flower.FlowerType flowerType)
    {
        return collection[flowerType][countIdx] >= collection[flowerType][requiredIdx];
    }

    public bool HasEnough(Flower.FlowerType flowerType, int requiredAmount)
    {
        return collection[flowerType][countIdx] >= requiredAmount;
    }

}
