using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class PacmanGameManager : MonoBehaviour
{
    public static PacmanGameManager Instance { get; private set; }

    public float gameTime = 60f;
    private float timer;
    private int totalBugs;
    private int bugsRemaining;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bugsCountText;

    private bool gameIsOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Time.timeScale = 1f;

        GameObject[] bugs = GameObject.FindGameObjectsWithTag("Bugs");
        totalBugs = bugs.Length;
        bugsRemaining = totalBugs;

        timer = gameTime;

        UpdateUI();
    }

    void Update()
    {
        if (gameIsOver) return; 

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            LoseGame();
        }

        UpdateUI();
    }

    public void BugsEaten()
    {
        if (gameIsOver) return;

        bugsRemaining--;
        UpdateUI();

        if (bugsRemaining <= 0)
        {
            WinGame();
        }
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2");
        }
        if (bugsCountText != null)
        {
            bugsCountText.text = "Bugs Left: " + bugsRemaining;
        }
    }

    void WinGame()
    {
        Debug.Log("You Win!");
        gameIsOver = true;
        //Time.timeScale = 0f; 
        LoadNextScene();
    }

    void LoseGame()
    {
        Debug.Log("You Lose!");
        gameIsOver = true;
        //Time.timeScale = 0f;
        RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
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