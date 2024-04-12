using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    Collider2D collider;

    public int amount;

    void Awake(){
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            col.GetComponent<PlayerMovement>().AddMoney(amount);
            Destroy(gameObject);
        }
    }
}
