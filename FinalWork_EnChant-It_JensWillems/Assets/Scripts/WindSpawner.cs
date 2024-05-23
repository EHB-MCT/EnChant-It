using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject WindPrefab;
    public GameObject WindT;

    [Header("Settings")]
    public int NumberOfWindInstances = 3;
    public float MinSpawnInterval = 5f;
    public float MaxSpawnInterval = 10f;
    public float SpawnRadius = 100f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(MinSpawnInterval, MaxSpawnInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnWind();
            nextSpawnTime = Time.time + Random.Range(MinSpawnInterval, MaxSpawnInterval);
        }
    }

    void SpawnWind()
    {
        for (int i = 0; i < NumberOfWindInstances; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
            GameObject windInstance = Instantiate(WindPrefab, spawnPosition, Quaternion.identity);
            windInstance.transform.SetParent(WindT.transform);

            ParticleSystem particleSystem = windInstance.GetComponent<ParticleSystem>();
            float particleDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constant;
            Destroy(windInstance, particleDuration);
        }
    }
}
