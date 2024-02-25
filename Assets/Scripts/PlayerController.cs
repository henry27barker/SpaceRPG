using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public int health;
    private Rigidbody2D rb2d;
    public Animator animator;
    private int direction;
    private SpriteRenderer spriteRenderer;
    public HealthBar healthBar;
    private Vector2 lookInputValue;
    public int lookRotation;
    public float rightStickDeadZone;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        MovementLogic();
        
        UpdateLookRotation();


        UpdateAnimations();

        
        UpdateHealthBar();
    }

    private void OnLook(InputValue value)
    {
        lookInputValue = value.Get<Vector2>();
    }

    private void MovementLogic()
    {
        //Movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        rb2d.velocity = new Vector2(moveHorizontal, moveVertical).normalized * new Vector2(speed, speed);
    }

    private void UpdateLookRotation()
    {
        Debug.Log(lookInputValue);
        if(Mathf.Abs(lookInputValue[0]) > rightStickDeadZone || Mathf.Abs(lookInputValue[1]) > rightStickDeadZone)
        {
            // Get input from the right stick
            float rightStickX = lookInputValue[0];
            float rightStickY = lookInputValue[1];

            // Calculate the angle in radians
            float angleRadians = Mathf.Atan2(rightStickY, rightStickX);

            // Convert radians to degrees
            float angleDegrees = angleRadians * Mathf.Rad2Deg;

            // Convert negative angles to positive equivalent
            if (angleDegrees < 0)
            {
                angleDegrees += 360f;
            }

            // Round to nearest integer
            lookRotation = Mathf.RoundToInt(angleDegrees);

            // Output the angle as an integer
            //Debug.Log("Right Stick Angle (Degrees): " + lookRotation);
        }
    }

    private void UpdateAnimations()
    {
        /*Directions:
         * 0 = Right
         * 1 = UpRight
         * 2 = DownLeft
         * 3 = Left
         */

        if (lookRotation > 315 || lookRotation <= 45)
        {
            direction = 0;
            animator.SetBool("FacingDown", true);
        }
        else if (lookRotation > 45 && lookRotation < 90)
        {
            direction = 1;
            animator.SetBool("FacingDown", false);
        }
        else if(lookRotation > 90 && lookRotation < 135)
        {
            direction = 2;
            animator.SetBool("FacingDown", false);
        }
        else
        {
            direction = 3;
            animator.SetBool("FacingDown", true);
        }

        if (direction < 2)
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
        /*
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
        }*/
    }
}
