using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    public GameObject Spellbook;

    public bool transitioningBetweenChapters = false;
    private bool _skip;
    public event Action OnSpawnEffectsCompleted;
    public void Start()
    {
        _skip = SceneTransition.skipTeleportEffect;
    }
    public void TeleportToChapter(ChapterController.Chapter targetChapter)
    {
        Debug.Log("TeleportToChapter method called.");

        List<Transform> positions = null;

        switch (targetChapter)
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
            Debug.Log("Teleporting to chapter: " + targetChapter);

            Transform newPosition = positions[0];
            if (PlayerGameObject != null)
            {
                Debug.Log("PlayerGameObject is not null.");
                if (!_skip)
                {
                    transitioningBetweenChapters = false;
                }
                if (!transitioningBetweenChapters)
                {
                    Debug.Log("yes");
                    transitioningBetweenChapters = true;
                    StartCoroutine(TeleportWithEffects(newPosition.position, newPosition.rotation));
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

    private IEnumerator TeleportWithEffects(Vector3 newPosition, Quaternion newRotation)
    {
        Debug.Log("TeleportWithEffects coroutine called.");
        Spellbook.SetActive(false);
        var playerController = PlayerGameObject.GetComponent<OVRPlayerController>();
        if (playerController != null)
        {
            Debug.Log("Disabling player controller.");
            playerController.enabled = false;
        }

        Debug.Log("Playing teleportation effects.");

        if (TeleportEffectPrefab != null)
        {
            GameObject teleportEffectInstance = Instantiate(TeleportEffectPrefab, TeleportEffectParent.transform.position, Quaternion.identity, TeleportEffectParent);
            ParticleSystem[] teleportEffects = teleportEffectInstance.GetComponentsInChildren<ParticleSystem>();
            if (teleportEffects.Length > 0)
            {
                Debug.Log("Playing teleport effects.");
                foreach (ParticleSystem ps in teleportEffects)
                {
                    ps.Play();
                }
                yield return new WaitForSeconds(4f);
                Debug.Log("Teleport effects finished.");
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

        PlayerGameObject.transform.position = newPosition;
        PlayerGameObject.transform.rotation = newRotation;
        Debug.Log($"Player teleported to {newPosition}.");

        if (SpawnEffectPrefab != null)
        {
            Debug.Log("Playing spawn effects.");

            GameObject spawnEffectInstance = Instantiate(SpawnEffectPrefab, TeleportEffectParent.transform.position, Quaternion.identity, SpawnEffectParent);
            ParticleSystem[] spawnEffects = spawnEffectInstance.GetComponentsInChildren<ParticleSystem>();
            if (spawnEffects.Length > 0)
            {
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

        if (playerController != null)
        {
            Debug.Log("Enabling player controller.");
            playerController.enabled = true;
        }

        transitioningBetweenChapters = false;

        OnSpawnEffectsCompleted?.Invoke();
    }

    public Transform GetPositionForChapter(ChapterController.Chapter chapter)
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
            return positions[0];
        }
        else
        {
            Debug.LogError("No teleportation points found for the selected chapter.");
            return null;
        }
    }
}
