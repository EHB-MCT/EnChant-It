using System;
using UnityEngine;

public class CastingSpell : MonoBehaviour
{
    [Header("References")]
    public GameObject FireBall;
    public GameObject FlameThrower;
    public GameObject HealSpell;

    public GameObject SpellSpawnPoint;
    public Camera MainCamera;
    public Player PlayerCharacter; 

    public Transform TeleportEffectParent;
    public Transform SpawnEffectParent;


    [Header("Settings")]
    public float DistanceFromCamera = 5f;
    public int FireManaCost = 20;
    public int InfernoManaCost = 50;
    public int HealManaCost = 30;

    public bool CastFireSpell = false;
    public bool CastHealSpell = false;
    public void UpdateSpell(string[] spells)
    {
        Debug.Log("Update spells being done!");
        if (spells.Length != 0)
        {
            CastSpell(spells);
            return;
        }

        if (spells.Length == 0 || spells[0] == "spell")
        {
            return;
        }
    }

    public void CastSpell(string[] spellNames)
    {
        if (spellNames.Length > 0)
        {
            string spellName = spellNames[0];

            // Fire ball
            if (spellName.Equals("fire", StringComparison.OrdinalIgnoreCase) && PlayerCharacter.currentMana >= FireManaCost)
            {
                CastFireSpell = true;
                PlayerCharacter.UseMana(FireManaCost);
                Debug.Log("found spell");

                Vector3 spawnPosition = SpellSpawnPoint.transform.position;
                Instantiate(FireBall, spawnPosition, Quaternion.identity);
            }

            // Flamethrower
            if (spellName.Equals("Inferno", StringComparison.OrdinalIgnoreCase) && PlayerCharacter.currentMana >= InfernoManaCost)
            {
                PlayerCharacter.UseMana(InfernoManaCost);
                Debug.Log("found spell");

                Vector3 spawnPosition = SpellSpawnPoint.transform.position;
                Quaternion spawnRotation = SpellSpawnPoint.transform.rotation;

                GameObject flameThrowerInstance = Instantiate(FlameThrower, spawnPosition, spawnRotation);
                flameThrowerInstance.transform.SetParent(SpellSpawnPoint.transform);
            }
            // Heal
            if (spellName.Equals("heal", StringComparison.OrdinalIgnoreCase) && PlayerCharacter.currentMana >= HealManaCost)
            {
                CastHealSpell = true;
                PlayerCharacter.UseMana(HealManaCost);
                Debug.Log("found spell");

                Vector3 playerPosition = PlayerCharacter.transform.position - new Vector3(0f, 1.5f, 0f); 
                Quaternion spawnRotation = SpellSpawnPoint.transform.rotation;

                Instantiate(HealSpell, playerPosition, spawnRotation);
            }
        }
    }
}
