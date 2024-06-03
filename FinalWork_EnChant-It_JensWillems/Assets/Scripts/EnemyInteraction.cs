using UnityEngine;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public int MaxHealth = 10;

    private int _currentHealth;
    private Animator _animator;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private SphereCollider sphereCollider;
    private bool _isDying = false;

    private void Start()
    {
        _currentHealth = MaxHealth;
        _animator = GetComponentInParent<Animator>();
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();

        if (_animator == null)
        {
            Debug.LogError("Animator component not found on parent.");
        }
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0 && !_isDying)
        {
            Die();
        }
    }

    void Die()
    {
        if (_animator != null)
        {
            Debug.Log("die");
            _animator.SetBool("Die", true);
        }

        if (sphereCollider != null)
        {
            sphereCollider.enabled = false;
        }

        _isDying = true;
    }

    private void Update()
    {
        if (_isDying && _animator != null)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1.0f)
            {
                OnDieAnimationComplete();
            }
        }
    }

    private void OnDieAnimationComplete()
    {
        if (_skinnedMeshRenderer != null)
        {
            _skinnedMeshRenderer.enabled = false;
        }

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);
        _currentHealth = MaxHealth;

        if (_animator != null)
        {
            _animator.SetBool("Die", false);
        }

        if (_skinnedMeshRenderer != null)
        {
            _skinnedMeshRenderer.enabled = true;
        }

        if (sphereCollider != null)
        {
            sphereCollider.enabled = true;
        }

        _isDying = false;
    }
}
