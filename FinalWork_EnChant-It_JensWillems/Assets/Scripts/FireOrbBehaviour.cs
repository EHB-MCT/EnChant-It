using System.Collections;
using UnityEngine;


public class FireOrbBehaviour : MonoBehaviour
{
    private Camera _mainCamera;

    [Header("Settings")]
    public float Speed = 5;
    public int DamageAmount = 10;
    public AudioSource AudioSourceImpact;
    public GameObject ParentGameObject;
    public AudioSource AudioSourceShoot;
    public ParticleSystem ParticleSystem;

    public void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene!");
            return;
        }

        Vector3 forwardDirection = _mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(forwardDirection);
    }
    void Update()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide wtih:" + other.name);
        if (other.CompareTag("Enemy"))
        {
            AudioSourceImpact.Play();
            AudioSourceShoot.Stop();
            ParticleSystem.Stop();

            if (ParentGameObject != null)
            {
                ParticleSystem[] particleSystems = ParentGameObject.GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem ps in particleSystems)
                {
                    ps.Stop();
                }
            }
            else
            {
                Debug.LogError("Parent GameObject is not assigned.");
            }

            StartCoroutine(DealDamageAfterParticlesStop(other));
        }
        /*
        if (other.CompareTag("Untagged"))
        {
            AudioSourceImpact.Play();
            AudioSourceShoot.Stop();
            ParticleSystem.Stop();

            if (ParentGameObject != null)
            {
                ParticleSystem[] particleSystems = ParentGameObject.GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem ps in particleSystems)
                {
                    ps.Stop();
                }
            }
            else
            {
                Debug.LogError("Parent GameObject is not assigned.");
            }
        }
        */
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
}

