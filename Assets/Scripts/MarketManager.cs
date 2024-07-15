using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    public GameObject marketPanel; // Reference to the market panel
    public PlayerManager playerManager;
    public ResourceManager resourceManager;
    private AudioManager audioManager;
    public Button closeButton; // Reference to the close button
    public GameObject RiflePriceText;
    public GameObject ShotgunPriceText;
    public GameObject RocketLauncherPriceText;
    public GameObject EquippedIcon;
    private int riflePrice;
    private int shotgunPrice;
    private int rocketLauncherPrice;
    private List<string> ownedItems;
    private Vector3 initialPos;

    //Audios
    private AudioClip marketOpenClose;
    private AudioClip marketBuy;

    void Awake()
    {
        ownedItems = new List<string>();
        riflePrice = 5;
        shotgunPrice = 7;
        rocketLauncherPrice = 9;
    }
    private void Start()
    {
        audioManager = AudioManager.Instance;
        marketOpenClose = Resources.Load<AudioClip>("UIOpen_Close");
        marketBuy = Resources.Load<AudioClip>("UIButtonPositive");


        marketPanel.SetActive(false);
        ownedItems.Add("pistol");
        closeButton.onClick.AddListener(CloseMarketPanel);
        initialPos = EquippedIcon.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            marketPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            marketPanel.SetActive(false);
            audioManager.PlaySFX(marketOpenClose);
        }
    }

    private void CloseMarketPanel()
    {
        marketPanel.SetActive(false);
        audioManager.PlaySFX(marketOpenClose);
    }
    private int GetPriceFromName(string name)
    {
        switch (name)
        {
            case "rifle":
                return riflePrice;
            case "shotgun":
                return shotgunPrice;
            case "rocket":
                return rocketLauncherPrice;
            default:
                return 0;
        }
    }
    private void adujustEquippedItem(string item)
    {
        switch (item)
        {
            case "rifle":
                EquippedIcon.transform.position = RiflePriceText.transform.position;
                RiflePriceText.SetActive(false);
                break;
            case "shotgun":
                EquippedIcon.transform.position = ShotgunPriceText.transform.position;
                ShotgunPriceText.SetActive(false);
                break;
            case "rocket":
                EquippedIcon.transform.position = RocketLauncherPriceText.transform.position;
                RocketLauncherPriceText.SetActive(false);
                break;
            default:
                EquippedIcon.transform.position = initialPos;
                break;
        }
    }
    public void ObtainItem(string item)
    {
        int itemPrice = GetPriceFromName(item);
        if (!ownedItems.Contains(item) && resourceManager.GetGold() >= itemPrice)
        {
            playerManager.ChangeWeapon(item);
            resourceManager.SubGold(itemPrice);
            ownedItems.Add(item);
            adujustEquippedItem(item);
            audioManager.PlaySFX(marketBuy);
        }
        else if (ownedItems.Contains(item))
        {
            playerManager.ChangeWeapon(item);
            adujustEquippedItem(item);
            audioManager.PlaySFX(marketBuy);
        }
        else
        {
            Debug.Log("Not enough gold to buy " + item);
        }
    }
}
