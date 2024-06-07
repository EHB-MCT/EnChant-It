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
    public GameObject Player;

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
            Debug.Log("this is done now");
            string spellName = spellNames[0];

            // Fire ball
            if (spellName.Equals("fire", StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("casting fire");
                CastFireSpell = true;
                //PlayerCharacter.UseMana(FireManaCost);

                Vector3 spawnPosition = SpellSpawnPoint.transform.position;
                Instantiate(FireBall, spawnPosition, Quaternion.identity);
            }

            // Flamethrower
            if (spellName.Equals("Inferno", StringComparison.OrdinalIgnoreCase))
            {
                Vector3 spawnPosition = SpellSpawnPoint.transform.position;
                Quaternion spawnRotation = SpellSpawnPoint.transform.rotation;

                GameObject flameThrowerInstance = Instantiate(FlameThrower, spawnPosition, spawnRotation);
                flameThrowerInstance.transform.SetParent(SpellSpawnPoint.transform);
                flameThrowerInstance.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            // Heal
            if (spellName.Equals("heal", StringComparison.OrdinalIgnoreCase))
            {
                CastHealSpell = true;

                Vector3 playerPosition = Player.transform.position - new Vector3(0f, 0f, 0f); 
                Quaternion spawnRotation = SpellSpawnPoint.transform.rotation;

                Instantiate(HealSpell, playerPosition, spawnRotation);
            }
        }
    }
}
