using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    [SerializeField] private string level1Name;
    [SerializeField] private string level2Name;
    [SerializeField] private string level3Name;

    [SerializeField] private float timer;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<LevelManager>();
                if (_instance == null)
                {
                    Debug.LogError("LevelManagre has not been assgined");
                }
            }
            return _instance;
        }
    }



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLevel(int level)
    {
        if (level == 1)
        {
            LoadLevel1();
        }
        else if(level == 2)
        {
            LoadLevel2();
        }
        else if (level == 3)
        {
            LoadLevel3();
        }
        else
        {
            Debug.LogError("Invalid Level");
        }
    }


    private void LoadLevel1()
    {
        SceneManager.LoadScene(level1Name);
    }

    private void LoadLevel2()
    {
        SceneManager.LoadScene(level2Name);
    }

    private void LoadLevel3()
    {
        SceneManager.LoadScene(level3Name);
    }
}
