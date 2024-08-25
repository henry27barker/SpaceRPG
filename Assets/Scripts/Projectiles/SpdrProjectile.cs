using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpdrProjectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D rb2d;
    public GameObject destroyObject;

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
            if (!col.gameObject.GetComponent<PlayerMovement>().parry)
            {
                Instantiate(destroyObject, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        if(col.gameObject.GetComponent<Mine>())
        {
            col.gameObject.GetComponent<Mine>().DestroyMine();
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Obstacle")
        {
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
