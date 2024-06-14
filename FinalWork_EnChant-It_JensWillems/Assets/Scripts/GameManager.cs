using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public int EnemiesAlive = 0;
    public int Round = 0;
    public int MaxRounds = 10;
    public float RoundInterval = 10f;
    public GameObject[] SpawnPoints;
    public GameObject EnemyPrefabs;
    public TextMeshProUGUI RoundNum;
    public TextMeshProUGUI RoundsSurvived;
    public GameObject EndScreen;
    public GameObject WinScreen;
    public Animator BlackScreenAnimator;
    public bool StartWave = false;
    public ChapterController ChapterController;
    private bool IsRoundInProgress = false;

    public GameObject HealthBar;

    private List<GameObject> SpawnedEnemies = new List<GameObject>();


    void Update()
    {
        if (StartWave && !IsRoundInProgress)
        {
            StartCoroutine(StartNextRound());
        }
    }

    IEnumerator StartNextRound()
    {
        IsRoundInProgress = true;

        while (EnemiesAlive > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(RoundInterval);

        Round++;

        if (Round <= MaxRounds)
        {
            int enemiesToSpawn = Round <= 5 ? Round : 5;
            NextWave(enemiesToSpawn);
            RoundNum.text = "Round: " + Round.ToString();
        }

        if (Round > MaxRounds)
        {
            WinGame();
        }

        IsRoundInProgress = false;
    }

    public void NextWave(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            GameObject enemySpawned = Instantiate(EnemyPrefabs, spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<EnemyManager>().GameManager = this;
            EnemiesAlive++;
            SpawnedEnemies.Add(enemySpawned);
        }
    }

    public void WinGame()
    {
        HealthBar.SetActive(false);
        RoundNum.enabled = false;
        RoundNum.text = "";
        Cursor.lockState = CursorLockMode.None;
        WinScreen.SetActive(true);
        RoundsSurvived.text = Round.ToString();
    }

    public void EndGame()
    {
        HealthBar.SetActive(false);
        RoundNum.enabled = false;
        RoundNum.text = "";
        Cursor.lockState = CursorLockMode.None;
        EndScreen.SetActive(true);
        RoundsSurvived.text = Round.ToString();

        DestroyAllEnemies();
    }

    private void DestroyAllEnemies()
    {
        foreach (GameObject enemy in SpawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        SpawnedEnemies.Clear();
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Round = 0;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        BlackScreenAnimator.SetTrigger("FadeIn");
        HealthBar.SetActive(true);
        Invoke("LoadMainMenuScene", .4f);
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
