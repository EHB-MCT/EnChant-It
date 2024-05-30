using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PositionManager : MonoBehaviour
{
    [Header("References")]
    public List<Transform> Chapter1Positions = new List<Transform>();
    public List<Transform> Chapter2Positions = new List<Transform>();
    public List<Transform> Chapter3Positions = new List<Transform>();
    public List<Transform> Chapter4Positions = new List<Transform>();
    public GameObject PlayerGameObject;
    public GameObject TeleportEffectPrefab;
    public GameObject SpawnEffectPrefab;
    public Transform TeleportEffectParent;
    public Transform SpawnEffectParent;

    public bool transitioningBetweenChapters = false;

    public event System.Action OnSpawnEffectsCompleted;

    public void TeleportToChapter(ChapterController.Chapter chapter)
    {
        List<Transform> positions = null;

        switch (chapter)
        {
            case ChapterController.Chapter.Chapter1:
                positions = Chapter1Positions;
                break;
            case ChapterController.Chapter.Chapter2:
                positions = Chapter2Positions;
                break;
            case ChapterController.Chapter.Chapter3:
                positions = Chapter3Positions;
                break;
            case ChapterController.Chapter.Chapter4:
                positions = Chapter4Positions;
                break;
        }

        if (positions != null && positions.Count > 0)
        {
            Transform newPosition = positions[0];
            if (PlayerGameObject != null)
            {
                if (chapter != ChapterController.Chapter.Chapter1)
                {
                    transitioningBetweenChapters = true;
                }
                StartCoroutine(TeleportWithEffects(newPosition.position, newPosition.rotation, chapter));
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

    private IEnumerator TeleportWithEffects(Vector3 newPosition, Quaternion newRotation, ChapterController.Chapter chapter)
    {
        var playerController = PlayerGameObject.GetComponent<OVRPlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
           // Debug.Log("Player controller disabled.");
        }

        if (transitioningBetweenChapters)
        {
            if (chapter != ChapterController.Chapter.Chapter1)
            {
                if (TeleportEffectPrefab != null)
                {
                    GameObject teleportEffectInstance = Instantiate(TeleportEffectPrefab, TeleportEffectParent.transform.position, Quaternion.identity, TeleportEffectParent);
                    ParticleSystem[] teleportEffects = teleportEffectInstance.GetComponentsInChildren<ParticleSystem>();
                    if (teleportEffects.Length > 0)
                    {
                        //Debug.Log("Playing teleport effects.");
                        foreach (ParticleSystem ps in teleportEffects)
                        {
                            ps.Play();
                        }
                        yield return new WaitForSeconds(4f);
                        //Debug.Log("Teleport effects finished.");
                        Destroy(teleportEffectInstance);
                    }
                    else
                    {
                        Debug.LogError("Teleport effect ParticleSystem components not found.");
                    }
                }
                else
                {
                    Debug.LogError("Teleport effect prefab is not assigned.");
                }
            }

            PlayerGameObject.transform.position = newPosition;
            PlayerGameObject.transform.rotation = newRotation;
            Debug.Log($"Player teleported to {newPosition}.");

            if (chapter != ChapterController.Chapter.Chapter1)
            {
                if (SpawnEffectPrefab != null)
                {
                    GameObject spawnEffectInstance = Instantiate(SpawnEffectPrefab, TeleportEffectParent.transform.position, Quaternion.identity, SpawnEffectParent);
                    ParticleSystem[] spawnEffects = spawnEffectInstance.GetComponentsInChildren<ParticleSystem>();
                    if (spawnEffects.Length > 0)
                    {
                        Debug.Log("Playing spawn effects.");
                        foreach (ParticleSystem ps in spawnEffects)
                        {
                            ps.Play();
                        }
                        yield return new WaitForSeconds(spawnEffects[0].main.duration);
                        Debug.Log("Spawn effects finished.");
                        Destroy(spawnEffectInstance);
                    }
                    else
                    {
                        Debug.LogError("Spawn effect ParticleSystem components not found.");
                    }
                }
                else
                {
                    Debug.LogError("Spawn effect prefab is not assigned.");
                }
            }

            if (playerController != null)
            {
                playerController.enabled = true;
                //Debug.Log("Player controller enabled.");
            }

            transitioningBetweenChapters = false;
        }
        else
        {
            // If not transitioning between chapters, simply teleport the player
            PlayerGameObject.transform.position = newPosition;
            PlayerGameObject.transform.rotation = newRotation;
            //Debug.Log($"Player teleported to {newPosition}.");

            if (playerController != null)
            {
                playerController.enabled = true;
               // Debug.Log("Player controller enabled.");
            }
        }

        // Trigger the event when done
        OnSpawnEffectsCompleted?.Invoke();
    }
}
