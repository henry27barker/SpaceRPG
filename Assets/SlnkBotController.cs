using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SlnkBotController : MonoBehaviour
{

    private bool deathRadiusReached;
    private Light2D deathLight;
    public int damage;
    public float deathRadius;
    public float explosionRadius;
    public float deathTime;
    private float distance;
    public Animator animator;
    public ParticleSystem enemyExplosionParticle;
    public GameObject player;
    public PlayerMovement playerController;
    public EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {

        deathRadiusReached = false;
        deathLight = GetComponent<Light2D>();

        playerController = player.GetComponent<PlayerMovement>();
        enemyMovement = GetComponent<EnemyMovement>();

        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().damage = damage;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().radius = explosionRadius;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().playerDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= deathRadius)
        {
            deathLight.enabled = true;
            deathRadiusReached = true;
            animator.SetBool("Explode", true);
        }

        if (enemyMovement.health <= 0)
        {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }

        if (deathRadiusReached && deathTime > 0)
        {
            deathTime -= Time.deltaTime;
        }
        else if (deathTime <= 0)
        {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }
        
        if (enemyMovement.health <= 0) {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0,0,0,0));
            Destroy(gameObject);
        }
    }
}
