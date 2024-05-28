using System;
using UnityEngine;
public class CastingSpell : MonoBehaviour
{
    [Header("References")]
    public GameObject FireBall;
    public GameObject FlameThrower;
    public GameObject HealSpell;
    public GameObject spellSpawnPoint;
    public Camera MainCamera;
    public Transform teleportEffectParent;
    public Transform spawnEffectParent;
    public bool CastFireSpell = false;
    

    [Header("Settings")]
    public float DistanceFromCamera = 5f;
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
            if (spellName.Equals("fire", StringComparison.OrdinalIgnoreCase))
            {
                CastFireSpell = true;
                Debug.Log("found spell");

                Vector3 spawnPosition = spellSpawnPoint.transform.position;

                Instantiate(FireBall, spawnPosition, Quaternion.identity);
            }

            //flamethrower
            if (spellName.Equals("Inferno", StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("found spell");

                Vector3 spawnPosition = spellSpawnPoint.transform.position;

                Quaternion spawnRotation = spellSpawnPoint.transform.rotation;

                GameObject flameThrowerInstance = Instantiate(FlameThrower, spawnPosition, spawnRotation);

                flameThrowerInstance.transform.SetParent(spellSpawnPoint.transform);
            }


            if (spellName.Equals("heal", StringComparison.OrdinalIgnoreCase))
            {
                CastFireSpell = true;
                Debug.Log("found spell");

                Vector3 spawnPosition = spellSpawnPoint.transform.position;
                Quaternion spawnRotation = spellSpawnPoint.transform.rotation;
                Debug.Log("do healing");
                Instantiate(HealSpell, teleportEffectParent.transform.position, Quaternion.identity, spawnEffectParent);
            }
        }
    }




}
