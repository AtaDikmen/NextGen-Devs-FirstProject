using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : Ally
{
    void Start()
    {
        characterName = "Soldier";
        damage = 10;
        attackSpeed = 4f;
        health = 100f;
        attackRadius = 7.0f;
    }

    

    protected override void Attack()
    {
        base.Attack();
    }
}
