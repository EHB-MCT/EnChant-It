using Meta.WitAi;
using Meta.WitAi.Data.Intents;
using Meta.WitAi.Json;
using UnityEngine;
public class VoiceIntentRecognizer : MonoBehaviour
{
    public CastingSpell CastingSpell;
    public Menu Menu;
    public VoiceAnswers VoiceAnswers;

    public void GetIntent(WitResponseNode commandResult)
    {
        string intentName = commandResult.GetIntentName();

        switch (intentName)
        {
            case "cast_spell":
                CastingSpell.UpdateSpell(commandResult.GetAllEntityValues("Spell:Spell"));
                break;
            case "cast_BookMenu":
                Menu.UpdateMenu(commandResult.GetAllEntityValues("Book:Book"));
                break;
            case "cast_Answer":
                VoiceAnswers.UpdateAnswer(commandResult.GetAllEntityValues("Answer:Answer"));
                break;
            default:
                Debug.LogWarning("This intent or word doesn't exist in the Wit.AI config");
                break;
        }
    }
}
