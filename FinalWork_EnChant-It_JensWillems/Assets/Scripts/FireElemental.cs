using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireElemental : MonoBehaviour
{
    public Text dialogueText;
    private int currentLine = 0;
    private bool dialogueFinished = false;

    private string[] conversationLines = {
        "Greetings, young adventurer! I am Flicker, the fire elemental.",
        "I see the flame of courage burning within you!",
        "Let me teach you the secrets of fire magic.",
        "To cast a fire spell, simply say 'ignite'.",
        "Now, repeat after me: 'ignite'.",
        "Excellent! With practice, you'll master the flames in no time!",
        "Remember, fire is both powerful and unpredictable. Use it wisely.",
        "Are you ready to harness the power of fire?"
    };

    private void Start()
    {
        StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        // Show dialogue line by line
        while (currentLine < conversationLines.Length)
        {
            dialogueText.text = conversationLines[currentLine];
            currentLine++;
            yield return new WaitForSeconds(2f); // Adjust the delay between lines if needed
        }

        dialogueFinished = true;
    }

    private void Update()
    {
        // Check if the dialogue is finished
        if (dialogueFinished)
        {
            // Optionally, wait for player input here
            // For this example, we'll just assume the player is ready after the dialogue finishes
            Debug.Log("Player is ready to harness the power of fire.");
            // You can proceed with whatever action you want here
        }
    }
}
