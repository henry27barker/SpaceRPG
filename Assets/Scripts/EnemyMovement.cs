using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public int damage;
    public float deathRadius;
    private bool deathRadiusReached;
    public float deathTime;
    public GameObject player;
    public PlayerMovement playerController;
    private float distance;
 
    void Start()
    {
        deathRadiusReached = false;
        playerController = player.GetComponent<PlayerMovement>();
    }
 
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance <= deathRadius){
            deathRadiusReached = true;
        }

        if(deathRadiusReached && deathTime > 0){
            deathTime -= Time.deltaTime;
        }
        else if(deathTime <= 0){
            playerController.decreaseHealth(damage);
            Destroy(gameObject);
        }
        else{
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
