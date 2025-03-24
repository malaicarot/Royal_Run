using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highestScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] AnimationEnd player;
    
    int score = 0;
    int highestScore = 0;

    void Start()
    {
        SoundManagers.soundManagersSingleTon.BackgroundMusic();
        PreferenceScore();
        GetScore();
        SetAnimation();
    }

    void PreferenceScore()
    {
        highestScore = GameManagers.ManagerSingleton.HighestScore();
        score = GameManagers.ManagerSingleton.CurrentScore();
    }

    void GetScore()
    {
        highestScoreText.text = highestScore.ToString();
        currentScoreText.text = score.ToString();
    }

    void SetAnimation()
    {
        if (highestScore < score)
        {
            player.HappyAnimation();
        }
    }

    public void MainMenu()
    {
        GameManagers.ManagerSingleton.MainMenu();
    }
    public void ReloadMainScene(){
        GameManagers.ManagerSingleton.ReloadMainScene();
    }
}
