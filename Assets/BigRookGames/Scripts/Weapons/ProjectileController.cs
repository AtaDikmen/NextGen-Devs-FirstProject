using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public Transform target;
        public GameObject explosionGO;

        // --- Config ---
        public float speed = 100;
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;


        private void Update()
        {
            // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
            if (targetHit) return;

            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (transform.position == target.position)
                {
                    targetHit = true;
                }
            }
            else
            {
                Explode();
                Destroy(gameObject);
            }
        }


        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        /// 
        
        private void OnCollisionEnter(Collision collision)
        {
            // --- return if not enabled because OnCollision is still called if compoenent is disabled ---
            if (!enabled) return;

            // --- Explode when hitting an object and disable the projectile mesh ---
            Explode();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach(Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();


            // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
            Destroy(gameObject, 2f);
        }


        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            // --- Instantiate new explosion option. I would recommend using an object pool ---
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);

            Instantiate(explosionGO, transform.position, Quaternion.identity);
        }
    }
}