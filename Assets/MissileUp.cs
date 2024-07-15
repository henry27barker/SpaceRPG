using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MissileUp : MonoBehaviour
{
    public float speed;
    public int damage;
    public Rigidbody2D rb2d;
    public SpriteRenderer spriteRenderer;
    public GameObject explosion;

    private float count = 0;
    private bool down = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.velocity = new Vector2(0,speed); 
        explosion.GetComponent<EnemyExplosionDamage>().damage = damage;
        explosion.GetComponent<EnemyExplosionDamage>().radius = 3;
        explosion.GetComponent<EnemyExplosionDamage>().playerDamage = true;
        explosion.GetComponent<EnemyExplosionDamage>().enemyDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!down) 
        {
            if (count > 5)
            {
                float newPos = transform.position.x;
                if (transform.parent.gameObject.transform.position.x > 1.2 && transform.parent.gameObject.transform.position.x < 1.5)
                {
                    transform.position = new Vector3(transform.position.x + Random.Range(2,3), transform.position.y, transform.position.z);
                }
                else if (transform.parent.gameObject.transform.position.x > 1.5)
                {
                    transform.position = new Vector3(transform.position.x + Random.Range(3.5f, 5), transform.position.y, transform.position.z);
                }
                else if (transform.parent.gameObject.transform.position.x < -1.2 && transform.parent.gameObject.transform.position.x > -1.5)
                {
                    transform.position = new Vector3(transform.position.x - Random.Range(2, 3), transform.position.y, transform.position.z);
                }
                else if (transform.parent.gameObject.transform.position.x < -1.5)
                {
                    transform.position = new Vector3(transform.position.x - Random.Range(3.5f, 5), transform.position.y, transform.position.z);
                } 
                rb2d.velocity = new Vector2(0, -2 * speed);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                count = 0;
                spriteRenderer.sortingLayerID = SortingLayer.NameToID("Missiles");
                down = true;
            } else
            {
                count += Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerMovement>().decreaseHealth(damage);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
