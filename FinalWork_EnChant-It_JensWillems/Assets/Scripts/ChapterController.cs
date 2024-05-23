using UnityEngine;
using System;

public class ChapterController : MonoBehaviour
{
    public enum Chapter
    {
        Chapter1,
        Chapter2,
        Chapter3
    }

    public Chapter currentChapter = Chapter.Chapter1;

    public PositionManager positionManager;

    public event Action<Chapter> OnChapterChanged;

    public void ChangeChapter(Chapter newChapter)
    {
        currentChapter = newChapter;
        Debug.Log("Changed to chapter: " + currentChapter);

        positionManager.TeleportToChapter(currentChapter);

        OnChapterChanged?.Invoke(currentChapter);

        if (positionManager.playerGameObject != null)
        {
            Vector3 playerPosition = positionManager.playerGameObject.transform.position;
            //Debug.Log("Player position after changing chapter: " + playerPosition);
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
