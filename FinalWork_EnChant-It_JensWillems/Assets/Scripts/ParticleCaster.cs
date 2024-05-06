using System;
using Meta.WitAi;
using Meta.WitAi.Data.Intents;
using Meta.WitAi.Json;
using Oculus.Interaction.Deprecated;
using UnityEngine;

public class ParticleCaster : MonoBehaviour
{
    public GameObject Fire;
    private WitResponseData wit;
    public Camera mainCamera;
    public float distanceFromCamera = 5f;

    public void SetSpell()
    {

    }

    public void UpdateSpell(WitResponseNode commandResult)
    {
        WitIntentData witIntentData = commandResult.GetFirstIntentData();
        string intentName = commandResult.GetIntentName();

        Debug.Log("Intent Name: " + intentName);
        string[] spells = commandResult.GetAllEntityValues("spell:spell");
        UpdateSpell(spells);
    }

    public void UpdateSpell(string[] spells)
    {
        Debug.Log("Intent: " + wit);
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

                Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

                GameObject fireInstance = Instantiate(Fire, spawnPosition, Quaternion.identity);
                fireInstance.SetActive(true);
            }
        }
    }
}
