using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionDamage : MonoBehaviour
{
    public int damage;
    public bool playerDamage;
    public bool enemyDamage;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag == "Enemy" && enemyDamage)
            {
                hit.gameObject.GetComponent<EnemyMovement>().decreaseHealth(damage);
            }
            if (hit.gameObject.tag == "Enemy2" && enemyDamage)
            {
                hit.gameObject.GetComponent<Damage>().decreaseHealth(damage);
                hit.gameObject.GetComponent<AIPath>().canMove = false;
                hit.gameObject.GetComponent<Rigidbody2D>().AddForce((hit.gameObject.transform.position - transform.position) * 20f, ForceMode2D.Impulse);
            }
            if (hit.gameObject.tag == "Player" && playerDamage)
            {
                hit.gameObject.GetComponent<PlayerMovement>().decreaseHealth(damage);
            }
            if(hit.gameObject.tag == "Obstacle")
            {
                if (hit.gameObject.GetComponent<Mine>())
                {
                    hit.gameObject.GetComponent<Mine>().DestroyMine();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
