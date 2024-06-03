using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action OnDestroyed;

    public float moveSpeed = 3.0f;
    public float attackRange = 2.0f;
    public float timeBetweenAttacks = 5f; 
    public int attackDamage = 5;

    private Transform playerTransform;
    private Animator animator; 
    private float timeSinceLastAttack = 0f; 

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            return; 
        }

        transform.LookAt(playerTransform);

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        animator.SetFloat("DistanceToPlayer", distanceToPlayer);

        if (distanceToPlayer > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position;

            timeSinceLastAttack += Time.deltaTime;

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                animator.SetTrigger("AttackTrigger");
                timeSinceLastAttack = 0f;
            }
        }
    }

    void AttackPlayer()
    {
        Player player = playerTransform.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
    }

    void Die()
    {
        animator.SetTrigger("DieTrigger");

        OnDestroyed?.Invoke();
    }
}
