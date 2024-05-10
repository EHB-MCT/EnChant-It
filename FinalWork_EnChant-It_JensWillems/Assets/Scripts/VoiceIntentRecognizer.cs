using System;
using Meta.WitAi;
using Meta.WitAi.Data.Intents;
using Meta.WitAi.Json;
using UnityEngine;

public class VoiceIntentRecognizer : MonoBehaviour
{
    public GameObject Fire;
    public Camera mainCamera;
    public float DistanceFromCamera = 5f;
    
    public CastingSpell CastingSpell;
    public Menu Menu;


    public void GetIntent(WitResponseNode commandResult)
    {
        WitIntentData witIntentData = commandResult.GetFirstIntentData();
        string intentName = commandResult.GetIntentName();

        Debug.Log("Intent Name: " + intentName);

        if (intentName == "cast_spell")
        {
            string[] spells = commandResult.GetAllEntityValues("spell:spell");
            CastingSpell.UpdateSpell(spells);
        }
        else if (intentName == "menu")
        {
            string[] menuOptions = commandResult.GetAllEntityValues("book:book");
            Menu.UpdateMenu(menuOptions);

        }
        else
        {
            Debug.LogError("This intent or word doesn't exist in the Wit.AI config");
        }

    }
}
