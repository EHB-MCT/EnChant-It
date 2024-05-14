using UnityEngine;
using System.Collections.Generic;

public class PositionManager : MonoBehaviour
{
    [Header("References")]
    public List<Transform> chapter1Positions = new List<Transform>(); 
    public List<Transform> chapter2Positions = new List<Transform>(); 
    public GameObject playerGameObject;

    public void TeleportToChapter(ChapterController.Chapter chapter)
    {
        List<Transform> positions = null;

        switch (chapter)
        {
            case ChapterController.Chapter.Chapter1:
                positions = chapter1Positions;
                break;
            case ChapterController.Chapter.Chapter2:
                positions = chapter2Positions;
                break;
        }

        if (positions != null && positions.Count > 0)
        {
            Transform newPosition = positions[0]; 
            if (playerGameObject != null)
            {
                playerGameObject.transform.position = newPosition.position;
                playerGameObject.transform.rotation = newPosition.rotation;
            }
            else
            {
                Debug.LogError("Player GameObject is not assigned.");
            }
        }
        else
        {
            Debug.LogError("No teleportation points found for the selected chapter.");
        }
    }
}
