using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private int woodStart = 0;
    [SerializeField] private int goldStart = 0;
    [SerializeField] private int woodRange = 50;

    [SerializeField] private TextMeshProUGUI woodResource;
    [SerializeField] private TextMeshProUGUI goldResource;
    private int currentWood;
    private int currentGold;

    private void Awake() {
        base.Awake();
        currentWood = woodStart;
        currentGold = goldStart;    
    }

    private void Update() {
        woodResource.text = "Wood: " + currentWood;
        goldResource.text = "Gold: " + currentGold;
    }


    public void AddWood()
    {
        currentWood += Random.Range(woodRange / 2, woodRange);
    }

    public void SubWood(int woodAmount)
    {
        if (currentWood <= 0)
            return;
        woodAmount = 1;
        currentWood -= woodAmount;
    }

    public void AddGold(int goldAmount)
    {
        goldAmount = 25;
        currentGold += goldAmount;
    }

    public void SubGold(int goldAmount)
    {
        if (currentGold <= 0)
            return;        
        goldAmount = 1;
        currentGold -= goldAmount;
    }
}
