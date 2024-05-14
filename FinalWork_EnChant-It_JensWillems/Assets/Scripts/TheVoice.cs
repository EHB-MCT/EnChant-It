using UnityEngine;
using System.Collections;

public class TheVoice : MonoBehaviour
{
    [Header("References")]
    public Menu Menu;
    public VoiceAnswers VoiceAnswers;
    public PositionManager PositionManager;
    public ChapterController chapterController; // Reference to the ChapterController script

    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Only start playing dialogue if the player is in chapter 1
        if (chapterController.currentChapter == ChapterController.Chapter.Chapter1)
        {
            StartCoroutine(PlayDialogue());
        }
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
                yield return new WaitUntil(() => VoiceAnswers.Answer);
            }

            currentClipIndex++;
        }

        if (chapterController.currentChapter == ChapterController.Chapter.Chapter1)
        {
            if (chapterController.currentChapter == ChapterController.Chapter.Chapter1)
            {
                ChapterController.Chapter nextChapter = (ChapterController.Chapter)((int)chapterController.currentChapter + 1);

                chapterController.ChangeChapter(nextChapter);
            }
            else
            {
                Debug.LogWarning("No next chapter available.");
            }
        }
    }
}
