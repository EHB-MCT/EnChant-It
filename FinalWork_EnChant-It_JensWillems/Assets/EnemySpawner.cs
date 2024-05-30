using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject[] enemyPrefabs; // Array to hold the different enemy prefabs

    [Header("Settings")]
    public int waveNumber = 1; // Initial wave number
    public float spawnRadius = 10f; // Radius around the player to spawn enemies
    public float minSpawnDelay = 2f; // Minimum spawn delay
    public float maxSpawnDelay = 5f; // Maximum spawn delay

    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemiesPerWave;

    void Start()
    {
        // Start the first wave
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            // Calculate number of enemies for the current wave
            enemiesPerWave = waveNumber * 5;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }

            // Wait until all enemies are destroyed
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            // Increment the wave number for the next wave
            waveNumber++;
        }
    }

    void SpawnEnemy()
    {
        // Randomly select an enemy type from the array
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // Calculate a random position around the player
        Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;
        Vector3 enemyPosition = new Vector3(player.transform.position.x + spawnPosition.x, player.transform.position.y, player.transform.position.z + spawnPosition.y);

        // Instantiate the enemy at the calculated position
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], enemyPosition, Quaternion.identity);

        // Add the spawned enemy to the active enemies list
        activeEnemies.Add(enemy);

        // Register the enemy's destruction
        enemy.GetComponent<Enemy>().OnDestroyed += () => activeEnemies.Remove(enemy);
    }
}
