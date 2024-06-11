using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [Header("Settings")]
    public float DamageAmount = 1;

    private ParticleSystem _flameThrowerParticles;
    private List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        _flameThrowerParticles = GetComponent<ParticleSystem>();
        if (_flameThrowerParticles == null)
        {
            Debug.LogError("ParticleSystem component not found on FlameThrower.");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _flameThrowerParticles.GetCollisionEvents(other, _collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {

            if (other.CompareTag("Enemy"))
            {
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
            enemyManager.Hit(DamageAmount);
        }
        else
        {
            Debug.LogError("EnemyManager component not found on the enemy.");
        }
    }

    private void Update()
    {
        if (_flameThrowerParticles && !_flameThrowerParticles.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
