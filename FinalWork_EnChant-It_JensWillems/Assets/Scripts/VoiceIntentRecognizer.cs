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
                CastingSpell.UpdateSpell(commandResult.GetAllEntityValues("spell:spell"));
                break;
            case "menu":
                Menu.UpdateMenu(commandResult.GetAllEntityValues("book:book"));
                break;
            case "say_answer":
                VoiceAnswers.UpdateAnswer(commandResult.GetAllEntityValues("Answer:Answer"));
                break;
            default:
                Debug.LogWarning("This intent or word doesn't exist in the Wit.AI config");
                break;
        }
    }
}
