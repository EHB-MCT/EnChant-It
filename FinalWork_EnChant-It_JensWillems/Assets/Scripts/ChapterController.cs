using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public enum Chapter
    {
        Chapter1,
        Chapter2
    }

    public Chapter currentChapter = Chapter.Chapter1;

    public PositionManager positionManager;

    public void ChangeChapter(Chapter newChapter)
    {
        currentChapter = newChapter;
        Debug.Log("Changed to chapter: " + currentChapter);

        positionManager.TeleportToChapter(currentChapter);

        // Debug player position after changing chapters
        if (positionManager.playerGameObject != null)
        {
            Vector3 playerPosition = positionManager.playerGameObject.transform.position;
            Debug.Log("Player position after changing chapter: " + playerPosition);
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
