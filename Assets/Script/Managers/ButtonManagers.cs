using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManagers : MonoBehaviour
{
    [SerializeField] int indexMainScene = 1;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Slider backgroundMusicSlider;
    [SerializeField] Slider SFXSlider;




    void Start()
    {
        GetVolumeValue(backgroundMusicSlider);
        GetVolumeValue(SFXSlider);
        backgroundMusicSlider.onValueChanged.AddListener(SoundManagers.soundManagersSingleTon.SettingSoundVolume);
        SFXSlider.onValueChanged.AddListener(SoundManagers.soundManagersSingleTon.SettingSFXVolume);
        SoundManagers.soundManagersSingleTon.BackgroundMusic();

    }

    void GetVolumeValue(Slider slider)
    {
        if (slider != null)
        {
            slider.value = SoundManagers.soundManagersSingleTon.soundVolume;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(indexMainScene);
    }
    public void Setting()
    {
        settingsPanel.SetActive(true);
    }
    public void MainMenu()
    {
        settingsPanel.SetActive(false);
    }

    public void DeleteScore()
    {
        GameManagers.ManagerSingleton.DeleteScore();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

}
