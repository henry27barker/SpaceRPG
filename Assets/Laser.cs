using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEditor.Rendering;
using UnityEngine.AI;

public class Laser : MonoBehaviour
{
    private bool up, down, flag, startDestroy;
    private float count = 0, count2 = 0, count3 = 0;
    private float activeHeight;
    private SpriteRenderer spriteRenderer;
    public Sprite activeSprite, inactiveSprite;
    public Material emissiveMaterial;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        up = true; down = false;
        activeHeight = 0.25f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        emissiveMaterial = spriteRenderer.GetComponent<SpriteRenderer>().material;

        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {/*
        if (!flag)
        {
            if (count > timer)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 6, transform.localScale.z);
                flag = true;
            }
            else
            {
                count += Time.deltaTime;
            }
        }*/
        if (up && transform.localScale.y > activeHeight + 0.2f)
        {
            up = false;
            down = true;
        } else if (up)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.01f, transform.localScale.z);
        }

        if(down && transform.localScale.y < activeHeight)
        {
            up = true;
            down = false;
        }
        else if(down)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.01f, transform.localScale.z);
        }
        if (!flag)
        {
            if (count > timer)
            {
                emissiveMaterial.SetColor("_Color", UnityEngine.Color.red * 3);
                spriteRenderer.color = UnityEngine.Color.white;
                activeHeight = 2;
                spriteRenderer.sprite = activeSprite;
                flag = true;
            }
            else
            {
                count += Time.deltaTime;
            }
        } else if (!startDestroy)
        {
            if (count2 > timer)
            {
                emissiveMaterial.SetColor("_Color", new UnityEngine.Color(43, 0, 0,100));
                activeHeight = 0.25f;
                startDestroy = true;
            }
            else
            {
                count2 += Time.deltaTime;
            }
        } else
        {
            if (count3 > timer)
            {
                Destroy(gameObject);
            } else
            {
                count3 += Time.deltaTime;
            }
        }



    }
}
