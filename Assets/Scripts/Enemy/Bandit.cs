using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shootRadius = 9f;
    private Transform player;
    private bool isPlayerInRange = false;
    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, shootRadius, LayerMask.GetMask("Player"));
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
            isShooting = false;
        }
            
        if (isPlayerInRange)
        {
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(ShootCoroutine());
            }
        }
    }

    IEnumerator ShootCoroutine()
    {
        while(isPlayerInRange)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetTarget(player);
            yield return new WaitForSeconds(1f);
        }  
    }

    public void SetTarget(Transform player)
    {
        this.player = player;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the Gizmo
        Gizmos.DrawWireSphere(transform.position, shootRadius); // Draw the sphere using Gizmos
    }
}
