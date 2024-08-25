using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private float amplitude;
    private float frequency;
    private float timer;

    public float duration;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(amplitude > 0)
        {
            if(duration > 0)
            {
                amplitude -= Time.deltaTime * timer;
            }
            
        } else
        {
            amplitude = 0;
        }
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
    }

    public void Shake(float amp, float time)
    {
        amplitude = amp;
        timer = amplitude / time;
    }
}
