using System.Collections;
using UnityEngine;

public class Chapter2Voice : MonoBehaviour
{
    [Header("References")]
    public PositionManager PositionManager;
    public ChapterController chapterController;
    public VoiceAnswers VoiceAnswers;
    public CastingSpell CastingSpell;

    [Header("Audio clips")]
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        chapterController.OnChapterChanged += HandleChapterChanged;

        PositionManager.OnSpawnEffectsCompleted += HandleSpawnEffectsCompleted;

        if (chapterController.currentChapter == ChapterController.Chapter.Chapter2)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private void OnDestroy()
    {
        chapterController.OnChapterChanged -= HandleChapterChanged;
        PositionManager.OnSpawnEffectsCompleted -= HandleSpawnEffectsCompleted;
    }

    private void HandleChapterChanged(ChapterController.Chapter newChapter)
    {
        if (newChapter == ChapterController.Chapter.Chapter2)
        {
            if (PositionManager.transitioningBetweenChapters)
            {
            }
            else
            {
                StartCoroutine(PlayDialogue());
            }
        }
    }

    private void HandleSpawnEffectsCompleted()
    {
        if (chapterController.currentChapter == ChapterController.Chapter.Chapter2)
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

            if (currentClipIndex == 0 && !VoiceAnswers.Answer)
            {
                yield return new WaitUntil(() => VoiceAnswers.Answer);
            }
            if (currentClipIndex == 2 && !CastingSpell.CastFireSpell)
            {
                yield return new WaitUntil(() => CastingSpell.CastFireSpell);
            }
            currentClipIndex++;
        }

       
        if (chapterController.currentChapter == ChapterController.Chapter.Chapter2)
        {
            //ChapterController.Chapter nextChapter = (ChapterController.Chapter)((int)chapterController.currentChapter + 1);

           //chapterController.ChangeChapter(nextChapter);
        }
        else
        {
            Debug.LogWarning("No next chapter available.");
        }
    }
}
