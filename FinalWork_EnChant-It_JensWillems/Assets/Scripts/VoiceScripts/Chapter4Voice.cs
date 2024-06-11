using System.Collections;
using UnityEngine;

public class Chapter4Voice : MonoBehaviour
{
    [Header("References")]
    public PositionManager PositionManager;
    public ChapterController ChapterController;
    public VoiceAnswers VoiceAnswers;
    public GameManager GameManager;
    public GameObject PopUp;

    [Header("Audio clips")]
    public AudioClip[] AudioClips;

    private AudioSource _audioSource;
    private int _currentClipIndex = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        ChapterController.OnChapterChanged += HandleChapterChanged;

        PositionManager.OnSpawnEffectsCompleted += HandleSpawnEffectsCompleted;

        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter4)
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
        if (newChapter == ChapterController.Chapter.Chapter4)
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
        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter4)
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
                PopUp.SetActive(true);
                VoiceAnswers.CanUpdateAnswer = true;
                yield return new WaitUntil(() => VoiceAnswers.Answer);
                VoiceAnswers.Answer = false;
                VoiceAnswers.CanUpdateAnswer = false;
                PopUp.SetActive(false);
            }
            _currentClipIndex++;
        }
        GameManager.StartWave = true;

    }
}
