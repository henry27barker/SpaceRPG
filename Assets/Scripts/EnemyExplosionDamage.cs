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
            else if (hit.gameObject.tag == "Player" && playerDamage)
            {
                hit.gameObject.GetComponent<PlayerMovement>().decreaseHealth(damage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
