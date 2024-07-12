using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    pistol,
    rifle,
    shotgun
}

public class PlayerManager : MonoBehaviour
{
    public WeaponType currentWeapon;

    [SerializeField] private GameObject[] weapons;

    [SerializeField] private GameObject[] allies;


    void Start()
    {
        currentWeapon = WeaponType.pistol;
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
                if (ally.activeSelf)
                    return;

                ally.transform.GetChild(0).position = _joinPosition.position;
                ally.SetActive(true);
            }
        }
    }

    public void ChangeWeapon(string weaponType)
    {
        WeaponType type;

        if (!Enum.TryParse(weaponType, true, out type))
        {
            Debug.LogError("Invalid weapon type: " + weaponType);
            return;
        }


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
            default:
                break;
        }
    }
}
