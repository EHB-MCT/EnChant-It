using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{

    [Header("References")]
    public GameObject WindPrefab;
    public GameObject WindT;
    public BoxCollider SpawnArea;

    [Header("Settings")]
    public int NumberOfWindInstances = 3;
    public float MminSpawnInterval = 5f;
    public float MaxSpawnInterval = 10f;

    private float nextSpawnTime;

    void Start()
    {
        if (SpawnArea == null)
        {
            Debug.LogError("BoxCollider for spawn area is not assigned.");
            enabled = false; 
            return;
        }

        nextSpawnTime = Time.time + Random.Range(MminSpawnInterval, MaxSpawnInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnWind();
            nextSpawnTime = Time.time + Random.Range(MminSpawnInterval, MaxSpawnInterval);
        }
    }

    void SpawnWind()
    {
        for (int i = 0; i < NumberOfWindInstances; i++)
        {
            Vector3 spawnPosition = GetRandomPositionInBoxCollider(SpawnArea);
            GameObject windInstance = Instantiate(WindPrefab, spawnPosition, Quaternion.identity);
            windInstance.transform.SetParent(WindT.transform);

            ParticleSystem particleSystem = windInstance.GetComponent<ParticleSystem>();
            float particleDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constant;
            Destroy(windInstance, particleDuration);
        }
    }

    Vector3 GetRandomPositionInBoxCollider(BoxCollider boxCollider)
    {
        Vector3 center = boxCollider.bounds.center;
        Vector3 size = boxCollider.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }
}
