using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject Player;
    public GameObject[] EnemyPrefabs; 

    [Header("Settings")]
    public int WaveNumber = 1; 
    public float SpawnRadius = 10f; 
    public float MinSpawnDelay = 2f;
    public float MaxSpawnDelay = 5f; 

    private List<GameObject> _activeEnemies = new List<GameObject>();
    private int enemiesPerWave;
    private bool spawning = false;

    /*
    private void Start()
    {
        StartSpawning();
    }
    */
    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            enemiesPerWave = WaveNumber * 5;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
            }

            yield return new WaitUntil(() => _activeEnemies.Count == 0);

            WaveNumber++;
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, EnemyPrefabs.Length);

        Vector2 spawnPosition = Random.insideUnitCircle * SpawnRadius;
        Vector3 enemyPosition = new Vector3(Player.transform.position.x + spawnPosition.x, Player.transform.position.y, Player.transform.position.z + spawnPosition.y);

        GameObject enemy = Instantiate(EnemyPrefabs[enemyIndex], enemyPosition, Quaternion.identity);

        _activeEnemies.Add(enemy);

        enemy.GetComponent<Enemy>().OnDestroyed += () => _activeEnemies.Remove(enemy);
    }
}
