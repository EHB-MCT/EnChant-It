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

    public void ChangeChapter(Chapter newChapter)
    {
        currentChapter = newChapter;
        PositionManager.TeleportToChapter(currentChapter);
        OnChapterChanged?.Invoke(currentChapter);

        if (PositionManager.PlayerGameObject != null)
        {
            Vector3 playerPosition = PositionManager.PlayerGameObject.transform.position;
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned.");
        }
    }

    private void OnValidate()
    {
        ChangeChapter(currentChapter);
    }
}
