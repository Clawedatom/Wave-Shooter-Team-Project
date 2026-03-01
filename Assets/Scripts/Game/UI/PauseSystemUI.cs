using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseSystemUI : MonoBehaviour
{
    [SerializeField] GameObject pauseUIGO = null;
    public bool paused = false;
    public void OnAwake()
    {
        

    }

    public void OnStart()
    {
        pauseUIGO.SetActive(false);
    }
    

 

    public void QuitGame()//send the game back to the title screen when quit game is selected
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenPauseUI() //stops all movement
    {
        

        pauseUIGO.SetActive(true);
    }

    public void ClosePauseUI() //resumes movement
    {
        

        pauseUIGO.SetActive(false);
    }
}
