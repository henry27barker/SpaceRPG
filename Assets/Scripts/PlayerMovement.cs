using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public int health;
    private Rigidbody2D rb2d;
    public Animator animator;
    private int direction;
    private SpriteRenderer spriteRenderer;
    public HealthBar healthBar;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        //Movement
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");
        float moveVertical = Input.GetAxisRaw ("Vertical");

        rb2d.velocity = new Vector2(moveHorizontal, moveVertical).normalized * new Vector2(speed, speed);

        Debug.Log(direction);

        //Animation
        UpdateAnimations();

        //HealthBar Updates
        UpdateHealthBar();
    }

    private void UpdateAnimations()
    {
        if (Mathf.Abs(rb2d.velocity.x) >= Mathf.Abs(rb2d.velocity.y) && rb2d.velocity.x != 0)
        {
            if (rb2d.velocity.x > 0)
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }
        }
        else if (rb2d.velocity.y > 0)
        {
            direction = 1;
        }
        else if (rb2d.velocity.y < 0)
        {
            direction = 2;
        }

        if (direction == 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        animator.SetInteger("Direction", direction);
    }

    private void UpdateHealthBar()
    {
        //Color Update
        healthBar.currentHealth = (float)health;
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            health -= 10;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            health += 10;
        }
        //Direction Update
        Vector3 playerPosition = new Vector3();
        Vector3 vectorToAdd;
        playerPosition = transform.position;
        switch (direction)
        {
            case -1:
                vectorToAdd = new Vector3(-1 * healthBar.sideOffset, 0f, 0f);
                healthBar.transform.position = playerPosition + vectorToAdd;
                break;
            case 0:
                vectorToAdd = new Vector3(healthBar.sideOffset, 0f, 0f);
                healthBar.transform.position = playerPosition + vectorToAdd;
                break;
            case 1:
                vectorToAdd = new Vector3(0f, healthBar.backOffset, 0f);
                healthBar.transform.position = playerPosition + vectorToAdd;
                break;
            default:
                vectorToAdd = new Vector3(0f, 0f, 0f);
                healthBar.transform.position = playerPosition + vectorToAdd;
                break;
        }
    }
}
