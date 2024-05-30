using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxMana = 100;
    public int currentMana;
    public float manaRegenRate = 5f; // Mana regenerated per second

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
        // Update sliders to match current health and mana
        healthSlider.value = currentHealth;
        manaSlider.value = currentMana;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        Debug.Log("Player Health: " + currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Player Health: " + currentHealth);
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        if (currentMana < 0)
        {
            currentMana = 0;
        }
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
        }
    }
}
