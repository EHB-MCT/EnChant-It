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
    }

    private void OnValidate()
    {
        ChangeChapter(currentChapter);
    }
}
