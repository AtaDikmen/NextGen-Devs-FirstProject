using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    //Audio Clips
    private List<AudioClip> collectCoin = new List<AudioClip>();
    private List<AudioClip> collectWood = new List<AudioClip>();

    [SerializeField] private int woodStart = 0;
    [SerializeField] private int goldStart = 0;
    [SerializeField] public int woodRange = 50;

    [SerializeField] private TextMeshProUGUI woodResource;
    [SerializeField] private TextMeshProUGUI goldResource;
    private int currentWood;
    private int currentGold;

    protected override void Awake()
    {
        SetAudioClips();

        currentWood = woodStart;
        currentGold = goldStart;
    }

    private void Start()
    {
        UpdateResourceTexts();
    }

    public void UpdateResourceTexts()
    {
        woodResource.text = currentWood.ToString();
        goldResource.text = currentGold.ToString();
    }

    public int GetWood()
    {
        return currentWood;
    }

    public int GetGold()
    {
        return currentGold;
    }

    public void AddWood(int woodAmount)
    {
        AudioManager.Instance.PlaySFX(collectWood[Random.Range(0, collectWood.Count)], 1f);

        currentWood += woodAmount;
        woodResource.text = currentWood.ToString();
    }

    public void SubWood(int woodAmount)
    {
        if (currentWood <= 0)
            return;
        currentWood -= woodAmount;
        woodResource.text = currentWood.ToString();
    }

    public void AddGold(int goldAmount)
    {
        AudioManager.Instance.PlaySFX(collectCoin[Random.Range(0, collectCoin.Count)], 1f);

        currentGold += goldAmount;
        goldResource.text = currentGold.ToString();
    }

    public void SubGold(int goldAmount)
    {
        if (currentGold <= 0)
            return;
        currentGold -= goldAmount;
        goldResource.text = currentGold.ToString();
    }

    private void SetAudioClips()
    {
        collectCoin.Add(Resources.Load<AudioClip>("CollectCoin (1)"));
        collectCoin.Add(Resources.Load<AudioClip>("CollectCoin (2)"));

        collectWood.Add(Resources.Load<AudioClip>("CollectWood (1)"));
        collectWood.Add(Resources.Load<AudioClip>("CollectWood (2)"));
        collectWood.Add(Resources.Load<AudioClip>("CollectWood (3)"));
        collectWood.Add(Resources.Load<AudioClip>("CollectWood (4)"));
    }
}
