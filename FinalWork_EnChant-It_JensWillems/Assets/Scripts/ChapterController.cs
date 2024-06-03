using UnityEngine;
using System;

public class ChapterController : MonoBehaviour
{
    public enum Chapter
    {
        Chapter1,
        Chapter2,
        Chapter3,
        Chapter4
    }

    [Header("References")]
    public Chapter currentChapter = Chapter.Chapter1;
    public PositionManager PositionManager;
    public event Action<Chapter> OnChapterChanged;
    private bool _skip;

    private bool initialized = false;

    private void Start()
    {
        currentChapter = GameStateManager.Instance.DesiredChapter;
        _skip = SceneTransition.skipTeleportEffect;
        ChangeChapter(currentChapter);
    }

    public void ChangeChapter(Chapter newChapter)
    {
            Debug.Log("skip: " + _skip);   
        if (!initialized && _skip)
        {
            initialized = true;
            _skip = false;
            Debug.Log("skip: " + _skip);
            TeleportDirectlyToChapter(newChapter);
        }
        else
        {
            Debug.Log("skip: " + _skip);
            currentChapter = newChapter;
            PositionManager.TeleportToChapter(currentChapter);
            OnChapterChanged?.Invoke(currentChapter);
        }

        if (PositionManager.PlayerGameObject != null)
        {
            Vector3 playerPosition = PositionManager.PlayerGameObject.transform.position;
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned.");
        }
    }

    /*
    private void OnValidate()
    {
        ChangeChapter(currentChapter);
    }
    */
    private void TeleportDirectlyToChapter(Chapter chapter)
    {
        currentChapter = chapter;
        PositionManager.PlayerGameObject.transform.position = PositionManager.GetPositionForChapter(chapter).position;
        PositionManager.PlayerGameObject.transform.rotation = PositionManager.GetPositionForChapter(chapter).rotation;
        OnChapterChanged?.Invoke(currentChapter);
    }
}
