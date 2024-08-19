using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public float speed;

    public GameObject player;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private GameObject shootingPointControl;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shootingPointControl = transform.Find("Control").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            shootingPointControl.transform.localScale = new Vector3(-1, -1, 1);
        } else
        {
            transform.localScale = new Vector3(1, 1, 1);
            shootingPointControl.transform.localScale = new Vector3(1, 1, 1);
        }
        if(GetComponent<AIPath>().canMove == false && rb.velocity.magnitude < 0.5f) {
            GetComponent<AIPath>().canMove = true;
        }
    }
    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the enemy to the player
            //Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Apply a force towards the player, allowing knockback to still have an effect
            //rb.AddForce(direction * 10f);

            // Optionally clamp the velocity to prevent excessive speed
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        }
    }
}
