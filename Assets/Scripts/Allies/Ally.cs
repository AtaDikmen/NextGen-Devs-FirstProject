using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity
{
    private void Awake()
    {
        targetTag = "Enemy";

        characterName = "Ally";
        damage = 10;
        attackSpeed = 1.0f;
        health = 100.0f;
        attackRadius = 5.0f;
        nextAttackTime = 0f;
    }

}
