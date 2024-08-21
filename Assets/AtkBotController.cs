using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBotController : MonoBehaviour
{
    public Animator animator;
    public EnemyMovement enemyMovement;
    public GameObject weapon;
    public GameObject player;
    public GameObject projectile;
    public Transform shootingPoint;
    public SpriteRenderer spriteRenderer;
    public GameObject parent;
    public AudioSource moveSound, footstep, deathSource, shootSource;

    public float deathAnimDuration;
    public Vector2 weaponOffsets;
    private float counter, counter2, counter3;

    private Vector3 lastPosition;
    private float lastCheckTime;
    private float speedThreshold = 0.5f;

    private int lastFrameHealth;
    private bool dead; 
    private float counter4, counter5;
    private bool reloading;
    public float fireRate, reloadTime;
    public int ammo;
    private int ammoCount;



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
        if (counter2 > 0.35)
        {
            moveSound.pitch = Random.Range(0.85f, 1f);
            moveSound.volume = Random.Range(0f, 0.2f);
            moveSound.PlayOneShot(moveSound.clip);
            counter2 = 0;
        }
        else
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
        float angleRadians = 0;

        float x, y;

        x = player.transform.position.x - transform.position.x;

        y = player.transform.position.y - transform.position.y;

        // Calculate the angle in radians
        angleRadians = Mathf.Atan2(y, x);
        // Convert radians to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        // Convert negative angles to positive equivalent
        if (angleDegrees < 0)
        {
            angleDegrees += 360f;
        }
        int lookRotation = Mathf.RoundToInt(angleDegrees);

        weapon.transform.rotation = Quaternion.Euler(0, 0, lookRotation);

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 12)
        {
            Shoot(lookRotation);
        }
    }

    private void CalculateLookDirection()
    {
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            weapon.transform.localScale = new Vector3(1, -1, 1);
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

    private void Shoot(int look)
    {
        if (!dead)
        {
            if (ammoCount < 1)
            {
                reloading = true;
                ammoCount = ammo;
            }

            if (reloading)
            {
                if (counter5 > reloadTime)
                {
                    reloading = false;
                    counter5 = 0;
                }
                else
                {
                    counter5 += Time.deltaTime;
                }
            }
            else
            {
                if (counter4 > fireRate)
                {
                    //Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, look + 15));
                    shootSource.volume = Random.Range(0.5f, 0.75f);
                    shootSource.pitch = Random.Range(0.5f, 0.65f);
                    shootSource.Play();
                    Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
                    ammoCount--;
                    //Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, look - 15));
                    counter4 = 0;
                }
                else
                {
                    counter4 += Time.deltaTime;
                }
            }
        }
    }
}
