using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Ability Specifications
    public float speed;
    public int health;

    //Settings
    public float rightStickDeadZone;
    public float stompDuration;

    //Helper Fields
    public int lookRotation;
    private int direction;
    private float stompCounter = 0f;
    private bool isStomping = false;

    //Inputs
    private Vector2 lookInputValue;
    private Vector2 moveInputValue;
    //private InputAction move;
    //private InputAction fire;

    //Components
    public Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    public PlayerInput playerControls;
    public GameObject mouseCursor;

    //Scripts
    public HealthBar healthBar;
    public WeaponController weapon;
    public HandsController hands;
    public PlayerShoot playerShoot;
    public PlayerStomp playerStomp;

    //ItemPickup
    public Interactable focus;
    public LayerMask crateLayerMask;
    public GameObject inventoryUI;
    
    private void Awake()
    {   
        playerControls = new PlayerInput();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Move.performed += ctx => moveInputValue = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInputValue = Vector2.zero;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.Move.performed -= ctx => moveInputValue = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled -= ctx => moveInputValue = Vector2.zero;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 100;
    }
 
    void Update()
    {
        if(inventoryUI.activeSelf == true){
            Time.timeScale = 0;
            return;
        }
        else{
            Time.timeScale = 1;
        }

        MovementLogic();
        
        UpdateLookRotation();


        UpdateAnimations();

        
        UpdateHealthBar();

        StompTimer();

        InputInteractable();
    }

    private void OnOpenInventory(){
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void OnLook(InputValue value)
    {
        lookInputValue = value.Get<Vector2>();
    }

    private void OnFire()
    {
        playerShoot.Shoot();
    }

    private void OnStomp()
    {
        animator.SetTrigger("Stomp");
        animator.SetBool("isStomping", true);

        healthBar.animator.SetTrigger("Stomp");
        hands.animator.SetTrigger("Stomp");

        healthBar.animator.SetBool("isStomping", true);
        hands.animator.SetBool("isStomping", true);

        isStomping = true;
    }

    private void StompTimer()
    {
        
        if(isStomping)
        {
            if (stompCounter > stompDuration)
            {
                animator.SetBool("isStomping", false);
                healthBar.animator.SetBool("isStomping", false);
                hands.animator.SetBool("isStomping", false);

                isStomping = false;
                stompCounter = 0;
            }
            else
            {
                stompCounter += Time.deltaTime;
            }
        } else
        {
            stompCounter = 0;
        }
    }
    
    private void MovementLogic()
    {
        rb2d.velocity = new Vector2(moveInputValue[0], moveInputValue[1]).normalized * new Vector2(speed, speed);
    }


    private void UpdateLookRotation()
    {
        float angleRadians = 0;
        // GAMEPAD
        if (Gamepad.current != null && (Gamepad.current.leftStick.ReadValue() != Vector2.zero || Gamepad.current.rightStick.ReadValue() != Vector2.zero))
        {
            // Remove Crosshair
            mouseCursor.SetActive(false);
            // DEADZONE
            if (Mathf.Abs(lookInputValue[0]) > rightStickDeadZone || Mathf.Abs(lookInputValue[1]) > rightStickDeadZone)
            {
                // Calculate the angle in radians
                angleRadians = Mathf.Atan2(lookInputValue[1], lookInputValue[0]);
                // Convert radians to degrees
                float angleDegrees = angleRadians * Mathf.Rad2Deg;

                // Convert negative angles to positive equivalent
                if (angleDegrees < 0)
                {
                    angleDegrees += 360f;
                }
                lookRotation = Mathf.RoundToInt(angleDegrees);
                weapon.UpdateRotation(lookRotation);
            }
        }
        // MOUSE
        /*
        else
        {
            //Add Crosshair
            mouseCursor.SetActive(true);
            // Find mouse position
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            mouseCursor.transform.position = mouseWorldPosition;

            // Find difference in player and mouse position
            float deltaX = mouseWorldPosition.x - transform.position.x;
            float deltaY = mouseWorldPosition.y - transform.position.y;


            // Calculate the angle in radians
            angleRadians = Mathf.Atan2(deltaY, deltaX);

            // Convert radians to degrees
            float angleDegrees = angleRadians * Mathf.Rad2Deg;

            // Convert negative angles to positive equivalent
            if (angleDegrees < 0)
            {
                angleDegrees += 360f;
            }
        
            lookRotation = Mathf.RoundToInt(angleDegrees);
            weapon.UpdateRotation(lookRotation);
        }
        */


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
            hands.animator.SetBool("FacingDown", true);

            weapon.transform.position = new Vector2(transform.position[0] + weapon.offsets[0], transform.position[1] + weapon.offsets[1]);
        }
        else if (lookRotation > 45 && lookRotation < 90)
        {
            direction = 1;
            animator.SetBool("FacingDown", false);
            healthBar.animator.SetBool("FacingDown", false);
            hands.animator.SetBool("FacingDown", false);

            weapon.transform.position = new Vector2(transform.position[0] + weapon.offsets[0], transform.position[1] + (weapon.offsets[1]) / 2);
        }
        else if(lookRotation > 90 && lookRotation < 135)
        {
            direction = 2;
            animator.SetBool("FacingDown", false);
            healthBar.animator.SetBool("FacingDown", false);
            hands.animator.SetBool("FacingDown", false);

            weapon.transform.position = new Vector2(transform.position[0] - weapon.offsets[0], transform.position[1] + (weapon.offsets[1] / 2));
        }
        else
        {
            direction = 3;
            animator.SetBool("FacingDown", true);
            healthBar.animator.SetBool("FacingDown", true);
            hands.animator.SetBool("FacingDown", true);

            weapon.transform.position = new Vector2(transform.position[0] - weapon.offsets[0], transform.position[1] + weapon.offsets[1]);
        }

        if (direction < 2)
        {
            //Facing Right
            spriteRenderer.flipX = false;
            healthBar.spriteRenderer.flipX = false;
            weapon.spriteRenderer.flipY = false;
            hands.spriteRenderer.flipX = false;
        }
        else
        {
            //Facing Left
            spriteRenderer.flipX = true;
            healthBar.spriteRenderer.flipX = true;
            weapon.spriteRenderer.flipY = true;
            hands.spriteRenderer.flipX = true;
        }

        if (rb2d.velocity.x != 0 || rb2d.velocity.y != 0)
        {
            animator.SetFloat("Speed", 1);
            healthBar.animator.SetFloat("Speed", 1);
            hands.animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            healthBar.animator.SetFloat("Speed", 0);
            hands.animator.SetFloat("Speed", 0);
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

    public void decreaseHealth(int damage){
        health -= damage;
    }

    private void InputInteractable(){
        if(Input.GetMouseButtonDown(0)){
            RemoveFocus();
        }
        if(Input.GetMouseButtonDown(1)){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(lookInputValue[0], lookInputValue[1], 0), 100, crateLayerMask);
            if(hit){
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null){
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus){
        if(newFocus != focus){
            if(focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus(){
        if(focus != null)
            focus.OnDefocused();
        focus = null;
    }
}
