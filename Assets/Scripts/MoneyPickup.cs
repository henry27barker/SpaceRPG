using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoneyPickup : MonoBehaviour
{
    Collider2D collider;

    private bool go;
    private GameObject player;

    public int amount;

    void Awake(){
        collider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2);
        Debug.DrawLine(transform.position, hit.point);
        if(hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                go = true;
            }
        }
        if(go)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 5 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            col.GetComponent<PlayerMovement>().AddMoney(amount);
            Destroy(gameObject);
        }
    }
}
