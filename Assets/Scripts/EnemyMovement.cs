using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{
    public int health;
    public SpriteRenderer spriteRenderer;
    public float whiteFlashTime;
    private float whiteFlashCounter;
 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);
    }

    void Update()
    {
        if(whiteFlashCounter > 0){
            spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            whiteFlashCounter -= Time.deltaTime;
        }
        else{
            spriteRenderer.material.SetFloat("_FlashAmount", 0);
        }
    }
        

    public void decreaseHealth(int damage){
        whiteFlashCounter = whiteFlashTime;
        health -= damage;
    }
}
