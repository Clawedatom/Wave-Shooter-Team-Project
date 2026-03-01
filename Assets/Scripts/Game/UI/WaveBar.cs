using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour
{
    [SerializeField] Slider Waveslider;
    EnemySpawner EnemySpawner;
    private static WaveBar _instance;    
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner = EnemySpawner.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        updateSlider();
    }

    public static WaveBar Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<WaveBar>();
                if (_instance == null)
                {
                    Debug.LogError("wavebar has not been assigned");
                }
            }
            return _instance;
        }
    }
    public void SetWave()
    {
        Waveslider.maxValue = EnemySpawner.GetTotalEnemies();
    }

    void updateSlider() 
    {
        Waveslider.value = EnemySpawner.EnemyDestroyed();
    }
}
