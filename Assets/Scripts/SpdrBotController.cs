using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class SpdrBotController : MonoBehaviour
{
    //SETTINGS
    public int health;
    public int damage;
    public int deathDamage;
    public int explosionRadius;

    //HELPERS
    private Vector3 lastPosition;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private bool isAdjustingPosition;

    //COMPONENTS
    public Animator animator;
    public GameObject player;
    public GameObject head;
    public PlayerMovement playerController;
    public EnemyMovement enemyMovement;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem enemyExplosionParticle;
    public AIPath aipath;
    public GameObject rayCastPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerMovement>();
        enemyMovement = GetComponent<EnemyMovement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);

        lastPosition = transform.position;

        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().damage = deathDamage;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().radius = explosionRadius;
        enemyExplosionParticle.GetComponent<EnemyExplosionDamage>().playerDamage = true;
    }

    // Update is called once per frame
    void Update()
    {

        //Weapon Flash Material Updates to match parent
        head.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", enemyMovement.spriteRenderer.material.GetFloat("_FlashAmount"));
        head.GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", enemyMovement.spriteRenderer.material.GetColor("_FlashColor"));

        //Movement Logic
        
        Vector3 direction = player.transform.position - rayCastPoint.transform.position;

        Vector3 normalizedDirection = direction.normalized; 

        Vector3 orthogonalVector = Vector3.Cross(direction, Vector3.up);


        RaycastHit2D hit = Physics2D.Raycast(rayCastPoint.transform.position, normalizedDirection);
        Debug.DrawRay(rayCastPoint.transform.position, orthogonalVector);

        if (hit.collider != null && hit.collider.tag != "Player")
        {
            // If the player is not in sight, start adjusting position
            //transform.position += new Vector3(0,1,0) * 1 * Time.deltaTime;
            aipath.enabled = true;
        }
        else if (hit.collider != null && hit.collider.tag == "Player")
        {
            // If the player is in sight, stop adjusting position
            aipath.enabled = false;
        }
        

        //Death Logic
        if (enemyMovement.health <= 0)
        {
            Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0, 0, 0, 0));
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

        if(lastPosition != transform.position)
        {
            Debug.Log("Speed 1");
            animator.SetFloat("Speed", 1);
        }
        else
        {
            Debug.Log("Speed 0");
            animator.SetFloat("Speed", 0);
        }

        lastPosition = transform.position;

    }
}
