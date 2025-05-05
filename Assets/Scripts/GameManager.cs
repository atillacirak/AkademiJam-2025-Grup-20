using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float survivalTimeGoal = 120f; 
    [SerializeField] private bool isGameOver = false;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minSpawnRadius = 8f;
    [SerializeField] private float maxSpawnRadius = 12f;
    [SerializeField] private float spawnInterval = 1.5f;

    [SerializeField] private Transform coreTransform; 
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;

    private int score = 0;
    private float gameTimer = 0f;
    private Coroutine spawnCoroutine;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        score = 0;
        gameTimer = 0f;
        isGameOver = false;
        UpdateScoreUI();
        spawnCoroutine = StartCoroutine(SpawnEnemyRoutine());
    }

    void Update()
    {
        if (isGameOver) return;

        gameTimer += Time.deltaTime;

        UpdateTimerUI();

        if (gameTimer >= survivalTimeGoal)
        {
            WinGame();
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnInterval);

            float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float randomRadius = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector3 spawnOffset = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * randomRadius;
            Vector3 spawnPosition = coreTransform.position + spawnOffset;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void UpdateTimerUI()
    {
        if (isGameOver) return; 


            float remainingTime = survivalTimeGoal - gameTimer;

            remainingTime = Mathf.Max(0f, remainingTime);

            int secondsRemaining = Mathf.CeilToInt(remainingTime);

            timerText.text = "Time: " + secondsRemaining;
        
    }

    public void IncrementScore()
    {
        if (isGameOver) return;
        score++;
        UpdateScoreUI();
    }

    public void EnemyReachedCore(EnemyMovement enemy)
    {
        if (isGameOver) return; 
        isGameOver = true;


        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        LoadNextScene();
    }

    void WinGame()
    {
        if (isGameOver) return;

        isGameOver = true;

        // zafer ekle

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        LoadNextScene();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}