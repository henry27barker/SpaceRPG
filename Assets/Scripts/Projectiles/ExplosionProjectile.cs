using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float radius;
    public ParticleSystem explosion;
    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;


        explosion.GetComponent<EnemyExplosionDamage>().damage = damage;
        explosion.GetComponent<EnemyExplosionDamage>().radius = radius;
        explosion.GetComponent<EnemyExplosionDamage>().playerDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
    }
}
