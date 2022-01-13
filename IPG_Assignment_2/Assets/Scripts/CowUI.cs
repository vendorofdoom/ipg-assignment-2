using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowUI : MonoBehaviour
{
    public CanvasGroup cowStatsUI;
    public TMPro.TextMeshProUGUI textName;
    public TMPro.TextMeshProUGUI textAbout;
    public TMPro.TextMeshProUGUI textPurpleFlowers;
    public TMPro.TextMeshProUGUI textPinkFlowers;
    public TMPro.TextMeshProUGUI textOrangeFlowers;

    public Button purpleButton;
    public Button pinkButton;
    public Button orangeButton;

    public FlowersCollection hpFlowers;
    public FlowerHUD flowerHUD;
    public Cow activeCow;

    public float reachDist;


    private void Update()
    {

        if (activeCow != null && cowStatsUI.gameObject.activeSelf)
        {
            bool canGiveFlowers = (Vector3.Distance(hpFlowers.transform.position, activeCow.transform.position) < reachDist);
            purpleButton.interactable = canGiveFlowers && !activeCow.flowerCollection.HasEnough(Flower.FlowerType.Purple);
            pinkButton.interactable = canGiveFlowers && !activeCow.flowerCollection.HasEnough(Flower.FlowerType.Pink);
            orangeButton.interactable = canGiveFlowers && !activeCow.flowerCollection.HasEnough(Flower.FlowerType.Orange);
        }
        
    }

    public void UpdateUI()
    {
        textName.text = activeCow.cowName;
        textAbout.text = activeCow.currStatus;
        textPurpleFlowers.text = activeCow.flowerCollection.Status(Flower.FlowerType.Purple, true);
        textPinkFlowers.text = activeCow.flowerCollection.Status(Flower.FlowerType.Pink, true);
        textOrangeFlowers.text = activeCow.flowerCollection.Status(Flower.FlowerType.Orange, true);
    }

    public void DisplayUI()
    {
        if (cowStatsUI.gameObject.activeSelf != true)
        {
            cowStatsUI.gameObject.SetActive(true);
        }
    }

    public void PurpleButton()
    {
        GiveFlowerUIButton(Flower.FlowerType.Purple);
    }

    public void PinkButton()
    {
        GiveFlowerUIButton(Flower.FlowerType.Pink);
    }

    public void OrangeButton()
    {
        GiveFlowerUIButton(Flower.FlowerType.Orange);
    }

    private void GiveFlowerUIButton(Flower.FlowerType flowerType)
    {

        if (activeCow == null)
        {
            return;
        }

        if (hpFlowers.HasEnough(flowerType, 1) && !activeCow.flowerCollection.HasEnough(flowerType))
        {
            hpFlowers.RemoveFlower(flowerType);
            activeCow.flowerCollection.AddFlower(flowerType);
            UpdateUI();
            flowerHUD.UpdateUI();
            activeCow.hearts.Emit(1);
        }

        // TODO: not sure if this is the best place to put this check, maybe move to cow update?
        if (activeCow.flowerCollection.HasEnough(Flower.FlowerType.Purple) && activeCow.flowerCollection.HasEnough(Flower.FlowerType.Pink) && activeCow.flowerCollection.HasEnough(Flower.FlowerType.Orange))
        {
            activeCow.WearFlowerCrown();
        }

    }

}
