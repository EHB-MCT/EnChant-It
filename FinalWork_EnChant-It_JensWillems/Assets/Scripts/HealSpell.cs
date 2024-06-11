using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    [Header("Settings")]
    public int HealAmount = 20;
    public float HealInterval = 1f;
    public float Duration = 6.0f;

    private float _healTimer;
    private float _durationTimer;

    void Start()
    {
        _healTimer = HealInterval;
        _durationTimer = Duration;
    }

    void Update()
    {
        _durationTimer -= Time.deltaTime;

        if (_durationTimer > 0)
        {
            _healTimer -= Time.deltaTime;

            if (_healTimer <= 0)
            {
                HealPlayer();
                _healTimer = HealInterval;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void HealPlayer()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.Heal(HealAmount);
        }
    }
}
