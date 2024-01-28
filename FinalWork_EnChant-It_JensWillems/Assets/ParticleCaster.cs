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
    
    public void SetSpell()
    {
        // You can add any initialization logic for spells here
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
        Debug.Log("2");
        if (spells.Length != 0)
        {
            Debug.Log("3");
            CastSpell(spells);
            return;
        }

        if (spells.Length == 0 || spells[0] == "spell")
        {
            Debug.Log("4");
            
            return;
        }
    }

    public void CastSpell(string[] spellNames)
    {
        Debug.Log("5");
        if (spellNames.Length > 0)
        {
            Debug.Log("test");
            // Assuming you have only one spell in the array for simplicity
            string spellName = spellNames[0];

            // Check if the spell matches a predefined spell name
            if (spellName.Equals("fire", StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("found spell");
                // Instantiate and activate the FirePrefab instead of the general ParticleEffect
                GameObject fireInstance = Instantiate(Fire, transform.position, Quaternion.identity);
                fireInstance.SetActive(true);

                // Add any additional logic here, such as handling spell effects or duration
            }
            // Add more else-if blocks for other spells

            // Note: Make sure to configure the FirePrefab in the Unity Editor with the appropriate settings.
        }
    }
}
