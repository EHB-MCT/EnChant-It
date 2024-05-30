using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [Header("Settings")]
    public int DamageAmount = 1;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(DealDamageAfterParticlesStop(other));
        }
    }

    private IEnumerator DealDamageAfterParticlesStop(GameObject enemy)
    {
        yield return null;

        EnemyInteraction enemyHealth = enemy.GetComponent<EnemyInteraction>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(DamageAmount);
        }
    }
}
