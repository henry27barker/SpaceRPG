using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : MonoBehaviour
{
    public int damage;
    public bool bounce;
    public float speed;
    public float radius;
    public ParticleSystem explosion;
    public Rigidbody2D rb2d;

    public float timer;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddTorque(Random.Range(-4,4));
        rb2d.velocity = transform.right * speed;

    }

    // Update is called once per frame
    void Update()
    {
        if(bounce)
        {
            if(counter > timer)
            {
                var copy = Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
                copy.GetComponent<EnemyExplosionDamage>().damage = damage;
                copy.GetComponent<EnemyExplosionDamage>().radius = radius;
                copy.GetComponent<EnemyExplosionDamage>().playerDamage = true;
                Destroy(gameObject);
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player")
        {
            var copy = Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
            copy.GetComponent<EnemyExplosionDamage>().damage = damage;
            copy.GetComponent<EnemyExplosionDamage>().radius = radius;
            copy.GetComponent<EnemyExplosionDamage>().playerDamage = true;
            copy.GetComponent<EnemyExplosionDamage>().enemyDamage = true;
            Destroy(gameObject);
        }
        if(!bounce)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Money" || col.gameObject.tag == "Enemy2" || col.gameObject.tag == "Projectile")
            { }
            else
            {
                var copy = Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
                copy.GetComponent<EnemyExplosionDamage>().damage = damage;
                copy.GetComponent<EnemyExplosionDamage>().radius = radius;
                copy.GetComponent<EnemyExplosionDamage>().playerDamage = true;
                copy.GetComponent<EnemyExplosionDamage>().enemyDamage = true;
                Destroy(gameObject);
            }
        }
    }
}
