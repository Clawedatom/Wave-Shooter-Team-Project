    using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    LevelManager levelManager;

    [SerializeField] private int level;

    private void Awake()
    {
        levelManager = LevelManager.Instance;
    }

    public void PlayGame()
    {
        levelManager.LoadLevel(1);
    }

    public void Options()
    {
        Debug.Log("Options not implemented yet");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}