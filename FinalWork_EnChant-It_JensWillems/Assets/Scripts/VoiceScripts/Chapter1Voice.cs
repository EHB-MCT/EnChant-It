using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Chapter1Voice : MonoBehaviour
{
    [Header("References")]
    public Menu Menu;
    public VoiceAnswers VoiceAnswers;
    public PositionManager PositionManager;
    public ChapterController ChapterController;
    public GameObject PopUp; 

    [Header("Audio clips")]
    public AudioClip[] AudioClips;

    private AudioSource _audioSource;
    private int _currentClipIndex = 0;
    private bool _menuOpenedFirstTime = false;

    private void Start()
    {
        PopUp.SetActive(false);
        _audioSource = GetComponent<AudioSource>();

        Menu.OnMenuOpenedFirstTime += HandleMenuOpenedFirstTime;


        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter1)
        {
            StartCoroutine(PlayDialogue());
        }
    }

    private void HandleMenuOpenedFirstTime()
    {
        _menuOpenedFirstTime = true;
    }

    private IEnumerator PlayDialogue()
    {
        while (_currentClipIndex < AudioClips.Length)
        {
            _audioSource.clip = AudioClips[_currentClipIndex];
            _audioSource.Play();

            yield return new WaitForSeconds(AudioClips[_currentClipIndex].length);

            if (_currentClipIndex == 1 && !_menuOpenedFirstTime)
            {
                Menu.EnableFirstTimeMenuOpening();
                yield return new WaitUntil(() => _menuOpenedFirstTime);
            }

            if (_currentClipIndex == 3)
            {
                VoiceAnswers.CanUpdateAnswer = true;
                PopUp.SetActive(true);
                yield return new WaitUntil(() => VoiceAnswers.Answer);
                VoiceAnswers.Answer = false;
                VoiceAnswers.CanUpdateAnswer = false;
                PopUp.SetActive(false);
            }

            _currentClipIndex++;
        }

        if (ChapterController.CurrentChapter == ChapterController.Chapter.Chapter1)
        {
            ChapterController.Chapter nextChapter = (ChapterController.Chapter)((int)ChapterController.CurrentChapter + 1);
            ChapterController.ChangeChapter(nextChapter);
        }
        else
        {
            Debug.LogWarning("No next chapter available.");
        }
    }

    private void OnDestroy()
    {
        if (Menu != null)
        {
            Menu.OnMenuOpenedFirstTime -= HandleMenuOpenedFirstTime;
        }
    }
}
