public class Rookie : Ally
{
    void Start()
    {
        allyType = AllyType.rookie;


        characterName = "Rookie";
        damage = 15;
        attackSpeed = 1.0f;
        maxHealth = 80.0f;
        currentHealth = maxHealth;
        attackRadius = 5.0f;
    }

    protected override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Shot");
    }

    protected override void OnShotSFX()
    {
        audioManager.PlaySFX2D.Invoke("ShotPistol", 0.15f, false);
    }
}