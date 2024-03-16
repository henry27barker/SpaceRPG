using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flicker : MonoBehaviour
{

    public bool flickerOn;
    public Light2D light2D;
    public SpriteRenderer spriteRenderer;

    private float counter;
    private float turnOn;
    private float turnOff;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        counter = 0;
        turnOn = Random.Range(0.1f, 0.5f);
        turnOff = Random.Range(0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(flickerOn)
        {
            //Light ON
            if (counter < turnOn)
            {
                light2D.intensity = 1;
                spriteRenderer.material.EnableKeyword("_Emission");
                spriteRenderer.material.SetColor("_Color", new Color(191f / 255f, 191f / 255f, 0f));

                counter += Time.deltaTime;
            }
            //Light OFF
            else if (counter < turnOff)
            {
                float rand = 0;//Random.Range(0.5f, 1f);
                light2D.intensity = rand;
                spriteRenderer.material.DisableKeyword("_Emission");
                spriteRenderer.material.SetColor("_Color", Color.black);
                counter += Time.deltaTime;
            }
            else
            {
                counter = 0;
                turnOn = Random.Range(0.1f, 0.5f);
                turnOff = Random.Range(0.1f, 0.5f);
            }
        }
        
    }
}
