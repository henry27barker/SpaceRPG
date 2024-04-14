using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBotController : MonoBehaviour
{
    public Animator animator;
    public EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMovement.health <= 0)
        {

            Destroy(gameObject);
        }
    }
}
