using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyExplosionLight : MonoBehaviour
{
    public Light2D light;
    public float intensity;
    public float decreaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        light.transform.rotation = new Quaternion(0,0,0,0);
        light.intensity = intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(light.intensity > 0){
            light.intensity -= Time.deltaTime * decreaseSpeed;
        }
    }
}
