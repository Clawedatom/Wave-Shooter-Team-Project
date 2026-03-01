using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    [SerializeField] GameObject DeathScreen;
    [SerializeField] TextMeshProUGUI DeathMessage;
    [SerializeField] TextMeshProUGUI ScoreMessage;
    [SerializeField] Animator TextAnimation;
    EnemySpawner enemySpawner;
    
    public void OnAwake()
    {
        enemySpawner = EnemySpawner.Instance;
    }
    public void OnStart()
    {
        DeathScreen.gameObject.SetActive(false);
        DeathMessage.gameObject.SetActive(false);
        ScoreMessage.gameObject.SetActive(false);
        
    }
    



    public void ShowDeadScreen()
    {
        Time.timeScale = 0.0f;
        DeathScreen.gameObject.SetActive(true);
        DeathMessage.gameObject.SetActive(true);
        ScoreMessage.gameObject.SetActive(true);
        TextAnimation.SetTrigger("death");
        ScoreMessage.text = "Waves Survived : " + (enemySpawner.GetCurrentWave() - 1);
          
    }

    public void RestartButton()
    {

    }
}
