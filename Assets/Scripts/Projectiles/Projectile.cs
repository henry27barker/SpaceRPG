using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Chainsaw")
        {
            col.gameObject.GetComponent<Chainsaw>().enemyMovement.decreaseHealth(damage); 
            if (col.gameObject.GetComponent<Chainsaw>().enemyMovement.hasRB)
            {
                col.gameObject.transform.parent.GetComponent<AIPath>().canMove = false;
                col.gameObject.transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.right * 10f, ForceMode2D.Impulse);
            }
            playerMovement.IncreaseHealth((int)(damage * (playerMovement.lifeSteal / 100f)));
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<EnemyMovement>().decreaseHealth(damage);
            playerMovement.IncreaseHealth((int)(damage * (playerMovement.lifeSteal / 100f)));
            if(col.gameObject.GetComponent<EnemyMovement>().hasRB)
            {
                col.gameObject.GetComponent<AIPath>().canMove = false;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 10f, ForceMode2D.Impulse);
            }
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Missile")
        {
            col.gameObject.GetComponent<MissileUp>().Explode();
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Enemy2")
        {
            col.gameObject.GetComponent<AIPath>().canMove = false;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 10f, ForceMode2D.Impulse);
            col.gameObject.GetComponent<Damage>().decreaseHealth(damage);
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (col.gameObject.GetComponent<Mine>())
        {
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
            col.gameObject.GetComponent<Mine>().DestroyMine();
        }
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Obstacle")
        {
            Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
