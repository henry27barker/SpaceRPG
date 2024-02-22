using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public Animator animator;
    private int direction;
    private SpriteRenderer spriteRenderer;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw ("Horizontal");
        float moveVertical = Input.GetAxisRaw ("Vertical");

        Debug.Log(direction);

        rb2d.velocity = new Vector2 (moveHorizontal, moveVertical).normalized * new Vector2 (speed, speed); 
        if(Mathf.Abs(rb2d.velocity.x) >= Mathf.Abs(rb2d.velocity.y) && rb2d.velocity.x != 0){
            if(rb2d.velocity.x > 0){
                direction = -1;
            }
            else{
                direction = 0;
            }
        }
        else if(rb2d.velocity.y > 0){
            direction = 1;
        }
        else if(rb2d.velocity.y < 0){
            direction = 2;
        }

        if(direction == 0){
            spriteRenderer.flipX = false;
        }
        else{
            spriteRenderer.flipX = true;
        }
        
        if(rb2d.velocity.x != 0 || rb2d.velocity.y != 0){
            animator.SetFloat("Speed", 1);
        }
        else{
            animator.SetFloat("Speed", 0);
        }
        animator.SetInteger("Direction", direction);
    }
}
