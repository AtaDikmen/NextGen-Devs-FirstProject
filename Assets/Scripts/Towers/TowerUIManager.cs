using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUIManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private AttackSpeedButton attackSpeedButton;
    [SerializeField] private SpeedButton speedButton;
    [SerializeField] private HealthButton healthButton;
    [SerializeField] private List<GameObject> canvasList;
    private int goldCost = 5;
    private int woodCost = 50;
    private void Awake() {
        
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
        if(ResourceManager.Instance.GetGold() != goldCost && ResourceManager.Instance.GetWood() != woodCost)
            return;
        
        attackSpeedButton.isDone = true;

        player.UpgradeAttackSpeed(3f);
        CloseAttackSpeedCanvas();

        ResourceManager.Instance.UpdateResourceTexts();
    }

    public void BuySpeedButton()
    {
        if(ResourceManager.Instance.GetGold() != goldCost && ResourceManager.Instance.GetWood() != woodCost)
            return;
            
        speedButton.isDone = true;

        player.UpgradeSpeed(3f);
        CloseSpeedCanvas();
        ResourceManager.Instance.UpdateResourceTexts();
    }

    public void BuyHealthButton()
    {
        if(ResourceManager.Instance.GetGold() != goldCost && ResourceManager.Instance.GetWood() != woodCost)
            return;

        healthButton.isDone = true;

        player.UpgradeHealth(50f);
        CloseHealthCanvas();
        ResourceManager.Instance.UpdateResourceTexts();
    }
}
