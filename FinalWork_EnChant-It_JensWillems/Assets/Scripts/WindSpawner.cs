using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    public GameObject windPrefab;
    public int numberOfWindInstances = 3;
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;
    public float spawnRadius = 20f;
    public GameObject WindT;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnWind();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnWind()
    {
        for (int i = 0; i < numberOfWindInstances; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject windInstance = Instantiate(windPrefab, spawnPosition, Quaternion.identity);

            windInstance.transform.SetParent(WindT.transform);

            ParticleSystem particleSystem = windInstance.GetComponent<ParticleSystem>();
            float particleDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constant;
            Destroy(windInstance, particleDuration);
        }
    }
}
