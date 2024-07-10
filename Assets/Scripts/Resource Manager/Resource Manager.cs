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

    private void Update()
    {
        currentWood = woodStart;
        currentGold = goldStart;
    }

    public void AddWood()
    {
        currentWood += Random.Range(woodRange / 2, woodRange);
        woodResource.text = currentWood.ToString();
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
        currentGold += goldAmount;
        goldResource.text = currentGold.ToString();
    }

    public void SubGold(int goldAmount)
    {
        if (currentGold <= 0)
            return;
        goldAmount = 1;
        currentGold -= goldAmount;
    }
}
