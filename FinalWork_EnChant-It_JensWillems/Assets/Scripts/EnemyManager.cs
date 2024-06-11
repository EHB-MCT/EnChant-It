using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    public GameObject Player;
    public Animator EnemyAnimator;
    public GameManager GameManager;
    public Slider HealthBar;

    [Header("Settings")]
    public float Damage = 20f;
    public float Health = 100;
    public bool PlayerInReach;
    private float AttackDelayTimer;
    public float AttackAnimStartDelay;
    public float DelayBetweenAttacks;

    public AudioSource AudioSource;
    public AudioClip[] ZombieSounds;

    private bool _hasStartedMoving = false;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        HealthBar.maxValue = Health;
        HealthBar.value = Health;
        PlayerInReach = false;
    }

    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (!_hasStartedMoving)
        {
            if (agent.hasPath && agent.velocity.sqrMagnitude > 0f)
            {
                _hasStartedMoving = true;
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                PlayerInReach = true;
            }
        }

        if (!AudioSource.isPlaying)
        {
            AudioSource.clip = ZombieSounds[Random.Range(0, ZombieSounds.Length)];
            AudioSource.Play();
        }

        HealthBar.transform.LookAt(Player.transform);
        agent.destination = Player.transform.position;

        if (agent.velocity.magnitude > 1)
        {
            EnemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            EnemyAnimator.SetBool("isRunning", false);
        }

        if (PlayerInReach)
        {
            AttackDelayTimer += Time.deltaTime;
        }

        if (AttackDelayTimer >= DelayBetweenAttacks - AttackAnimStartDelay && AttackDelayTimer <= DelayBetweenAttacks && PlayerInReach)
        {
            EnemyAnimator.SetTrigger("isAttacking");
        }

        if (AttackDelayTimer >= DelayBetweenAttacks && PlayerInReach)
        {
            Player.GetComponent<PlayerManager>().Hit(Damage);
            AttackDelayTimer = 0;
        }
    }

    public void Hit(float damage)
    {
        Health -= damage;
        HealthBar.value = Health;
        if (Health == 0)
        {
            EnemyAnimator.SetTrigger("isDead");
            Destroy(gameObject, 10f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManager>());
            Destroy(GetComponent<BoxCollider>());
            GameManager.EnemiesAlive--;
        }
    }
}