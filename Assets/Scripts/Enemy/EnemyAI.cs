using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float patrolRadius = 10.0f;
    [SerializeField] private float chaseRadius = 5;
    private NavMeshAgent agent;
    private float initialSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialSpeed = agent.speed;
    }

    private void Update()
    {
        if (agent.isStopped) return;
        
        FindPlayerToChase();
    }

    private void FindPlayerToChase()
    {
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
            agent.SetDestination(closestEnemy.position);
        }
        else
        {
            Patrol();
        }
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    public void Patrol()
    {
        if (agent.remainingDistance > agent.stoppingDistance) return;
        
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
