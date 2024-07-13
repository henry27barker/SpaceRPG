using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBotController : MonoBehaviour
{
    public Animator animator;
    public EnemyMovement enemyMovement;
    public GameObject weapon;
    public GameObject player;
    public SpriteRenderer spriteRenderer;
    public GameObject parent;

    public float deathAnimDuration;
    public Vector2 weaponOffsets;
    private float counter;

    private Vector3 lastPosition;
    private float lastCheckTime;
    private float speedThreshold = 0.5f;

    private int lastFrameHealth;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();    

        lastPosition = transform.position;
        lastCheckTime = Time.time;

        lastFrameHealth = enemyMovement.health;

    }

    // Update is called once per frame
    void Update()
    {
        //Weapon Flash Material Updates to match parent
        weapon.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", enemyMovement.spriteRenderer.material.GetFloat("_FlashAmount"));
        weapon.GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", enemyMovement.spriteRenderer.material.GetColor("_FlashColor"));


        //Animation Logic
        animator.SetFloat("Speed", CalculateSpeed());
        CalculateLookDirection();


        //Death Logic
        if (enemyMovement.health <= 0)
        {
            parent.GetComponent<AIPath>().canMove = false;
            animator.SetBool("Death", true);
            GetComponent<BoxCollider2D>().enabled = false;
            weapon.SetActive(false);
            if (counter < deathAnimDuration)
            {
                counter += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void CalculateLookDirection(){
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            weapon.transform.localScale = new Vector3(-1, 1, 1);
            weapon.transform.position = new Vector2(transform.position[0] - weaponOffsets[0], transform.position[1] + weaponOffsets[1]);
        }
        else
        {
            spriteRenderer.flipX = false;
            weapon.transform.localScale = new Vector3(1, 1, 1);
            weapon.transform.position = new Vector2(transform.position[0] + weaponOffsets[0], transform.position[1] + weaponOffsets[1]);
        }
    }

    private float CalculateSpeed()
    {
        // Calculate time elapsed since last check
        float deltaTime = Time.time - lastCheckTime;

        // Calculate distance traveled since last check
        float distance = Vector3.Distance(lastPosition, transform.position);

        // Calculate speed
        float speed = distance / deltaTime;

        // Update last position and last check time
        lastPosition = transform.position;
        lastCheckTime = Time.time;

        return speed;
    }
}
