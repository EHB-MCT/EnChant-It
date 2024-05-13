using System;
using UnityEngine;
public class CastingSpell : MonoBehaviour
{
    public GameObject Fire;
    public Camera mainCamera;
    public float distanceFromCamera = 5f;
    public Book Book;
    public AutoFlip AutoFlip;

    public GameObject spellSpawnPoint;
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

            if (spellName.Equals("fire", StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("found spell");

                // Use the position and rotation of the spell spawn point
                Vector3 spawnPosition = spellSpawnPoint.transform.position;
                Quaternion spawnRotation = spellSpawnPoint.transform.rotation;

                // Instantiate the fireball
                GameObject fireInstance = Instantiate(Fire, spawnPosition, spawnRotation);
                fireInstance.SetActive(true);

                // Set the fireball's initial velocity to move along the X-axis direction aligned with the player's view
                ParticleProjectile fireProjectile = fireInstance.GetComponent<ParticleProjectile>();
                if (fireProjectile != null)
                {
                    // Get the forward direction of the spell spawn point (assuming it's aligned with the player's view)
                    Vector3 forwardDirection = spellSpawnPoint.transform.forward;

                    fireProjectile.Launch(forwardDirection);
                }

                // Set the rotation of the fireball to match the spawn point's rotation
                fireInstance.transform.rotation = spawnRotation;

                // Do not make the fireball a child of the spellSpawnPoint
            }
        }
    }




}
