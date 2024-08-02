using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public int health;
    public SpriteRenderer spriteRenderer;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;
    private GameObject player;
    public float seeRadius = 10f;
    public GameObject moneyItem;
 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);
        if(transform.parent != null){
            aiPath = transform.parent.gameObject.GetComponent<AIPath>();
            aiDestinationSetter = transform.parent.gameObject.GetComponent<AIDestinationSetter>();
            player = GameObject.FindWithTag("Player");
            aiPath.canMove = false;
        }
    }

    void Update()
    {
        if(transform.parent != null){
            aiDestinationSetter.target = player.transform;
            if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= seeRadius){
                aiPath.canMove = true;
            }
            else{
                //aiPath.canMove = false;
            }
        }

        if(whiteFlashCounter > 0){
            spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            whiteFlashCounter -= Time.deltaTime;
        }
        else{
            spriteRenderer.material.SetFloat("_FlashAmount", 0);
        }
    }

    void OnDestroy(){
        Instantiate(moneyItem, gameObject.transform.position, Quaternion.identity);
    }
        

    public void decreaseHealth(int damage){
        whiteFlashCounter = whiteFlashTime;
        health -= damage;
    }
}
