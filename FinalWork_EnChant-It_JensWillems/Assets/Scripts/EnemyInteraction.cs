using UnityEngine;
using System.Collections;

public class EnemyInteraction : MonoBehaviour
{
    public int MaxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        BoxCollider bc = gameObject.GetComponent<BoxCollider>();
        mr.enabled = false;
        bc.enabled = false;
        StartCoroutine(Respawn(mr, bc));
    }

    IEnumerator Respawn(MeshRenderer mr, BoxCollider bc)
    {
        yield return new WaitForSeconds(5f);
        currentHealth = MaxHealth;
        mr.enabled = true;
        bc.enabled = true;
    }
}
