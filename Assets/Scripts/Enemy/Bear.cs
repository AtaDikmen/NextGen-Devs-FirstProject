using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    void Start()
    {
        characterName = "Bear";
        damage = 15;
        attackSpeed = 1.0f;
        health = 80.0f;
        attackRadius = 2.0f;
    }

    protected override void Attack()
    {
        // Special attack of Bear
    }
}
