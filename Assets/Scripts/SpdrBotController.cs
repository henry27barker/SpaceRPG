using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
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
    public float explosionRadius;

    //HELPERS
    private Vector3 lastPosition;
    public float whiteFlashTime;
    private float whiteFlashCounter;
    private bool isAdjustingPosition;
    public Transform newTarget;

    //COMPONENTS
    public Animator animator;
    public GameObject player;
    public GameObject head;
    public PlayerMovement playerController;
    public EnemyMovement enemyMovement;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem enemyExplosionParticle;
    public AIPath aipath;
    public AIDestinationSetter enemyDestinationSetter;
    public GameObject rayCastPoint;

    //Debugging
    public GameObject circle;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerMovement>();
        enemyMovement = GetComponent<EnemyMovement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);

        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        //Weapon Flash Material Updates to match parent
        head.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", enemyMovement.spriteRenderer.material.GetFloat("_FlashAmount"));
        head.GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", enemyMovement.spriteRenderer.material.GetColor("_FlashColor"));

        //Movement Logic
        /*
        Vector3 direction = player.transform.position - rayCastPoint.transform.position;
        Vector3 normalizedDirection = direction.normalized;
        RaycastHit2D hit = Physics2D.Raycast(rayCastPoint.transform.position, normalizedDirection);

        Debug.DrawRay(rayCastPoint.transform.position, normalizedDirection);

        if (hit.collider != null && hit.collider.tag != "Player" && aipath.enabled == false)
        {
            // If the player is not in sight, start adjusting position
            //transform.position += new Vector3(0,1,0) * 1 * Time.deltaTime;

            Vector3 directionToObstable = hit.transform.position - rayCastPoint.transform.position;

            Vector3 normalizedDirectionToObstable = directionToObstable.normalized;

            Vector3 orthogonalVector = new Vector3(normalizedDirectionToObstable.y, normalizedDirectionToObstable.x * -1, 0);

            Debug.DrawRay(transform.position, orthogonalVector);

            newTarget.position = transform.position + orthogonalVector * 2;

            circle.transform.position = newTarget.position;

            enemyDestinationSetter.target = newTarget;

            aipath.enabled = true;
        }
        else if (hit.collider != null && hit.collider.tag == "Player")
        {
            // If the player is in sight, stop adjusting position
            aipath.enabled = false;
        }
        */

        //Death Logic
        if (enemyMovement.health <= 0)
        {
            var explosionTemp = Instantiate(enemyExplosionParticle, transform.position, new Quaternion(0, 0, 0, 0));
            explosionTemp.GetComponent<EnemyExplosionDamage>().damage = deathDamage;
            explosionTemp.GetComponent<EnemyExplosionDamage>().radius = explosionRadius;
            explosionTemp.GetComponent<EnemyExplosionDamage>().playerDamage = true;
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
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        lastPosition = transform.position;

    }
}
