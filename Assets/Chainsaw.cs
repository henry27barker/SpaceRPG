using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    public int damage;
    public float cooldown;

    public EnemyMovement enemyMovement;
    public ChainBotController chainBotController;

    private float counter;
    private bool canDoDamage;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = chainBotController.player;
        canDoDamage = true;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canDoDamage) { 
            if (counter < cooldown)
            {
                counter += Time.deltaTime;
            }
            else
            {
                counter = 0;
                canDoDamage = true;
            }
        }

        //Swing Chainsaw
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 1)
        {
            /*
             * NEED TO IMPLEMENT THE SWING MECHANIC
             */
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && canDoDamage)
        {
            canDoDamage = false;
            col.gameObject.GetComponent<PlayerMovement>().decreaseHealth(damage);
        }
    }
}
