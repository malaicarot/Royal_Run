using TMPro;
using UnityEngine;

public class ScoreManagers : MonoBehaviour
{
    public static ScoreManagers ScoreManagerSingleton;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    int score = 0;

    void Awake()
    {
        if (ScoreManagerSingleton == null)
        {
            ScoreManagerSingleton = this;
            DontDestroyOnLoad(ScoreManagerSingleton);
        }
        else
        {
            Destroy(ScoreManagerSingleton);
        }
    }

    public void AddScore(int _score)
    {
        score += _score;
        textMeshProUGUI.text = $"{score}";
    }
}
