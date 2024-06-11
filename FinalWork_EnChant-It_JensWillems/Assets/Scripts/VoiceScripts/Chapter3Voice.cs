using Meta.WitAi.Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3Voice : MonoBehaviour
{
    [Header("References")]
    public PositionManager PositionManager;
    public ChapterController ChapterController;
    public VoiceAnswers VoiceAnswers;
    public CastingSpell CastingSpell;
    public Animator EnemyAnimator;
    public GameObject PopUp;
    public PlayerManager PlayerManager;
    public string EnemyAnimationName = "EnemyAnimation";

    [Header("Audio clips")]
    public AudioClip[] AudioClips;

    private AudioSource _audioSource;
    private int _currentClipIndex = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        ChapterController.OnChapterChanged += HandleChapterChanged;
        PositionManager.OnSpawnEffectsCompleted += HandleSpawnEffectsCompleted;

        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter3)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private void OnDestroy()
    {
        ChapterController.OnChapterChanged -= HandleChapterChanged;
        PositionManager.OnSpawnEffectsCompleted -= HandleSpawnEffectsCompleted;
    }

    private void HandleChapterChanged(ChapterController.Chapter newChapter)
    {
        if (newChapter == ChapterController.Chapter.Chapter3)
        {
            if (PositionManager._transitioningBetweenChapters)
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
        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter3)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private IEnumerator PlayDialogue()
    {
        while (_currentClipIndex < AudioClips.Length)
        {
            _audioSource.clip = AudioClips[_currentClipIndex];
            _audioSource.Play();
            yield return new WaitForSeconds(AudioClips[_currentClipIndex].length);

            if (_currentClipIndex == 0 && !VoiceAnswers.Answer)
            {
                PlayerManager.Hit(50);
            }
            if (_currentClipIndex == 1 && !CastingSpell.CastHealSpell)
            {
                yield return new WaitUntil(() => CastingSpell.CastHealSpell);
            }

            if (_currentClipIndex == 2 && !VoiceAnswers.Answer)
            {
                PopUp.SetActive(true);
                VoiceAnswers.CanUpdateAnswer = true;
                yield return new WaitUntil(() => VoiceAnswers.Answer);
                VoiceAnswers.Answer = false;
                VoiceAnswers.CanUpdateAnswer = false;
                PopUp.SetActive(false);
            }

       

            _currentClipIndex++;
        }

        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter3)
        {
            ChapterController.Chapter nextChapter = (ChapterController.Chapter)((int)ChapterController.CurrentChapter + 1);
            ChapterController.ChangeChapter(nextChapter);
        }
        else
        {
            Debug.LogWarning("No next chapter available.");
        }
    }

    private bool IsAnimationFinished()
    {
        return EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !EnemyAnimator.IsInTransition(0);
    }
}
