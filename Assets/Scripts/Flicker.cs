using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class Flicker : MonoBehaviour
{

    public bool flickerOn;
    public float flickerIntensity;
    public Light2D light2D;
    public SpriteRenderer spriteRenderer;
    public Material emissiveMaterial;
    public Color color;

    private float counter;
    private float onLength;
    private float offLength;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        emissiveMaterial = spriteRenderer.GetComponent<SpriteRenderer>().material;
        counter = 0;
        onLength = Random.Range(0.5f, flickerIntensity);
        offLength = 0.1f;

        if (flickerIntensity < 0.5)
        {
            flickerIntensity = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(flickerOn)
        {
            if (counter > onLength)
            {
                //Turn on
                light2D.intensity = 1;
                emissiveMaterial.SetColor("_Color", color * 3);
                counter = 0;
                onLength = Random.Range(0f, flickerIntensity);
            }
            else if(counter > onLength - offLength)
            {
                //Turn off
                light2D.intensity = 0;
                emissiveMaterial.SetColor("_Color", Color.black);
                counter += Time.deltaTime;
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
        
    }
}
