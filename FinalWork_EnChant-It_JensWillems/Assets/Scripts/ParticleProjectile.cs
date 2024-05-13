using UnityEngine;

public class ParticleProjectile : MonoBehaviour
{
    public int damageAmount = 10;
    public float speed = 10f;

    // Adjust the projectile's velocity to match the direction the player is looking
    public void Launch(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = transform.right * speed;
    }

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.right * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the enemy's health component
            EnemyInteraction enemyHealth = other.GetComponent<EnemyInteraction>();
            if (enemyHealth != null)
            {
                // Deal damage to the enemy
                enemyHealth.TakeDamage(damageAmount);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
