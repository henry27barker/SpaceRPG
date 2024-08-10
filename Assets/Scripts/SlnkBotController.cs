using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SlnkBotController : MonoBehaviour
{
    //SETTINGS
    public int health; 
    public int damage;
    public float deathRadius;
    public float explosionRadius;
    public float deathTime;

    //HELPERS
    private bool deathRadiusReached;
    private Vector2 lastPosition;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private float distance;

    //COMPONENTS
    private Light2D deathLight;
    public Animator animator;
    public ParticleSystem enemyExplosionParticle;
    public GameObject player;
    public PlayerMovement playerController;
    public EnemyMovement enemyMovement;
    public SpriteRenderer spriteRenderer;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {

        deathRadiusReached = false;
        deathLight = GetComponent<Light2D>();

        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerMovement>();
        enemyMovement = GetComponent<EnemyMovement>();

        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().damage = damage;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().radius = explosionRadius;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().playerDamage = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= deathRadius)
        {
            deathLight.color = Color.red;
            deathRadiusReached = true;
            animator.SetBool("Explode", true);
        }

        if (enemyMovement.health <= 0)
        {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0, 0, 0, 0));
            Projectiles();
            Destroy(gameObject);
        }

        if (deathRadiusReached && deathTime > 0)
        {
            deathTime -= Time.deltaTime;
        }
        else if (deathTime <= 0)
        {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0, 0, 0, 0));
            Projectiles();
            Destroy(gameObject);
        }
        
        if (enemyMovement.health <= 0) {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0,0,0,0));
            Projectiles();
            Destroy(gameObject);
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

    private void Projectiles()
    {
        var explosionTemp = Instantiate(projectile, new Vector3(transform.position.x + 1, transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x + 0.71f, transform.position.y + 0.71f, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 45);
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 90);
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x - 0.71f, transform.position.y + 0.71f, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 135);
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x - 1, transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 180);
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x - 0.71f, transform.position.y - 0.71f, 0), new Quaternion(0,0,0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 225);
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y - 1, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 270);
        explosionTemp = Instantiate(projectile, new Vector3(transform.position.x + 0.71f, transform.position.y - 0.71f, 0), new Quaternion(0, 0, 0, 0));
        explosionTemp.transform.rotation = Quaternion.Euler(0, 0, 315);
    }
}
