using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerButtons : MonoBehaviour
{
    public FlowersCollection hpInventory;
    public Cow activeCow;

    public void GiveFlower(Flower.FlowerType flowerType)
    {
        if (hpInventory.HasEnough(flowerType, 1) && (activeCow != null) && !activeCow.flowerCollection.HasEnough(flowerType)) 
        {
            hpInventory.RemoveFlower(flowerType);
            activeCow.flowerCollection.AddFlower(flowerType);
        }
    }

}
