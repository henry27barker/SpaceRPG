using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanProjectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D rb2d;

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMovement>().decreaseHealth(damage);
        }
        if (col.gameObject.tag == "Missile" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Obstacle") 
        { 
            Debug.Log("Dont Destroy"); 
        }
        else
            Destroy(gameObject);
    }
}
