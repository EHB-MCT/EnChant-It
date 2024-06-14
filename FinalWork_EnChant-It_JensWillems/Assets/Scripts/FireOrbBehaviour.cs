using System.Collections;
using UnityEngine;

public class FireOrbBehaviour : MonoBehaviour
{
    private Camera _mainCamera;

    [Header("Settings")]
    public float Speed = 5;
    public int DamageAmount = 10;
    public AudioSource AudioSource;
    public GameObject ParentGameObject;
    public ParticleSystem ParticleSystem;

    public float Damage = 25f;

    private bool _hasHit = false;

    public void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            return;
        }

        Vector3 forwardDirection = _mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(forwardDirection);

        StartCoroutine(StopParticlesAndDestroyAfterDelay());
    }

    void Update()
    {
        if (!_hasHit)
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _hasHit = true;

            EnemyManager enemyManager = collision.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.Hit(Damage);
            }

            Debug.Log("hit!");
            AudioSource.Play();
            ParticleSystem.Stop();

            if (ParentGameObject != null)
            {
                StartCoroutine(StopParticlesAfterDelay());
            }
            else
            {
                Debug.LogError("Parent GameObject is not assigned.");
            }

            StartCoroutine(DealDamageAfterParticlesStop(collision.collider));
        }

        Invoke("OnImpactDestroy", 3);
    }

    public void OnImpactDestroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator StopParticlesAfterDelay()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }

        ParticleSystem[] particleSystems = ParentGameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    private IEnumerator DealDamageAfterParticlesStop(Collider enemy)
    {
        yield return null;

        EnemyInteraction enemyHealth = enemy.GetComponent<EnemyInteraction>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(DamageAmount);
        }
    }

    private IEnumerator StopParticlesAndDestroyAfterDelay()
    {
        yield return new WaitForSeconds(3);

        ParticleSystem[] particleSystems = ParentGameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
        Destroy(gameObject);
    }
}
