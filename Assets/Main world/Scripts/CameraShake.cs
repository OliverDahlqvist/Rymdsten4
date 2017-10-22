using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform[] camTransform;
    
    public float shakeDuration = 0f;
    
    float decreaseFactor = 2f;

    Vector3[] originalPos;

    void Start()
    {
        originalPos = new Vector3[camTransform.Length];
        for (int i = 0; i < camTransform.Length; i++)
        {
            originalPos[i] = camTransform[i].localPosition;
        }
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            for (int i = 0; i < camTransform.Length; i++)
            {
                camTransform[i].localPosition = originalPos[i] + Random.insideUnitSphere * PlayerClass.shakeAmount;
            }
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            for (int i = 0; i < camTransform.Length; i++)
            {
                camTransform[i].localPosition = originalPos[i];
            }
        }
    }

    public void AddShake(float duration)
    {
        if (duration > 1)
        {
            shakeDuration = duration;
        }
        else if (shakeDuration < 1)
        {
            shakeDuration += duration;
        }
    }
}
