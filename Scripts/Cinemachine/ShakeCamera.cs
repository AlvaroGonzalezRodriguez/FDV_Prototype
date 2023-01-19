using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance;
    private CinemachineVirtualCamera mainCamera;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if(shakeTime <= 0.0f)
            {
                perlin.m_AmplitudeGain = 0.0f;
            }
        }
    }

    public void ShakeCam(float amplitude, float time)
    {
        perlin.m_AmplitudeGain = amplitude;
        shakeTime = time;
    }
}