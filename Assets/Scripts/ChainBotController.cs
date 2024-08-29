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
    public AudioSource moveSound, footstep, deathSource;
    public bool hasRB;

    public float deathAnimDuration;
    public Vector2 weaponOffsets;
    private float counter, counter2, counter3;

    private Vector3 lastPosition;
    private float lastCheckTime;
    private float speedThreshold = 0.5f;

    private int lastFrameHealth;
    private bool dead;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();    

        lastPosition = transform.position;
        lastCheckTime = Time.time;

        lastFrameHealth = enemyMovement.health;
        counter3 = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter2 > 0.35)
        {
            moveSound.pitch = Random.Range(0.85f, 1f);
            moveSound.volume = Random.Range(0f, 0.2f);
            moveSound.PlayOneShot(moveSound.clip);
            counter2 = 0;
        } else
        {
            counter2 += Time.deltaTime;
        }
        if (counter3 > 0.35)
        {
            footstep.pitch = Random.Range(0.85f, 1f);
            footstep.volume = Random.Range(0.85f, 1f);
            footstep.PlayOneShot(footstep.clip);
            counter3 = 0;
        }
        else
        {
            counter3 += Time.deltaTime;
        }

        //Weapon Flash Material Updates to match parent
        weapon.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", enemyMovement.spriteRenderer.material.GetFloat("_FlashAmount"));
        weapon.GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", enemyMovement.spriteRenderer.material.GetColor("_FlashColor"));


        //Animation Logic
        animator.SetFloat("Speed", CalculateSpeed());
        CalculateLookDirection();


        //Death Logic
        if (enemyMovement.health <= 0)
        {
            dead = true;
            if (hasRB)
            {
                GetComponent<AIPath>().canMove = false;
                GetComponent<PolygonCollider2D>().enabled = false;
                enemyMovement.dead = true;

            }
            else
            {
                parent.GetComponent<AIPath>().canMove = false;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            animator.SetBool("Death", true);
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
