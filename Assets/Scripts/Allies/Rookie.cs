public class Rookie : Ally
{
    void Start()
    {
        characterName = "Rookie";
        damage = 15;
        attackSpeed = 1.0f;
        health = 80.0f;
        attackRadius = 5.0f;
    }

    protected override void Attack()
    {
        base.Attack();
    }
}