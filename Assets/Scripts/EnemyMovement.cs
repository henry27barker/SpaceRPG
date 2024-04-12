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
    public ParticleSystem enemyExplosionParticle;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private Light2D deathLight;
 
    void Start()
    {
        deathRadiusReached = false;
        playerController = player.GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        deathLight = GetComponent<Light2D>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);

        lastPosition = transform.position;

        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().damage = damage;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().radius = explosionRadius;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().playerDamage = true;
    }

    void Update()
    {
        if(whiteFlashCounter > 0){
            spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            whiteFlashCounter -= Time.deltaTime;
            Debug.Log(whiteFlashCounter);
        }
        else{
            spriteRenderer.material.SetFloat("_FlashAmount", 0);
        }
        if (health <= 0) {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0,0,0,0));
            Destroy(gameObject);
        }

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= deathRadius) {
            deathLight.enabled = true;
            deathRadiusReached = true;
            animator.SetBool("Explode", true);
        }

        if (deathRadiusReached && deathTime > 0) {
            deathTime -= Time.deltaTime;
        }
        else if (deathTime <= 0) {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0,0,0,0));
            /*if (distance <= explosionRadius) {
                playerController.decreaseHealth(damage);
            }*/
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
        whiteFlashCounter = whiteFlashTime;
        health -= damage;
    }
}
