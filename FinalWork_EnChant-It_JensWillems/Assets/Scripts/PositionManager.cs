/*using UnityEngine;
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
*/

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
                var playerController = playerGameObject.GetComponent<OVRPlayerController>();
                if (playerController != null)
                {
                    playerController.enabled = false;
                }

                playerGameObject.transform.position = newPosition.position;
                playerGameObject.transform.rotation = newPosition.rotation;

                if (playerController != null)
                {
                    playerController.enabled = true;
                }
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TeleportRandomPosition();
        }
    }

    void TeleportRandomPosition()
    {
        if (playerGameObject != null)
        {
            var playerController = playerGameObject.GetComponent<OVRPlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            playerGameObject.transform.position = new Vector3(Random.Range(-5, 5), playerGameObject.transform.position.y, Random.Range(-5, 5));

            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }
}
