using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;
    public float deathRadius;
    public float explosionRadius;
    private bool deathRadiusReached;
    public float deathTime;
    public GameObject player;
    public PlayerMovement playerController;
    private float distance;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;
 
    void Start()
    {
        deathRadiusReached = false;
        playerController = player.GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lastPosition = transform.position;
    }

    void Update()
    {

        if (health <= 0) {
            Destroy(gameObject);
        }

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= deathRadius) {
            deathRadiusReached = true;
            animator.SetBool("Explode", true);
        }

        if (deathRadiusReached && deathTime > 0) {
            deathTime -= Time.deltaTime;
        }
        else if (deathTime <= 0) {
            if (distance <= explosionRadius) {
                playerController.decreaseHealth(damage);
            }
            Destroy(gameObject);
        }
        else {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }


        //BELOW: Flips the Sprite Based on movement direction
        if (lastPosition[0] < transform.position[0])
        {
            spriteRenderer.flipX = false;
        } 
        else if (lastPosition[0] > transform.position[0])
        {
            spriteRenderer.flipX = true;
        }

        lastPosition = transform.position;

    }
        

    public void decreaseHealth(int damage){
        health -= damage;
    }
}
