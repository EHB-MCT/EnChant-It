using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    public TextMeshProUGUI healthNum;
    public GameManager gameManager;
    public GameObject playerCamera;
    public CanvasGroup hurtPanel;
    private float shakeTime;
    private float shakeDuration;
    private Quaternion playerCameraOriginalRotation;

    // Health bar related fields
    public GameObject Bar;
    public bool RecoverHealth;

    void Start()
    {
        // playerCameraOriginalRotation = playerCamera.transform.localRotation;
        UpdateHealthBar();
    }

    void Update()
    {
        if (hurtPanel.alpha > 0)
        {
            hurtPanel.alpha -= Time.deltaTime;
        }
        if (shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            CameraShake();
        }
        /*
        else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
        {
            playerCamera.transform.localRotation = playerCameraOriginalRotation;
        }
        */

        if (RecoverHealth)
        {
            if (health < 100)
            {
                health += Time.deltaTime * 5; // Example recovery rate
                if (health > 100)
                {
                    health = 100;
                    RecoverHealth = false;
                }
                UpdateHealthDisplay();
            }
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        UpdateHealthDisplay();
        if (health <= 0)
        {
            gameManager.EndGame();
        }
        else
        {
            shakeTime = 0;
            shakeDuration = .2f;
            hurtPanel.alpha = .7f;
        }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > 100) health = 100;
        UpdateHealthDisplay();
    }

    public void CameraShake()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2, 2), 0, 0);
    }

    private void UpdateHealthDisplay()
    {
        healthNum.text = health.ToString("F0") + " Health";
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        Bar.GetComponent<RectTransform>().sizeDelta = new Vector2(health, Bar.GetComponent<RectTransform>().sizeDelta.y);
    }
}
