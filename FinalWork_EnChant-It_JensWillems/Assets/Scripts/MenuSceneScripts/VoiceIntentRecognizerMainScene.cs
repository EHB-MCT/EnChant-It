using Meta.WitAi;
using Meta.WitAi.Data.Intents;
using Meta.WitAi.Json;
using UnityEngine;

public class VoiceIntentRecognizerMainScene : MonoBehaviour
{
    public SceneTransition SceneTransition;

    public void GetIntent(WitResponseNode commandResult)
    {
        string intentName = commandResult.GetIntentName();
        Debug.Log(intentName + ": this is being done");
        switch (intentName)
        {
            
            case "cast_MainMenu":
                Debug.Log(intentName + ": Scene trans");
                SceneTransition.UpdateOption(commandResult.GetAllEntityValues("Option:Option"));
                break;

            default:
                Debug.LogWarning("This intent or word doesn't exist in the Wit.AI config");
                break;
        }
    }
}
