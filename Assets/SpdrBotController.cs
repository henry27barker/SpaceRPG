using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class SpdrBotController : MonoBehaviour
{
    //SETTINGS
    public int health;
    public int damage;

    //HELPERS
    private Vector2 lastPosition;
    public float whiteFlashTime;
    private float whiteFlashCounter;

    //COMPONENTS
    public Animator animator;
    public GameObject player;
    public GameObject head;
    public PlayerMovement playerController;
    public EnemyMovement enemyMovement;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerMovement>();
        enemyMovement = GetComponent<EnemyMovement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_FlashAmount", 0);

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyMovement.health <= 0)
        {
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
}
