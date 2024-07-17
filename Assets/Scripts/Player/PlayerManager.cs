using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    pistol,
    rifle,
    shotgun,
    rocket
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeText;

    private AudioManager audioManager;
    private AudioClip upgradePistol;
    private AudioClip upgradeRifle;
    private AudioClip upgradeShotgun;
    private AudioClip upgradeRocket;

    public int rookieNum, soldierNum, vanguardNum, bulldozerNum;


    private Player player;

    public WeaponType currentWeapon;

    [SerializeField] private GameObject[] weapons;

    [SerializeField] private GameObject[] allies;


    void Start()
    {
        audioManager = AudioManager.Instance;
        upgradePistol = Resources.Load<AudioClip>("FreeUpgradePistol");
        upgradeRifle = Resources.Load<AudioClip>("FreeUpgradeRifle");
        upgradeShotgun = Resources.Load<AudioClip>("FreeUpgradeShotgun");
        upgradeRocket = Resources.Load<AudioClip>("FreeUpgradeRocket");

        currentWeapon = WeaponType.pistol;

        player = GetComponentInChildren<Player>();
    }

    void Update()
    {
       
    }

    public void OnSpeedBoost(float _speed)
    {
        foreach (var ally in allies)
        {
            if(ally.activeSelf)
                ally.GetComponentInChildren<Ally>().agent.speed = _speed;
        }
    }


    public void AllyJoinGroup(AllyType _allyType, Transform _joinPosition)
    {
        foreach (GameObject ally in allies)
        {
            if (ally.GetComponentInChildren<Ally>().allyType == _allyType)
            {
                if (!ally.activeSelf)
                {
                    ally.transform.GetChild(0).position = _joinPosition.position;
                    ally.SetActive(true);

                    
                }

                switch (_allyType)
                {
                    case AllyType.rookie:
                        audioManager.PlaySFX(upgradePistol);
                        rookieNum++;
                        if (rookieNum == 3)
                        {
                            ally.GetComponentInChildren<Ally>().UpgradeAlly();
                            StartCoroutine(UpgradeTextCoroutine(_allyType));
                        }
                        break;
                    case AllyType.soldier:
                        soldierNum++;
                        audioManager.PlaySFX(upgradeRifle);
                        if (soldierNum == 3)
                        {
                            ally.GetComponentInChildren<Ally>().UpgradeAlly();
                            StartCoroutine(UpgradeTextCoroutine(_allyType));
                        }
                        break;
                    case AllyType.vanguard:
                        vanguardNum++;
                        audioManager.PlaySFX(upgradeShotgun);
                        if (vanguardNum == 3)
                        {
                            ally.GetComponentInChildren<Ally>().UpgradeAlly();
                            StartCoroutine(UpgradeTextCoroutine(_allyType));
                        }
                        break;
                    case AllyType.bulldozer:
                        bulldozerNum++;
                        audioManager.PlaySFX(upgradeRocket);
                        if (bulldozerNum == 3)
                        {
                            ally.GetComponentInChildren<Ally>().UpgradeAlly();
                            StartCoroutine(UpgradeTextCoroutine(_allyType));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private IEnumerator UpgradeTextCoroutine(AllyType allyType)
    {
        upgradeText.text = "The " + allyType.ToString() + " upgraded!";

        yield return new WaitForSeconds(2);

        upgradeText.text = "";
    }

    public void ChangeWeapon(string weaponType)
    {
        WeaponType type;

        if (!Enum.TryParse(weaponType, true, out type))
        {
            Debug.LogError("Invalid weapon type: " + weaponType);
            return;
        }

        player.SetWeaponStat(type);


        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }

        switch (type)
        {
            case WeaponType.pistol:
                currentWeapon = WeaponType.pistol;
                weapons[0].SetActive(true);
                break;
            case WeaponType.rifle:
                currentWeapon = WeaponType.rifle;
                weapons[1].SetActive(true);
                break;
            case WeaponType.shotgun:
                currentWeapon = WeaponType.shotgun;
                weapons[2].SetActive(true);
                break;
            case WeaponType.rocket:
                currentWeapon = WeaponType.rocket;
                weapons[3].SetActive(true);
                break;
            default:
                break;
        }
    }
}
