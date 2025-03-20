using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagers : MonoBehaviour
{
    [SerializeField] int indexMainScene = 1;


    public void StartGame()
    {
        SceneManager.LoadScene(indexMainScene);
    }
    public void Setting()
    {

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
