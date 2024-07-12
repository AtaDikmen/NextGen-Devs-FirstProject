public class Rookie : Ally
{
    void Start()
    {
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
        audioManager.PlaySFX2D.Invoke("ShotPistol", 0.15f, false);
    }
}