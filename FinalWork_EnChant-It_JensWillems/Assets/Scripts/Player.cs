using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxMana = 100;
    public int currentMana;
    public float manaRegenRate = 5f; 

    public Slider healthSlider;
    public Slider manaSlider;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;

        StartCoroutine(RegenerateMana());
    }

    void Update()
    {
        UpdateHealthSlider();
        UpdateManaSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthSlider();
        Debug.Log("Player Health: " + currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthSlider();
        Debug.Log("Player Health: " + currentHealth);
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        if (currentMana < 0)
        {
            currentMana = 0;
        }
        UpdateManaSlider();
        Debug.Log("Player Mana: " + currentMana);
    }

    void Die()
    {
        Debug.Log("Player Died!");
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentMana += (int)manaRegenRate;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            UpdateManaSlider();
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    private void UpdateManaSlider()
    {
        if (manaSlider != null)
        {
            manaSlider.value = currentMana;
        }
    }
}
