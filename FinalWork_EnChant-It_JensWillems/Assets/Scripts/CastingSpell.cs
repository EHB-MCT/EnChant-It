using System;
using UnityEngine;
public class CastingSpell : MonoBehaviour
{
    public GameObject Fire;
    public Camera mainCamera;
    public float distanceFromCamera = 5f;
    public Book Book;
    public AutoFlip AutoFlip;

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
                //Book.OnMouseDragRightPage();
                AutoFlip.FlipRightPage();

                Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

                GameObject fireInstance = Instantiate(Fire, spawnPosition, Quaternion.identity);
                fireInstance.SetActive(true);
            }
        }
    }

}
