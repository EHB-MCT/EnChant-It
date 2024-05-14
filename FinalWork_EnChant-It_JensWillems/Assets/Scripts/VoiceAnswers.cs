using System;
using UnityEngine;

public class VoiceAnswers : MonoBehaviour
{
    public bool Answer = false;
    public void UpdateAnswer(string[] answerCommands)
    {
        if (answerCommands.Length != 0)
        {
            MenuCommands(answerCommands);
            return;
        }

        if (answerCommands.Length == 0 || answerCommands[0] == "answer")
        {
            return;
        }
    }

    public void MenuCommands(string[] annswerCommand)
    {
        if (annswerCommand.Length > 0)
        {
            string answerCommandName = annswerCommand[0];

            if (answerCommandName.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Answer = true;
            }

        }
    }
}
