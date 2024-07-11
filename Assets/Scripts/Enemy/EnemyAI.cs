using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 10.0f;
    [SerializeField] private float chaseRadius = 5;
    private NavMeshAgent agent;
    private Animator animator;
    private Entity entity;
    private GameManager gameManager;
    private Transform playerParent;
    private float initialSpeed;
    private Transform selectedPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        entity = GetComponent<Entity>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerParent = gameManager.GetPlayerParent();
        initialSpeed = agent.speed;
    }

    private void Update()
    {
        if (gameManager.isEnemyWavePhase && playerParent.childCount != 0)
        {
            Debug.Log("Enemy Wave Phase "+ playerParent.childCount);
            agent.SetDestination(SetPlayerPosition());
        }
        else 
        {
            Debug.Log("Patrol Phase");
            FindPlayerToChase();
        }
    }

    private void FindPlayerToChase()
    {
        if(entity.isTargetFound) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chaseRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hitCollider.transform;
                }
            }
        }
         
        if (closestEnemy != null)
        {
            animator.SetTrigger("Run");
            agent.SetDestination(closestEnemy.position);
        }
        else
        {
            Patrol();
        }
    }
    public void Stop()
    {        
        //isPatrolling = false;
        agent.isStopped = true;
        agent.speed = 0;
    }

    public void Patrol()
    {
        if (!agent.isStopped && agent.remainingDistance > agent.stoppingDistance) return;

        animator.SetTrigger("Walk");

        //isPatrolling = true;
        agent.isStopped = false;
        agent.speed = initialSpeed;
        agent.SetDestination(RandomNavmeshLocation());
    }

    private Vector3 RandomNavmeshLocation() 
    { 
        Vector3 randomPoint = (Random.insideUnitSphere * patrolRadius) + transform.position;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.blue, 1.0f);
            return hit.position;
        }

        return transform.position;
    }

    private Vector3 SetPlayerPosition() 
    {
        if (!selectedPlayer)
        {
            selectedPlayer = playerParent.GetChild(Random.Range(0, playerParent.childCount));
        }   

        return selectedPlayer.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
