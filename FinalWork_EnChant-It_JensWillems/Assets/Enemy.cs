using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action OnDestroyed;

    public float moveSpeed = 3.0f;
    public float attackRange = 2.0f;
    public float timeBetweenAttacks = 5f; // Time between attacks
    public int attackDamage = 5;

    private Transform playerTransform;
    private Animator animator; // Reference to the Animator component
    private float timeSinceLastAttack = 0f; // Time elapsed since the last attack

    void Start()
    {
        // Find the player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return; // Exit if no player found
        }

        // Look at the player
        transform.LookAt(playerTransform);

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Update the DistanceToPlayer parameter in the Animator
        animator.SetFloat("DistanceToPlayer", distanceToPlayer);

        if (distanceToPlayer > attackRange)
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Stop moving
            transform.position = transform.position;

            // Increment time since last attack
            timeSinceLastAttack += Time.deltaTime;

            // Check if it's time to attack
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // Trigger attack animation
                animator.SetTrigger("AttackTrigger");
                // Reset time since last attack
                timeSinceLastAttack = 0f;
            }
        }
    }

    void AttackPlayer()
    {
        // Assume the player has a method TakeDamage(int damage)
        Player player = playerTransform.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
    }

    void Die()
    {
        // Trigger die animation
        animator.SetTrigger("DieTrigger");

        // Invoke the OnDestroyed event
        OnDestroyed?.Invoke();
    }
}
