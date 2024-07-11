using BigRookGames.Weapons;
using System.Net.Sockets;
using UnityEngine;

public class Bulldozer : Ally
{
    public GameObject rocketPrefab;
    [SerializeField] private GunfireController rocketController;


    void Start()
    {
        characterName = "Bulldozer";
        damage = 100;
        attackSpeed = 0.1f;
        maxHealth = 150.0f;
        currentHealth = maxHealth;
        attackRadius = 10.0f;
    }

    protected override void Attack()
    {
        FireRocket();
    }

    private void FireRocket()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        rocketController.target = target;
        rocketController.FireWeapon();


        Rocket rocketScript = rocketPrefab.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.damage = damage;
            rocketScript.explosionRadius = 5.0f;
        }

        //Debug.Log(characterName + " fired a rocket at " + target.name + " causing an explosion with " + damage + " damage.");
    }
}
