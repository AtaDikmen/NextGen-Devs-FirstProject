public class Rookie : Ally
{
    void Start()
    {
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
    }
}