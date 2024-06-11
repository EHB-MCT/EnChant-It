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
    public Chapter CurrentChapter = Chapter.Chapter1;
    public PositionManager PositionManager;
    public event Action<Chapter> OnChapterChanged;

    private bool _skip;
    private bool initialized = false;

    private void Start()
    {
        CurrentChapter = GameStateManager.Instance.DesiredChapter;
        _skip = SceneTransition.SkipTeleportEffect;
        ChangeChapter(CurrentChapter);
    }

    public void ChangeChapter(Chapter newChapter)
    {
        if (!initialized && _skip)
        {
            initialized = true;
            _skip = false;
            TeleportDirectlyToChapter(newChapter);
        }
        else
        {
            CurrentChapter = newChapter;
            PositionManager.TeleportToChapter(CurrentChapter);
            OnChapterChanged?.Invoke(CurrentChapter);
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

    private void TeleportDirectlyToChapter(Chapter chapter)
    {
        CurrentChapter = chapter;
        PositionManager.PlayerGameObject.transform.position = PositionManager.GetPositionForChapter(chapter).position;
        PositionManager.PlayerGameObject.transform.rotation = PositionManager.GetPositionForChapter(chapter).rotation;
        OnChapterChanged?.Invoke(CurrentChapter);
    }
}
