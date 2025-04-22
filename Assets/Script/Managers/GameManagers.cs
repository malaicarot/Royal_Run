using System.Collections;
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
    [SerializeField] string startScene = "StartScene";
    [SerializeField] string endScene = "EndScene";
    [SerializeField] string mainScene = "MainScene";

    int score = 0;
    int highestScore = 0;

    [SerializeField] float startTime = 5f;
    [SerializeField] float timeAdded = 5f;

    float leftTime = 0;
    bool timeOut = false;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == startScene)
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
        GetHighestScore();
        SoundManagers.soundManagersSingleTon.BackgroundMusic();
    }

    void Update()
    {
        TimeCountDown();
    }

    void GetHighestScore()
    {
        int savedScore = PlayerPrefs.GetInt("PlayerScore", 0);
        highestScore = savedScore;
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
        SaveScore();
        StartCoroutine(waitForLoadScene());


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

    public void ReloadMainScene()
    {
        Destroy(this);
        SceneManager.LoadScene(mainScene);
        GetHighestScore();
    }

    public void DeleteScore()
    {
        PlayerPrefs.DeleteKey("PlayerScore");
    }

    IEnumerator waitForLoadScene()
    {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(endScene);
    }

    void SaveScore()
    {
        int higherScore = Mathf.Max(score, highestScore);
        PlayerPrefs.SetInt("PlayerScore", higherScore);
        PlayerPrefs.Save();
    }

    public int HighestScore()
    {
        return highestScore;

    }
    public int CurrentScore()
    {
        return score;
    }
}
