using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    public int healAmount = 20;
    public float healInterval = 1f;
    public float duration = 6.0f; 

    private float healTimer;
    private float durationTimer;

    void Start()
    {
        healTimer = healInterval;
        durationTimer = duration;
    }

    void Update()
    {
        durationTimer -= Time.deltaTime;

        if (durationTimer > 0)
        {
            healTimer -= Time.deltaTime;

            if (healTimer <= 0)
            {
                HealPlayer();
                healTimer = healInterval;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void HealPlayer()
    {
        Player playerHealth = FindObjectOfType<Player>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
}
