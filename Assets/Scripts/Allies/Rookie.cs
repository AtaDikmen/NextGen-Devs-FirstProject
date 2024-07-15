using UnityEngine;

public class Rookie : Ally
{
    protected override void Start()
    {
        base.Start();

        allyType = AllyType.rookie;

        characterName = "Rookie";
        damage = 10;
        attackSpeed = 1.0f;
        maxHealth = 100f;
        currentHealth = maxHealth;
        attackRadius = 6f;
    }

    protected override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Shot");
    }

    protected override void OnShotSFX()
    {
        AudioClip shotSFX = Resources.Load<AudioClip>("ShotPistolV1");

        audioManager.PlaySFX(shotSFX);
    }

    public override void UpgradeAlly()
    {
        damage = 20;
        attackSpeed = 2f;
        maxHealth = 200f;
        currentHealth = maxHealth;

        Debug.Log("Rookie UPGRADED!");
    }
}