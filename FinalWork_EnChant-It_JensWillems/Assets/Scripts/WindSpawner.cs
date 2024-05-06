using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    public GameObject windPrefab;
    public int numberOfWindInstances = 3; // Number of wind instances to spawn at once
    public float minSpawnInterval = 5f; // Minimum time between spawns
    public float maxSpawnInterval = 10f; // Maximum time between spawns
    public float spawnRadius = 20f; // Radius within which the wind can spawn

    private float nextSpawnTime; // Time when the next wind will spawn

    void Start()
    {
        // Initialize nextSpawnTime to start spawning immediately
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Check if it's time to spawn wind
        if (Time.time >= nextSpawnTime)
        {
            SpawnWind();
            // Calculate the time for the next wind spawn
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnWind()
    {
        for (int i = 0; i < numberOfWindInstances; i++)
        {
            // Generate a random position within the spawn radius
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

            // Instantiate the wind prefab at the random position
            GameObject windInstance = Instantiate(windPrefab, spawnPosition, Quaternion.identity);

            // Get the particle system component
            ParticleSystem particleSystem = windInstance.GetComponent<ParticleSystem>();

            // Get the duration of the particle system
            float particleDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constant;

            // Destroy the wind instance after the particle system duration
            Destroy(windInstance, particleDuration);
        }
    }
}
