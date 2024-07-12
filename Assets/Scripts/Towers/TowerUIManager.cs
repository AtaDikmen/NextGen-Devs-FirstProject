using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    private ResourceManager resourceManager;

    [SerializeField] private AttackSpeedButton attackSpeedButton;
    [SerializeField] private SpeedButton speedButton;
    [SerializeField] private HealthButton healthButton;
    [SerializeField] private List<GameObject> canvasList;
    private int goldCost = 5;
    private int woodCost = 50;
    private void Awake() {
        resourceManager = ResourceManager.Instance;
    }

    public void CloseAttackSpeedCanvas()
    {
        canvasList[0].gameObject.SetActive(false);
    }

    public void CloseSpeedCanvas()
    {
        canvasList[1].gameObject.SetActive(false);
    }

    public void CloseHealthCanvas()
    {
        canvasList[2].gameObject.SetActive(false);
    }

    public void BuyAttackSpeedButton()
    {
        if (resourceManager.GetGold() >= goldCost && resourceManager.GetWood() >= woodCost)
        {
            resourceManager.SubGold(goldCost);
            resourceManager.SubWood(woodCost);

            attackSpeedButton.isDone = true;

            player.UpgradeAttackSpeed(3f);
            CloseAttackSpeedCanvas();

            resourceManager.UpdateResourceTexts();

            attackSpeedButton.CheckIsDone();
        }
        else
        {
            Debug.LogWarning("Not enough gold or wood");
        }
    }

    public void BuySpeedButton()
    {
        if (resourceManager.GetGold() >= goldCost && resourceManager.GetWood() >= woodCost)
        {
            resourceManager.SubGold(goldCost);
            resourceManager.SubWood(woodCost);

            speedButton.isDone = true;

            player.UpgradeSpeed(3f);
            CloseSpeedCanvas();
            resourceManager.UpdateResourceTexts();

            speedButton.CheckIsDone();

        }
        else
        {
            Debug.LogWarning("Not enough gold or wood");
        }
        
    }

    public void BuyHealthButton()
    {
        if (resourceManager.GetGold() >= goldCost && resourceManager.GetWood() >= woodCost)
        {
            resourceManager.SubGold(goldCost);
            resourceManager.SubWood(woodCost);

            healthButton.isDone = true;

            player.UpgradeHealth(50f);
            CloseHealthCanvas();
            resourceManager.UpdateResourceTexts();

            healthButton.CheckIsDone();
        }
        else
        {
            Debug.LogWarning("Not enough gold or wood");
        }
    }
}
