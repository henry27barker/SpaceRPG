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
    public WeaponController weapon;
 
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
            weapon.UpdateRotation(lookRotation);
        }
    }

    private void UpdateAnimations()
    {
        /********************
         * Directions:
         * 0 = Right
         * 1 = UpRight
         * 2 = DownLeft
         * 3 = Left
         *********************/

        if (lookRotation > 270 || lookRotation <= 45)
        {
            direction = 0;
            animator.SetBool("FacingDown", true);
            healthBar.animator.SetBool("FacingDown", true);
            //weapon.transform.position = new Vector2(weapon.transform.position[0] + weapon.offsets[0], weapon.transform.position[1] - weapon.offsets[1]);
        }
        else if (lookRotation > 45 && lookRotation < 90)
        {
            direction = 1;
            animator.SetBool("FacingDown", false);
            healthBar.animator.SetBool("FacingDown", false);
            //weapon.transform.position = new Vector2(weapon.transform.position[0] + weapon.offsets[0], weapon.transform.position[1] - weapon.offsets[1]);
        }
        else if(lookRotation > 90 && lookRotation < 135)
        {
            direction = 2;
            animator.SetBool("FacingDown", false);
            healthBar.animator.SetBool("FacingDown", false);
            //weapon.transform.position = new Vector2(weapon.transform.position[0] + (- 1 * weapon.offsets[0]), weapon.transform.position[1]+ weapon.offsets[1]);
        }
        else
        {
            direction = 3;
            animator.SetBool("FacingDown", true);
            healthBar.animator.SetBool("FacingDown", true);
            //weapon.transform.position = new Vector2(weapon.transform.position[0] + (-1 * weapon.offsets[0]), weapon.transform.position[1] + weapon.offsets[1]);
        }

        if (direction < 2)
        {
            //Facing Right
            spriteRenderer.flipX = false;
            healthBar.spriteRenderer.flipX = false;
            weapon.spriteRenderer.flipY = false;
        }
        else
        {
            //Facing Left
            spriteRenderer.flipX = true;
            healthBar.spriteRenderer.flipX = true;
            weapon.spriteRenderer.flipY = true;
        }

        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            animator.SetFloat("Speed", 1);
            healthBar.animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            healthBar.animator.SetFloat("Speed", 0);
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
        
    }
}
