using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    public static GameManagers ManagerSingleton;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI gameOverText;

    [SerializeField] GameObject PauseMenus;

    int score = 0;

    [SerializeField] float startTime = 5f;
    [SerializeField] float timeAdded = 5f;

    float leftTime = 0;
    bool timeOut = false;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            Destroy(gameObject);
            return;
        }
        if (ManagerSingleton == null)
        {
            ManagerSingleton = this;
            DontDestroyOnLoad(ManagerSingleton);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        leftTime = startTime;
    }

    void Update()
    {
        TimeCountDown();
    }

    public void AddScore(int _score)
    {
        if (timeOut) return;

        score += _score;
        scoreText.text = $"{score}";
    }

    public void AddTime()
    {
        leftTime += timeAdded;

    }

    void TimeCountDown()
    {
        if (timeOut) return;

        leftTime -= Time.deltaTime;
        timeText.text = leftTime.ToString("F1");
        if (leftTime <= 0)
        {
            ActiveGameOverText();
        }
    }

    void ActiveGameOverText()
    {
        timeOut = true;
        playerMovement.enabled = false;
        Time.timeScale = .1f;
        gameOverText.gameObject.SetActive(true);
    }

    public void ActivePause()
    {
        Time.timeScale = 0f;
        PauseMenus.SetActive(true);
    }
    public void Countinue()
    {
        Time.timeScale = 1f;
        PauseMenus.SetActive(false);
    }

    public void MainMenu()
    {
        Destroy(this);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
