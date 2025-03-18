using NUnit.Framework;
using TMPro;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers ManagerSingleton;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI gameOverText;

    int score = 0;

    [SerializeField] float startTime = 5f;
    [SerializeField] float timeAdded = 5f;

    float leftTime = 0;
    bool timeOut = false;

    void Awake()
    {
        if (ManagerSingleton == null)
        {
            ManagerSingleton = this;
            DontDestroyOnLoad(ManagerSingleton);
        }
        else
        {
            Destroy(ManagerSingleton);
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
        if(timeOut) return;

        score += _score;
        scoreText.text = $"{score}";
    }

    public void AddTime(){
        leftTime += timeAdded;

    }

    void TimeCountDown()
    {
        if(timeOut) return;

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
}
