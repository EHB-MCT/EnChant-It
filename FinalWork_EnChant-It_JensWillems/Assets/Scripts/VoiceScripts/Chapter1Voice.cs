using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Chapter1Voice : MonoBehaviour
{
    [Header("References")]
    public Menu Menu;
    public VoiceAnswers VoiceAnswers;
    public PositionManager PositionManager;
    public ChapterController chapterController;

    public Slider slider; 
    public GameObject sliderObject; 

    [Header("Audio clips")]
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool menuOpenedFirstTime = false;

    private void Start()
    {
        sliderObject.SetActive(false);
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

            if (currentClipIndex == 3)
            {
                VoiceAnswers.CanUpdateAnswer = true;
                ShowSlider();
                yield return new WaitUntil(() => VoiceAnswers.Answer);
                VoiceAnswers.Answer = false;
                VoiceAnswers.CanUpdateAnswer = false; // Reset the flag
            }

            currentClipIndex++;
        }

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

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (Menu != null)
        {
            Menu.OnMenuOpenedFirstTime -= HandleMenuOpenedFirstTime;
        }
    }


    public void ShowSlider()
    {
        sliderObject.SetActive(true);
        slider.value = 0;
        //StartCoroutine(IncreaseSliderValue());
    }

    /*
    private IEnumerator IncreaseSliderValue()
    {
        float elapsedTime = 0f;
        float duration = 10f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(0, 1, elapsedTime / duration);
            yield return null;
        }
        sliderObject.SetActive(false);
        slider.value = 0;
    }
    */
}
