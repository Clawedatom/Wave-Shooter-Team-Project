using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private static CameraHandler _instance;

    public static CameraHandler Instance
    {
        get
        {
            if ( _instance == null)
            {
                _instance = FindAnyObjectByType<CameraHandler>();
                if ( _instance == null )
                {
                    Debug.LogError("CameraHandler has not been assigned");
                }
            }
            return _instance;
        }
    }

    public float followSpeed = 2f;
    public Transform target;
    
    public void OnAwake()
    {
        target = PlayerManager.Instance.transform;
    }

    public void OnStart()
    {
        
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -15f);
        transform.position = newPos;
    }
}
