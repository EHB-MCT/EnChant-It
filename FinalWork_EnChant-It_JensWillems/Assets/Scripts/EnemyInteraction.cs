using UnityEngine;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public int MaxHealth = 10;
    private int currentHealth;
    private Animator animator;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private SphereCollider sphereCollider;
    private bool isDying = false;

    private void Start()
    {
        currentHealth = MaxHealth;
        animator = GetComponentInParent<Animator>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on parent.");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0 && !isDying)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null)
        {
            Debug.Log("die");
            animator.SetBool("Die", true);
        }

        if (sphereCollider != null)
        {
            sphereCollider.enabled = false;
        }

        isDying = true;
    }

    private void Update()
    {
        if (isDying && animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1.0f)
            {
                OnDieAnimationComplete();
            }
        }
    }

    private void OnDieAnimationComplete()
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.enabled = false;
        }

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);
        currentHealth = MaxHealth;

        if (animator != null)
        {
            animator.SetBool("Die", false);
        }

        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.enabled = true;
        }

        if (sphereCollider != null)
        {
            sphereCollider.enabled = true;
        }

        isDying = false;
    }
}
