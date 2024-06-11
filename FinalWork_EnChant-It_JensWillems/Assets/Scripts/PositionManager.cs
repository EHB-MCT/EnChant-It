using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PositionManager : MonoBehaviour
{
    [Header("Positions")]
    public List<Transform> Chapter1Positions = new List<Transform>();
    public List<Transform> Chapter2Positions = new List<Transform>();
    public List<Transform> Chapter3Positions = new List<Transform>();
    public List<Transform> Chapter4Positions = new List<Transform>();
    [Header("References")]
    public GameObject PlayerGameObject;
    public GameObject TeleportEffectPrefab;
    public GameObject SpawnEffectPrefab;
    public GameObject Spellbook;

    public Transform TeleportEffectParent;
    public Transform SpawnEffectParent;

    public event Action OnSpawnEffectsCompleted;
    
    public bool _transitioningBetweenChapters = false;
    private bool _skip;

    public void Start()
    {
        _skip = SceneTransition.skipTeleportEffect;
    }
    public void TeleportToChapter(ChapterController.Chapter targetChapter)
    {
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
            Transform newPosition = positions[0];
            if (PlayerGameObject != null)
            {
                if (!_skip)
                {
                    _transitioningBetweenChapters = false;
                }
                if (!_transitioningBetweenChapters)
                {
                    _transitioningBetweenChapters = true;
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
        Spellbook.SetActive(false);
        var playerController = PlayerGameObject.GetComponent<OVRPlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (TeleportEffectPrefab != null)
        {
            GameObject teleportEffectInstance = Instantiate(TeleportEffectPrefab, TeleportEffectParent.transform.position, Quaternion.identity, TeleportEffectParent);
            ParticleSystem[] teleportEffects = teleportEffectInstance.GetComponentsInChildren<ParticleSystem>();
            if (teleportEffects.Length > 0)
            {
                foreach (ParticleSystem ps in teleportEffects)
                {
                    ps.Play();
                }
                yield return new WaitForSeconds(3f);
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
            playerController.enabled = true;
        }

        _transitioningBetweenChapters = false;

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
