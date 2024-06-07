using System.Collections;
using UnityEngine;

public class Chapter4Voice : MonoBehaviour
{
    [Header("References")]
    public PositionManager PositionManager;
    public ChapterController chapterController;
    public VoiceAnswers VoiceAnswers;
    public GameManager GameManager;

    [Header("Audio clips")]
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        chapterController.OnChapterChanged += HandleChapterChanged;

        PositionManager.OnSpawnEffectsCompleted += HandleSpawnEffectsCompleted;

        if (chapterController.currentChapter == ChapterController.Chapter.Chapter4)
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
        if (newChapter == ChapterController.Chapter.Chapter4)
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
        if (chapterController.currentChapter == ChapterController.Chapter.Chapter4)
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
                VoiceAnswers.CanUpdateAnswer = true;
                yield return new WaitUntil(() => VoiceAnswers.Answer);
                VoiceAnswers.Answer = false;
                VoiceAnswers.CanUpdateAnswer = false; 
            }
            currentClipIndex++;
        }
        GameManager.StartWave = true;

    }
}
