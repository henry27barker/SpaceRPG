using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileUp : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2d;
    public SpriteRenderer spriteRenderer;

    public float count = 0;
    private bool down = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.velocity = new Vector2(0,speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!down) 
        {
            if (count > 3)
            {

                rb2d.velocity = new Vector2(0, -2 * speed);
                spriteRenderer.flipY = true;
                count = 0;
            } else
            {
                count += Time.deltaTime;
            }
        }
    }
}
