using System;
using UnityEngine;

public class VoiceAnswers : MonoBehaviour
{
    public bool Answer = false;
    public bool CanUpdateAnswer = false; 

    public void UpdateAnswer(string[] answerCommands)
    {
        if (!CanUpdateAnswer) return; 

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

    public void MenuCommands(string[] answerCommands)
    {
        if (!CanUpdateAnswer) return; 

        if (answerCommands.Length > 0)
        {
            string answerCommandName = answerCommands[0];

            if (answerCommandName.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Answer = true;
            }
        }
    }
}
