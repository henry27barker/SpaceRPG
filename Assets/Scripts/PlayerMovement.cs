using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement playerInstance;

    //Ability Specifications
    public float speed;
    public int maxHealth;
    public int health;
    public int money;
    public float lifeSteal;
    public float maxShieldHealth;
    public float shieldHealth;
    public float shieldRecharge;

    //Settings
    public float rightStickDeadZone;
    public float stompDuration;
    public float stompRadius;
    public int stompDamage;
    public float stompKnockback;
    public float whiteFlashTime;

    //Helper Fields
    public int lookRotation;
    private int direction;
    private float stompCounter = 0f;
    private bool isStomping = false;
    private float stompDamageWait;
    private float whiteFlashCounter = 0f;
    private float footstepCounter = 0;
    private float shootingPointOffset = 0;
    public bool dead = false;

    private bool rechargeShield;
    private bool shoot;

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
    public ParticleSystem healthParticleSystem;
    public GameObject skillTreeUI;
    public GameObject shopUI = null;
    public GameObject inventoryFirst;
    public GameObject skillTreeFirst;
    private GameObject interactMenu;
    public GameObject codeUI = null;
    public AudioSource footstepSource, moneySound;
    public AudioSource stompSource;
    public AudioSource openInventorySound, closeInvetorySound;
    public AudioSource navigationSound;
    public Transform shootingPoint;
    public Camera mainCamera;
    public GameObject shield;
    public Light2D shieldLight;

    //Scripts
    public HealthBar healthBar;
    public WeaponController weapon;
    public HandsController hands;
    public PlayerShoot playerShoot;

    //Interaction
    public Interactable focus;
    public LayerMask interactableLayerMask;
    public GameObject inventoryUI;
    public float pickupRadius;
    private Collider2D[] hits;
    Interactable closestInteractable;
    public GameObject InteractablePrompt;

    
    private void Awake()
    {   
        mainCamera = Camera.main;
        if (playerInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        playerInstance = this;
        DontDestroyOnLoad(gameObject);

        playerControls = gameObject.GetComponent<PlayerInput>();
        Cursor.visible = false;
        skillTreeUI = GameObject.FindWithTag("SkillTree");
        inventoryUI = GameObject.FindWithTag("InventoryUI");
        inventoryFirst = inventoryUI.transform.Find("Inventory/ItemsParent/InventorySlot/ItemButton").gameObject;
        InteractablePrompt = GameObject.FindWithTag("InteractablePrompt");
        interactMenu = GameObject.FindObjectOfType<InteractMenu>().gameObject;
        shootingPointOffset = shootingPoint.localPosition.y;
    }

    private void OnEnable()
    {
        //playerControls.Enable();
        playerControls.actions["Move"].performed += ctx => moveInputValue = ctx.ReadValue<Vector2>();
        playerControls.actions["Move"].canceled += ctx => moveInputValue = Vector2.zero;
        playerControls.actions["Shield"].performed += ctx =>
        {
            shield.SetActive(true);
            rechargeShield = false;
        };
        playerControls.actions["Shield"].canceled += ctx =>
        {
            shield.SetActive(false);
            rechargeShield = true;
        };

        playerControls.actions["Fire"].performed += ctx => shoot = true;
        playerControls.actions["Fire"].canceled += ctx => shoot = false;
    }

    private void OnDisable()
    {
        //playerControls.Disable();
        playerControls.actions["Move"].performed -= ctx => moveInputValue = ctx.ReadValue<Vector2>();
        playerControls.actions["Move"].canceled -= ctx => moveInputValue = Vector2.zero;
        playerControls.actions["Shield"].performed -= ctx =>
        {
            shield.SetActive(true);
            rechargeShield = false;
        };
        playerControls.actions["Shield"].canceled -= ctx =>
        {
            shield.SetActive(false);
            rechargeShield = true;
        };

        playerControls.actions["Fire"].performed -= ctx => shoot = true;
        playerControls.actions["Fire"].canceled -= ctx => shoot = false;
    }

    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 100;
        stompDamageWait = 0.625f * stompDuration;
        healthBar.currentHealth = health;
        InteractablePrompt.SetActive(false);
        shield = transform.Find("Shield").gameObject;
        shieldLight = shield.GetComponent<Light2D> ();
        rechargeShield = true;
    }
 
    void Update()
    {
        if(health <= 0){
            playerControls.SwitchCurrentActionMap("UI");
            if (!dead)
            {
                animator.SetBool("Death", true);
                spriteRenderer.material.SetFloat("_FlashAmount", 0f);
                hands.gameObject.SetActive(false);
                weapon.gameObject.SetActive(false);
                healthBar.gameObject.SetActive(false);
                mainCamera.orthographicSize = 5;
            }
            dead = true;
            return;
        }
        if(inventoryUI.activeSelf == true || skillTreeUI.activeSelf == true || shopUI != null || codeUI != null){
            Time.timeScale = 0;
            return;
        }
        else{
            Time.timeScale = 1;
            healthBar.UpdateColor();
        }

        MovementLogic();
        
        UpdateLookRotation();


        UpdateAnimations();

        
        UpdateHealthBar();

        StompTimer();

        CheckInteractPrompt();

        WhiteFlash();

        HandleShield();

        HandleShoot();
    }

    // private void OnNavigate(){
    //     navigationSound.PlayOneShot(navigationSound.clip);
    // }

    private void OnOpenInventory(){
        if(health <= 0){
            return;
        }
        openInventorySound.Play();
        if(interactMenu.transform.parent.gameObject.activeSelf == false && skillTreeUI.activeSelf == false && shopUI == null && codeUI == null){
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if(inventoryUI.activeSelf == true){
                EventSystem.current.SetSelectedGameObject(null);
                playerControls.SwitchCurrentActionMap("UI");
                EventSystem.current.SetSelectedGameObject(inventoryFirst);
            }
            else{
                playerControls.SwitchCurrentActionMap("Player");
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        skillTreeUI.SetActive(false);
        if (shopUI != null)
        {
            shopUI.SetActive(false);
            shopUI = null;
        }/*
        if (lockerUI != null)
        {
            lockerUI.SetActive(false);
            lockerUI = null;
        }*/
        if (codeUI != null)
        {
            codeUI.SetActive(false);
            codeUI = null;
        }
    }

    private void OnCloseInventory(){
        if(health <= 0){
            return;
        }
        closeInvetorySound.Play();
        if(skillTreeUI.activeSelf == false && shopUI == null && codeUI == null){
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            interactMenu.transform.parent.gameObject.SetActive(false);
            if(inventoryUI.activeSelf == true){
                EventSystem.current.SetSelectedGameObject(null);
                playerControls.SwitchCurrentActionMap("UI");
                EventSystem.current.SetSelectedGameObject(inventoryFirst);
            }
            else{
                playerControls.SwitchCurrentActionMap("Player");
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        skillTreeUI.SetActive(false);
        if(interactMenu.transform.parent.gameObject.activeSelf == false){
            EventSystem.current.SetSelectedGameObject(null);
            playerControls.SwitchCurrentActionMap("Player");
        }
        if(shopUI != null){
            shopUI.SetActive(false);
            shopUI = null;
        }/*
        if (lockerUI != null)
        {
            lockerUI.SetActive(false);
            lockerUI = null;
        }*/
        if (codeUI != null)
        {
            codeUI.SetActive(false);
            codeUI = null;
        }
    }

    private void OnCancel(){
        if(interactMenu.transform.parent.gameObject.activeSelf == true){
            interactMenu.transform.parent.gameObject.SetActive(false);
        }
        else if(health > 0){
        closeInvetorySound.Play();
        if(interactMenu.transform.parent.gameObject.activeSelf == false && skillTreeUI.activeSelf == false && shopUI == null && codeUI == null){
            if(inventoryUI.activeSelf == true){
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                EventSystem.current.SetSelectedGameObject(null);
                playerControls.SwitchCurrentActionMap("UI");
                EventSystem.current.SetSelectedGameObject(inventoryFirst);
            }
            else{
                playerControls.SwitchCurrentActionMap("Player");
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        skillTreeUI.SetActive(false);
        if(interactMenu.transform.parent.gameObject.activeSelf == false){
            EventSystem.current.SetSelectedGameObject(null);
            playerControls.SwitchCurrentActionMap("Player");
        }
        if(shopUI != null){
            shopUI.SetActive(false);
            shopUI = null;
        }/*
        if (lockerUI != null)
        {
            lockerUI.SetActive(false);
            lockerUI = null;
        }*/
        if (codeUI != null)
        {
            codeUI.SetActive(false);
            codeUI = null;
        }
        }
    }

    private void OnLook(InputValue value)
    {
        lookInputValue = value.Get<Vector2>();
    }

    private void OnStomp()
    {
        if (!isStomping)
        {
            Invoke("StompDamage", stompDamageWait);
            animator.SetTrigger("Stomp");
            animator.SetBool("isStomping", true);

            healthBar.animator.SetTrigger("Stomp");
            hands.animator.SetTrigger("Stomp");

            healthBar.animator.SetBool("isStomping", true);
            hands.animator.SetBool("isStomping", true);

            isStomping = true;
        }
    }

    private void StompDamage()
    {
        stompSource.pitch = Random.Range(0.85f, 1f);
        stompSource.Play();
        Collider2D[] stompHits = Physics2D.OverlapCircleAll(transform.position, stompRadius);

        foreach (Collider2D hit in stompHits)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                hit.gameObject.GetComponent<EnemyMovement>().decreaseHealth(stompDamage);
            }
            if (hit.gameObject.tag == "Enemy2")
            {
                hit.gameObject.GetComponent<AIPath>().canMove = false;
                Vector2 direction = transform.position - hit.transform.position;
                direction.Normalize();
                hit.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -50f, ForceMode2D.Impulse);
                hit.gameObject.GetComponent<Damage>().decreaseHealth(stompDamage);
            }
        }
    }


    private void OnInteract()
    {
        hits = Physics2D.OverlapCircleAll(transform.position, pickupRadius, interactableLayerMask);
        SetClosestHitToFront();
        if (hits.Length > 0)
        {
            //Debug.Log("CircleCast hit " + hits[0].transform.name);
            closestInteractable = hits[0].GetComponent<Interactable>();
            if (closestInteractable != null)
            {
                //Debug.Log("Hit");
                SetFocus(closestInteractable);
            }
        }
    }

    private void CheckInteractPrompt()
    {
        hits = Physics2D.OverlapCircleAll(transform.position, pickupRadius, interactableLayerMask);
        SetClosestHitToFront();
        if (hits.Length > 0)
        {
            closestInteractable = hits[0].GetComponent<Interactable>();
            if (closestInteractable != null)
            {
                InteractablePrompt.SetActive(true);
                InteractablePrompt.transform.position = closestInteractable.transform.position;
            }
            else{
                InteractablePrompt.SetActive(false);
            }
        }
        else{
            InteractablePrompt.SetActive(false);
        }
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

    private void WhiteFlash() {

        if (whiteFlashCounter > 0)
        {
            spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            hands.spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            weapon.spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            healthBar.spriteRenderer.material.SetFloat("_FlashAmount", 0.5f);
            whiteFlashCounter -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.material.SetFloat("_FlashAmount", 0f);
            hands.spriteRenderer.material.SetFloat("_FlashAmount", 0f);
            weapon.spriteRenderer.material.SetFloat("_FlashAmount", 0f);
            healthBar.spriteRenderer.material.SetFloat("_FlashAmount", 0f);
        }
    }
    
    private void MovementLogic()
    {
        rb2d.velocity = new Vector2(moveInputValue[0], moveInputValue[1]).normalized * new Vector2(speed, speed);

        if(footstepCounter > 0.3 && rb2d.velocity.magnitude != 0)// Mathf.Abs(rb2d.velocity.magnitude / 10) && rb2d.velocity.magnitude != 0)
        {
            footstepSource.volume = Random.Range(0.25f, 0.35f);
            footstepSource.pitch = Random.Range(0.85f, 1f);
            footstepSource.Play();
            footstepCounter = 0;
        } else
        {
            footstepCounter += Time.deltaTime;
        }
    }


    private void UpdateLookRotation()
    {
        if(health > 0){
            float angleRadians = 0;
            // GAMEPAD
            if (playerControls.currentControlScheme == "Gamepad")// && (Gamepad.current.leftStick.ReadValue() != Vector2.zero || Gamepad.current.rightStick.ReadValue() != Vector2.zero))
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
            
            else
            {
                //Add Crosshair
                mouseCursor.SetActive(true);
                // Find mouse position
                Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
            hands.animator.SetBool("FacingDown", true);

            //weapon.transform.position = new Vector2(transform.position[0] + weapon.offsets[0], transform.position[1] + weapon.offsets[1]);
        }
        else if (lookRotation > 45 && lookRotation < 90)
        {
            direction = 1;
            animator.SetBool("FacingDown", false);
            healthBar.animator.SetBool("FacingDown", false);
            hands.animator.SetBool("FacingDown", false);

            //weapon.transform.position = new Vector2(transform.position[0] + weapon.offsets[0], transform.position[1] + (weapon.offsets[1]));
        }
        else if(lookRotation >= 90 && lookRotation < 135)
        {
            direction = 2;
            animator.SetBool("FacingDown", false);
            healthBar.animator.SetBool("FacingDown", false);
            hands.animator.SetBool("FacingDown", false);

            //weapon.transform.position = new Vector2(transform.position[0] - weapon.offsets[0], transform.position[1] + (weapon.offsets[1]));
        }
        else
        {
            direction = 3;
            animator.SetBool("FacingDown", true);
            healthBar.animator.SetBool("FacingDown", true);
            hands.animator.SetBool("FacingDown", true);
 
            //weapon.transform.position = new Vector2(transform.position[0] - weapon.offsets[0], transform.position[1] + weapon.offsets[1]);
        }

        if(lookRotation <= 90 || lookRotation > 350)
        {
            //Aiming From 12 o'clock to aroud 5 o'clock
            weapon.transform.localScale = new Vector2(1, 1); 
            weapon.spriteRenderer.sortingOrder = 0;
        } else if (lookRotation <= 315 && lookRotation > 270)
        {
            //Aiming From around 5 o'clock to 6 o'clock
            weapon.transform.localScale = new Vector2(1, -1);
            weapon.spriteRenderer.sortingOrder = 10;
        } else if (lookRotation <= 270 && lookRotation > 225)
        {
            //Aiming From 6 o'clock to aroud 8 o'clock
            weapon.transform.localScale = new Vector2(1, 1);
            weapon.spriteRenderer.sortingOrder = 10;
        } else
        {
            //Aiming From around 8 o'clock to 12 o'clock
            weapon.transform.localScale = new Vector2(1, -1);
            weapon.spriteRenderer.sortingOrder = 0;
        }

            if (direction == 2 || direction == 3)
        {
            //Facing Left
            spriteRenderer.flipX = true;
            healthBar.spriteRenderer.flipX = true;
            hands.spriteRenderer.flipX = true;
            //shootingPoint.localPosition = new Vector3(0.5f, -0.164f, 0);
        } else 
        {
            //Facing Right
            spriteRenderer.flipX = false;
            healthBar.spriteRenderer.flipX = false;
            hands.spriteRenderer.flipX = false;
            //shootingPoint.localPosition = new Vector3(0.5f, 0.164f, 0);
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
            decreaseHealth(10);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreaseHealth(10);
        }
        
    }

    public void decreaseHealth(int damage)
    {
        if(shield.activeSelf)
        {
            if (shieldHealth - damage < 0)
            {
                damage -= (int)shieldHealth;
                shieldHealth = 0;
                whiteFlashCounter = whiteFlashTime;
                health -= damage;

                healthBar.UpdateColor();
            } else
            {
                shieldHealth -= damage;
            }
        } else
        {
            whiteFlashCounter = whiteFlashTime;
            health -= damage;

            healthBar.UpdateColor();
        }
    }

    public void IncreaseHealth(int amount){
        
        if (health + amount <= maxHealth){
            health += amount;
            healthBar.UpdateColor();
            StartCoroutine(BeginHealthParticleSystem(amount));
        }
        else{
            if(health < maxHealth){
                StartCoroutine(BeginHealthParticleSystem(maxHealth - health));
            }
            health = maxHealth;
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
    
    private void SetClosestHitToFront(){
        if(hits.Length < 1){
            return;
        }
        int minIndex = 0;
        for(int i = 1; i < hits.Length; i++){
            if(Vector2.Distance(new Vector2(hits[i].transform.position.x, hits[i].transform.position.y), new Vector2(transform.position.x, transform.position.y)) < Vector2.Distance(new Vector2(hits[minIndex].transform.position.x, hits[minIndex].transform.position.y), new Vector2(transform.position.x, transform.position.y))){
                minIndex = i;
            }
        }
        hits[0] = hits[minIndex];
    }

    public void AddMoney(int amount){
        money += amount;
        moneySound.PlayOneShot(moneySound.clip);
    }

    public void StartPill(int healthAmount, int time){
        StartCoroutine(Begin(healthAmount, time));
        Debug.Log("starting pill");
    }

    IEnumerator Begin(int healthAmount, int time){
        Debug.Log("in begin");
        float counter = time;
        while(counter > 0){
            yield return StartCoroutine(Wait(1));
            Debug.Log("in while loop");
            IncreaseHealth(healthAmount/time);
            counter--;
        }
    }

    IEnumerator BeginHealthParticleSystem(int amount){
        var emission = healthParticleSystem.emission;
        emission.rateOverTime = amount / 5f;
        ParticleSystem currentHealthParticle = Instantiate(healthParticleSystem, this.transform);
        yield return StartCoroutine(Wait(3));
        Destroy(currentHealthParticle.gameObject);
    }

    private IEnumerator Wait(float waitTime)
    {
        float counter = waitTime;
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            yield return null;
        }
    }

    private void HandleShield()
    {
        if(rechargeShield && shieldHealth < maxShieldHealth)
        {
            shieldHealth += shieldRecharge * Time.deltaTime;
        } else if (!rechargeShield && shieldHealth > 0)
        {
            shieldHealth -= 50f * Time.deltaTime;
        }
        float tempScale = shieldHealth / maxShieldHealth;
        shield.transform.localScale = new Vector3(tempScale, tempScale, 1);
        shieldLight.pointLightOuterRadius = 3 * tempScale;
    }

    private void HandleShoot()
    {
        if(shoot)
        {
            playerShoot.Shoot();
        }
    }
}
