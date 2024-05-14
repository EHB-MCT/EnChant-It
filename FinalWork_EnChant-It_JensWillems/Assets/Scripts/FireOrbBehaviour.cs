using UnityEngine;

public class FireOrbBehaviour : MonoBehaviour
{
    private Camera _mainCamera;

    [Header("Settings")]
    public float Speed = 5;
    public int DamageAmount = 10;

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
        if (other.CompareTag("Enemy"))
        {
            EnemyInteraction enemyHealth = other.GetComponent<EnemyInteraction>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(DamageAmount);
            }

            Destroy(gameObject);
        }
    }
}
