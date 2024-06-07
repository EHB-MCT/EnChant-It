using UnityEngine;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public int MaxHealth = 10;

    private float _currentHealth;
    private Animator _animator;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private SphereCollider _sphereCollider;
    private bool _isDying = false;

    private void Start()
    {
        _currentHealth = MaxHealth;
        _animator = GetComponentInParent<Animator>();
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();

        if (_animator == null)
        {
            Debug.LogError("Animator component not found on parent.");
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _animator.SetTrigger("isDead");
            Invoke("Die", 4);
        }
    }

    void Die()
    {
        if (_animator != null)
        {
            Debug.Log("isDead");
        }

        if (_sphereCollider != null)
        {
            _sphereCollider.enabled = false;
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
        yield return new WaitForSeconds(8f);
        _currentHealth = MaxHealth;

        if (_animator != null)
        {
            _animator.SetTrigger("isAlive");
        }

        if (_skinnedMeshRenderer != null)
        {
            _skinnedMeshRenderer.enabled = true;
        }

        if (_sphereCollider != null)
        {
            _sphereCollider.enabled = true;
        }
        _isDying = false;
    }
}
