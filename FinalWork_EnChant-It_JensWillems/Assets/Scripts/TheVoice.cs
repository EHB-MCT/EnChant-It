using System.Collections;
using UnityEngine;

public class TheVoice : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    public Menu Menu;
    public VoiceAnswers VoiceAnswers;
    public PositionManager PositionManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        while (currentClipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

            yield return new WaitForSeconds(audioClips[currentClipIndex].length);

            if (currentClipIndex == 2 && !Menu.OpenMenuFirstTime)
            {
                yield return new WaitUntil(() => Menu.OpenMenuFirstTime);
            }
            if (currentClipIndex == 4 && !VoiceAnswers.Answer)
            {
                yield return new WaitUntil(() =>VoiceAnswers.Answer);
            }

            // Move to the next clip
            currentClipIndex++;
          
        }
        PositionManager.TeleportToPosition(1);
        Debug.Log("Dialogue finished.");
    }
}
