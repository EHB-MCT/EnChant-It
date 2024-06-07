using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [Header("Settings")]
    public float DamageAmount = 1;

    private ParticleSystem flameThrowerParticles;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        flameThrowerParticles = GetComponent<ParticleSystem>();
        if (flameThrowerParticles == null)
        {
            Debug.LogError("ParticleSystem component not found on FlameThrower.");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = flameThrowerParticles.GetCollisionEvents(other, collisionEvents);
        Debug.Log($"Number of collision events: {numCollisionEvents}");

        for (int i = 0; i < numCollisionEvents; i++)
        {
            Debug.Log($"Collision with: {other.name}");

            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit detected.");
                DealDamage(other);
            }
            else
            {
                Debug.Log($"Object hit does not have the 'Enemy' tag. Tag is: {other.tag}");
            }
        }
    }

    private void DealDamage(GameObject enemy)
    {
        EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
        if (enemyManager != null)
        {
            Debug.Log("Dealing damage to enemy.");
            enemyManager.Hit(DamageAmount);
        }
        else
        {
            Debug.LogError("EnemyManager component not found on the enemy.");
        }
    }

    private void Update()
    {
        // Check if the particle system has finished playing and destroy the GameObject if it has
        if (flameThrowerParticles && !flameThrowerParticles.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
