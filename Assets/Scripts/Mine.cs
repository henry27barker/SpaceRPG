using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMine()
    {
        var copy = Instantiate(explosion, transform.position, Quaternion.identity);
        copy.GetComponent<EnemyExplosionDamage>().playerDamage = true;
        copy.GetComponent<EnemyExplosionDamage>().enemyDamage = true;
        Destroy(gameObject);
    }
}
