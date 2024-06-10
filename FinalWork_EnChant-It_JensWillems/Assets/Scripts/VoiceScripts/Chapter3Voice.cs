using Meta.WitAi.Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3Voice : MonoBehaviour
{
    [Header("References")]
    public PositionManager PositionManager;
    public ChapterController chapterController;
    public VoiceAnswers VoiceAnswers;
    public CastingSpell CastingSpell;
    public Animator enemyAnimator; // Reference to the enemy's Animator component
    public string enemyAnimationName = "EnemyAnimation"; // Name of the enemy animation

    [Header("Audio clips")]
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        chapterController.OnChapterChanged += HandleChapterChanged;
        PositionManager.OnSpawnEffectsCompleted += HandleSpawnEffectsCompleted;

        if (chapterController.currentChapter == ChapterController.Chapter.Chapter3)
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
        if (newChapter == ChapterController.Chapter.Chapter3)
        {
            if (PositionManager.transitioningBetweenChapters)
            {
                // Additional logic if needed during transitions
            }
            else
            {
                StartCoroutine(PlayDialogue());
            }
        }
    }

    private void HandleSpawnEffectsCompleted()
    {
        if (chapterController.currentChapter == ChapterController.Chapter.Chapter3)
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
                // Uncomment and implement this if needed
                // yield return new WaitUntil(() => VoiceAnswers.Answer);
            }
            if (currentClipIndex == 1) 
            {
                enemyAnimator.SetTrigger("Attack");
                yield return new WaitUntil(() => IsAnimationFinished());
                enemyAnimator.SetTrigger("Idle");

            }
            if (currentClipIndex == 2 && !CastingSpell.CastHealSpell)
            {
                yield return new WaitUntil(() => CastingSpell.CastHealSpell);
            }

            if (currentClipIndex == 5 && !VoiceAnswers.Answer)
            {
                VoiceAnswers.CanUpdateAnswer = true;
                yield return new WaitUntil(() => VoiceAnswers.Answer);
                VoiceAnswers.Answer = false;
                VoiceAnswers.CanUpdateAnswer = false; // Reset the flag
            }

       

            currentClipIndex++;
        }

        if (chapterController.currentChapter == ChapterController.Chapter.Chapter3)
        {
            ChapterController.Chapter nextChapter = (ChapterController.Chapter)((int)chapterController.currentChapter + 1);
            chapterController.ChangeChapter(nextChapter);
        }
        else
        {
            Debug.LogWarning("No next chapter available.");
        }
    }

    private bool IsAnimationFinished()
    {
        // Check if the enemy's animation is done
        return enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !enemyAnimator.IsInTransition(0);
    }
}
