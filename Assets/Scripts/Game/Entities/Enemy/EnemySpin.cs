using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpin : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10f;

    public void OnUpdate()
    {
        float newZRotation = transform.eulerAngles.z + rotSpeed;
        Quaternion targetRot = Quaternion.Euler(0, 0, newZRotation);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime);

    }
}
