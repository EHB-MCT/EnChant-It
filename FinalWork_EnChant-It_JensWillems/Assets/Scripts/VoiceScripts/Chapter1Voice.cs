using UnityEngine;
using System.Collections;

public class Chapter1Voice : MonoBehaviour
{
    [Header("References")]
    public Menu Menu;
    public VoiceAnswers VoiceAnswers;
    public PositionManager PositionManager;
    public ChapterController chapterController;

    [Header("Audio clips")]
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool menuOpenedFirstTime = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Subscribe to the OnMenuOpenedFirstTime event
        Menu.OnMenuOpenedFirstTime += HandleMenuOpenedFirstTime;

        // Only start playing dialogue if the player is in chapter 1
        if (chapterController.currentChapter == ChapterController.Chapter.Chapter1)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private void HandleMenuOpenedFirstTime()
    {
        menuOpenedFirstTime = true;
    }

    private IEnumerator PlayDialogue()
    {
        while (currentClipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

            yield return new WaitForSeconds(audioClips[currentClipIndex].length);

            if (currentClipIndex == 1 && !menuOpenedFirstTime)
            {
                Menu.EnableFirstTimeMenuOpening();
                yield return new WaitUntil(() => menuOpenedFirstTime);
            }
            if (currentClipIndex == 3 && !VoiceAnswers.Answer)
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

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (Menu != null)
        {
            Menu.OnMenuOpenedFirstTime -= HandleMenuOpenedFirstTime;
        }
    }
}
