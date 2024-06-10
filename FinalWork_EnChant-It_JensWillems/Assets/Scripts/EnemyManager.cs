using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnimator;
    public float damage = 20f;
    public float health = 100;
    public GameManager gameManager;
    public Slider slider;

    public bool playerInReach;
    private float attackDelayTimer;
    public float attackAnimStartDelay;
    public float delayBetweenAttacks;

    public AudioSource audioSource;
    public AudioClip[] zombieSounds;
    private bool hasStartedMoving = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        slider.maxValue = health;
        slider.value = health;
        playerInReach = false;
    }

    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Debug.Log(agent.remainingDistance);

        if (!hasStartedMoving)
        {
            if (agent.hasPath && agent.velocity.sqrMagnitude > 0f)
            {
                hasStartedMoving = true;
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                playerInReach = true;
            }
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
            audioSource.Play();
        }

        slider.transform.LookAt(player.transform);
        agent.destination = player.transform.position;

        if (agent.velocity.magnitude > 1)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }

        if (playerInReach)
        {
            attackDelayTimer += Time.deltaTime;
        }

        if (attackDelayTimer >= delayBetweenAttacks - attackAnimStartDelay && attackDelayTimer <= delayBetweenAttacks && playerInReach)
        {
            enemyAnimator.SetTrigger("isAttacking");
        }

        if (attackDelayTimer >= delayBetweenAttacks && playerInReach)
        {
            player.GetComponent<PlayerManager>().Hit(damage);
            attackDelayTimer = 0;
        }
    }
    /*
private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            playerInReach = true;
        }
    }
    */
    public void Hit(float damage)
    {
        health -= damage;
        slider.value = health;
        if (health == 0)
        {
            enemyAnimator.SetTrigger("isDead");
            Destroy(gameObject, 10f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManager>());
            Destroy(GetComponent<BoxCollider>());
            Debug.Log("killed one");
            gameManager.EnemiesAlive--;

        }
    }
    /*
    private void OnCollisionStay(Collision collision)
    {
        if (playerInReach)
        {
            attackDelayTimer += Time.deltaTime;
        }

        if (attackDelayTimer >= delayBetweenAttacks - attackAnimStartDelay && attackDelayTimer <= delayBetweenAttacks && playerInReach)
        {
            enemyAnimator.SetTrigger("isAttacking");
        }

        if (attackDelayTimer >= delayBetweenAttacks && playerInReach)
        {
            player.GetComponent<PlayerManager>().Hit(damage);
            attackDelayTimer = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            playerInReach = false;
            attackDelayTimer = 0;
        }
    }
    */
}