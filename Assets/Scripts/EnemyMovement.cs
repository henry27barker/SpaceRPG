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
    public Rigidbody2D rb;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;
    public GameObject player;
    public float seeRadius = 10f;
    public GameObject moneyItem;
    public int moneyAmount;
    public bool hasRB;
    public bool hasSeen;
    public bool dead;
 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);
            player = GameObject.FindWithTag("Player"); 
        if(!hasRB){
            player = GameObject.FindWithTag("Player");
            if (transform.parent != null)
            {
                aiPath = transform.parent.gameObject.GetComponent<AIPath>();
                aiDestinationSetter = transform.parent.gameObject.GetComponent<AIDestinationSetter>();
                if (player != null)
                {
                    aiDestinationSetter.target = player.transform;
                }
                aiPath.canMove = false;
            }
        } else
        {
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                aiDestinationSetter.target = player.transform;
            }
            aiPath.canMove = false;
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= seeRadius && !hasSeen && !dead)
            {
                if (aiPath != null)
                    aiPath.canMove = true;
                hasSeen = true;
            }
            else
            {
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
        if (hasRB && !dead)
        {
            if (GetComponent<AIPath>().canMove == false && rb.velocity.magnitude < 0.5f)
            {
                if(hasSeen)
                    GetComponent<AIPath>().canMove = true;
            }
        }
    }

    public void decreaseHealth(int damage){
        whiteFlashCounter = whiteFlashTime;
        health -= damage;
        if(health <= 0){
            if (moneyAmount > 0)
            {
                GameObject tempMoney = Instantiate(moneyItem, gameObject.transform.position, Quaternion.identity);
                tempMoney.GetComponent<MoneyPickup>().amount = moneyAmount;
            }
        }
    }
}
