using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public GameObject player;
    private float distance;
 
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }
 
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        rb2d.velocity = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
