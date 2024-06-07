using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int EnemiesAlive = 0;
    public int Round = 0;
    public int MaxRounds = 10;
    public float RoundInterval = 10f;
    public GameObject[] SpawnPoints;
    public GameObject EnemyPrefabs;
    public GameObject PauseMenu;
    public TextMeshProUGUI RoundNum;
    public TextMeshProUGUI RoundsSurvived;
    public GameObject EndScreen;
    public Animator BlackScreenAnimator;
    public bool StartWave = false;
    public ChapterController ChapterController;
    private bool IsRoundInProgress = false;

    private void Start()
    {
        //roundNum.enabled = false;
    }

    void Update()
    {
        if (StartWave && !IsRoundInProgress)
        {
            StartCoroutine(StartNextRound());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    IEnumerator StartNextRound()
    {
        IsRoundInProgress = true;

        // Wait until all enemies are dead
        while (EnemiesAlive > 0)
        {
            yield return null;
        }

        // Wait for the round interval
        yield return new WaitForSeconds(RoundInterval);

        Round++;

        if (Round <= MaxRounds)
        {
            int enemiesToSpawn = Round <= 5 ? Round : 5;
            NextWave(enemiesToSpawn);
            RoundNum.text = "Round: " + Round.ToString();
        }

        if (Round >= MaxRounds)
        {
            Debug.Log("you did it");
            // Play sound for completing all rounds
            // You can add your sound playing code here
        }

        IsRoundInProgress = false;
    }

    public void NextWave(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            GameObject enemySpawned = Instantiate(EnemyPrefabs, spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            EnemiesAlive++;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        EndScreen.SetActive(true);
        RoundsSurvived.text = Round.ToString();
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
        Invoke("LoadMainMenuScene", .4f);
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.volume = 0;
    }

    public void UnPause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.volume = 1;
    }
}
